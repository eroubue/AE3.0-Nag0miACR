using System.Net.Http.Json;
using AEAssist;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.SGE.Settings;

namespace Nagomi.SGE.utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class FFlogsAPI
{
    private readonly string _apiKey;
    private readonly HttpClient _client;
    private const string BaseUrl = "https://www.fflogs.com/api/v2/client";
    private readonly Func<Task<string>> _getTokenCallback;

    public FFlogsAPI(string apiKey, Func<Task<string>> getTokenCallback)
    {
        _apiKey = apiKey;
        _getTokenCallback = getTokenCallback;
        _client = new HttpClient();
    }

    /// <summary>
    /// 获取访问令牌（OAuth2 Client Credentials Flow）
    /// </summary>
    public async Task<string> GetAccessTokenAsync()
    {
        try
        {
            var clientId = SGESettings.Instance?.FFlogsClientId ?? string.Empty;
            var clientSecret = SGESettings.Instance?.FFlogsClientSecret ?? string.Empty;
            
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("Client ID and Client Secret must be configured");
            }

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://www.fflogs.com/oauth/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials"
                })
            };

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", 
                Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"))
            );

            var response = await _client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(responseContent);
            return json.RootElement.GetProperty("access_token").GetString();
        }
        catch (Exception ex)
        {
            Core.Resolve<MemApiChatMessage>().Toast($"FFlogs API Error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 执行GraphQL查询
    /// </summary>
    public async Task<JsonElement> ExecuteGraphQlQueryAsync(string query)
    {
        var token = await _getTokenCallback();
        var retryCount = 0;
        var startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        
        while (retryCount < 10)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, BaseUrl)
            {
                Content = JsonContent.Create(new { query })
            };
            
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Headers.Add("Accept", "application/json");
            
            var response = await _client.SendAsync(requestMessage);
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStreamAsync();
                var json = await JsonDocument.ParseAsync(responseContent);
                return json.RootElement;
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                token = await GetAccessTokenAsync();
                retryCount++;
                continue;
            }
            
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Core.Resolve<MemApiChatMessage>().Toast("Rate limit reached, waiting for 1 hour...");
                var timeStruct = DateTimeOffset.FromUnixTimeSeconds(startTime + 3660).DateTime;
                Core.Resolve<MemApiChatMessage>().Toast($"Resuming at {timeStruct:yyyy-MM-dd HH:mm:ss}");
                
                await Task.Delay(3660000);
                startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                retryCount++;
                continue;
            }

            response.EnsureSuccessStatusCode();
        }
        
        throw new HttpRequestException($"Failed to execute GraphQL query after {retryCount} retries");
    }

    /// <summary>
    /// 构建NDPS查询语句（服务器名即为slug）
    /// </summary>
    private string BuildNDPSQuery(string characterName, string serverSlug, string region, int zoneId)
    {
        return $@"
{{
  characterData {{
    character(name: ""{characterName}"", serverSlug: ""{serverSlug}"", serverRegion: ""{region}"") {{
      zoneRankings(zoneID: {zoneId}, metric: ""nDPS"") {{
        rankings {{
          encounterID
          encounter {{
            name
          }}
          bestAmount
        }}
      }}
    }}
  }}
}}";
    }

    /// <summary>
    /// 获取指定玩家在指定地图的NDPS（服务器名即为slug）
    /// </summary>
    public async Task<Dictionary<string, float>> GetCharacterNDPSAsync(string characterName, string serverName, string region, int zoneId)
    {
        var query = BuildNDPSQuery(characterName, serverName, region, zoneId);
        var json = await ExecuteGraphQlQueryAsync(query);

        var result = new Dictionary<string, float>();

        var rankings = json
            .GetProperty("data")
            .GetProperty("characterData")
            .GetProperty("character")
            .GetProperty("zoneRankings")
            .GetProperty("rankings");

        foreach (var ranking in rankings.EnumerateArray())
        {
            var bossName = ranking.GetProperty("encounter").GetProperty("name").GetString();
            var ndps = ranking.GetProperty("bestAmount").GetSingle();
            result[bossName] = ndps;
        }

        return result;
    }

    /// <summary>
    /// 构建绝本查询语句（通过encounterID，支持metric参数）
    /// </summary>
    private string BuildUltimateNDPSQuery(string characterName, string serverSlug, string region, int encounterID, string metric)
    {
        return $@"
{{
  characterData {{
    character(name: ""{characterName}"", serverSlug: ""{serverSlug}"", serverRegion: ""{region}"") {{
      encounterRankings(encounterID: {encounterID}, metric: {metric})
    }}
  }}
}}";
    }

    /// <summary>
    /// 查询指定角色在指定绝本(encounterID)的当前职业最高metric（ndps/rdps/adps）、击杀数、排名百分比
    /// </summary>
    public async Task<(float value, int totalKills, float rankPercent)> GetCurrentJobBestMetricAsync(string characterName, string serverSlug, string region, int encounterID, string currentJob, string metric)
    {
        var query = BuildUltimateNDPSQuery(characterName, serverSlug, region, encounterID, metric);
        var json = await ExecuteGraphQlQueryAsync(query);

        try
        {
            var data = json.GetProperty("data");
            if (!data.TryGetProperty("characterData", out var characterData) ||
                !characterData.TryGetProperty("character", out var character) ||
                !character.TryGetProperty("encounterRankings", out var rankings) ||
                rankings.ValueKind != JsonValueKind.Object)
            {
                throw new Exception("未查询到该角色的绝本数据，可能角色名、服务器名、区服或encounterID错误，或该角色无该绝本记录。");
            }

            // 解析总击杀次数
            int totalKills = rankings.TryGetProperty("totalKills", out var tk) && tk.ValueKind == JsonValueKind.Number ? tk.GetInt32() : 0;

            float maxValue = 0f;
            float maxRankPercent = 0f;
            if (rankings.TryGetProperty("ranks", out var ranks) && ranks.ValueKind == JsonValueKind.Array)
            {
                foreach (var rank in ranks.EnumerateArray())
                {
                    if (rank.TryGetProperty("spec", out var spec) && spec.GetString() == currentJob)
                    {
                        float value = 0f;
                        // 优先取metric字段
                        if (rank.TryGetProperty(metric.ToUpper(), out var metricVal) && metricVal.ValueKind == JsonValueKind.Number)
                        {
                            value = metricVal.GetSingle();
                        }
                        else if (rank.TryGetProperty(metric, out var metricVal2) && metricVal2.ValueKind == JsonValueKind.Number)
                        {
                            value = metricVal2.GetSingle();
                        }
                        else if (rank.TryGetProperty("amount", out var amount) && amount.ValueKind == JsonValueKind.Number)
                        {
                            value = amount.GetSingle();
                        }
                        if (value > maxValue)
                        {
                            maxValue = value;
                            // 优先rankPercent，其次historicalPercent、todayPercent
                            if (rank.TryGetProperty("rankPercent", out var rp) && rp.ValueKind == JsonValueKind.Number)
                                maxRankPercent = rp.GetSingle();
                            else if (rank.TryGetProperty("historicalPercent", out var hp) && hp.ValueKind == JsonValueKind.Number)
                                maxRankPercent = hp.GetSingle();
                            else if (rank.TryGetProperty("todayPercent", out var tp) && tp.ValueKind == JsonValueKind.Number)
                                maxRankPercent = tp.GetSingle();
                            else
                                maxRankPercent = 0f;
                        }
                    }
                }
            }
            return (maxValue, totalKills, maxRankPercent);
        }
        catch (Exception ex)
        {
            LogHelper.Print($"解析fflogs返回数据失败: {ex.Message}\n原始json: {json.ToString()}");
            return (0f, 0, 0f);
        }
    }

    /// <summary>
    /// 构建zone列表查询语句
    /// </summary>
    private string BuildZoneListQuery()
    {
        return @"
        {
          gameData {
            zones {
              id
              name
            }
          }
        }";
    }

    /// <summary>
    /// 获取zoneID（副本ID）
    /// </summary>
    public async Task<int> GetZoneIdAsync(string zoneName)
    {
        var query = BuildZoneListQuery();
        var json = await ExecuteGraphQlQueryAsync(query);

        foreach (var zone in json.GetProperty("data").GetProperty("gameData").GetProperty("zones").EnumerateArray())
        {
            if (zone.GetProperty("name").GetString().Equals(zoneName, StringComparison.OrdinalIgnoreCase))
            {
                return zone.GetProperty("id").GetInt32();
            }
        }
        throw new Exception("Zone not found");
    }
}
