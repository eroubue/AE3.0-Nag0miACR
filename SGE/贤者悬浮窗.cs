using AEAssist;
using AEAssist.API.MemoryApi;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using AEAssist.Verify;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Party;
using System.Runtime.CompilerServices;
using System.Text;
using AEAssist.GUI;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons.GameFunctions;
using ECommons.ImGuiMethods;

using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
using Wotou.Dancer.Utility;


namespace Nagomi.SGE;

public class 贤者悬浮窗
{
    private static bool showHiddenImgui = false;
    private static bool showHiddenImguiPro = false;
    private static int decorativeSliderValue = 0;
    private static int decorativeSliderValue1;
    private static int decorativeSliderValue2;
    private static int decorativeSliderValue3;
    private static int decorativeSliderValue4;
    private static string password = "";
    private const string correctPassword = "22/7";
    private const string correctPasswordPro = "我是西条和的狗";// 替换为实际的密码
    private static string characterName = "角色名";
    private static string serverName = "服务器名";
    private static string region = "CN"; // 如 "CN"
    private static int encounterID = 1061; // 默认UwU

    private static string resultText = "";
    private static bool isLoading = false;
    
    private static int selectedMetricIndex = 0;
    private static readonly string[] metricOptions = { "ndps", "rdps", "dps", "cdps", "wdps" };

    private static List<(string Name, string World, float MetricValue, float RankPercent)> partyFflogsList = new();
    private static bool isPartyQuerying = false;
    

   public static void DrawDev(JobViewWindow jobViewWindow)
    {
        ImGui.TextUnformatted($"均衡状态: {Core.Resolve<JobApi_Sage>().Eukrasia}");
        ImGui.TextUnformatted($"蓝豆: {Core.Resolve<JobApi_Sage>().Addersgall}");
        ImGui.TextUnformatted($"蓝豆计时: {Core.Resolve<JobApi_Sage>().AddersgallTimer}");
        ImGui.TextUnformatted($"红豆: {Core.Resolve<JobApi_Sage>().Addersting}");
      
        
        
        DefaultInterpolatedStringHandler interpolatedStringHandler;
        
        if (ImGui.CollapsingHeader("倒计时"))
        {
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
            interpolatedStringHandler.AppendLiteral("状态: ");
            interpolatedStringHandler.AppendFormatted<bool>(Core.Resolve<MemApiCountdown>().IsActive());
            ImGui.TextUnformatted(interpolatedStringHandler.ToStringAndClear());
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
            interpolatedStringHandler.AppendLiteral("倒计时剩余: ");
            interpolatedStringHandler.AppendFormatted<float>(Core.Resolve<MemApiCountdown>().TimeRemaining());
            ImGui.TextUnformatted(interpolatedStringHandler.ToStringAndClear());
        }
        if (ImGui.CollapsingHeader("通用"))
        {
            ImGui.TextUnformatted($"自身是否在移动: {Nagomi.Helper.自身是否在移动()}");
            ImGui.TextUnformatted($"自身是否在读条: {Nagomi.Helper.自身是否在读条()}");
            ImGui.TextUnformatted($"GCD剩余时间: {Nagomi.Helper.GCD剩余时间()}");
            ImGui.TextUnformatted($"GCD可用状态: {Nagomi.Helper.GCD可用状态()}");
            ImGui.TextUnformatted($"高优先级gcd队列中技能数量: {Nagomi.Helper.高优先级gcd队列中技能数量()}");
            ImGui.TextUnformatted($"上一个gcd技能: {Nagomi.Helper.上一个gcd技能()}");
            ImGui.TextUnformatted($"上一个能力技能: {Nagomi.Helper.上一个能力技能()}");
            ImGui.TextUnformatted($"上一个连击技能: {Nagomi.Helper.上一个连击技能()}");
        }

        if (ImGui.CollapsingHeader("LB"))
        {
            ImGui.Text("LB充能槽数量: " + Core.Me.LimitBreakBarCount().ToString());
            ImGui.Text("每条充能槽填满所需: " + Core.Me.LimitBreakBarValue().ToString());
            ImGui.Text("当前LB充能数值: " + Core.Me.LimitBreakCurrentValue().ToString());
        }

    }
   public static void xuanfuchuang(JobViewWindow jobViewWindow)
    {

        ImGui.SameLine();
        if (SGESettings.Instance.H1)
        {
            ImGui.TextColored(new System.Numerics.Vector4(1.0f, 0.0f, 0.0f, 1.0f), "H1");
        }
        else if (!SGESettings.Instance.H1)
        {
            ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "H2");
        }

        ImGui.SameLine();
        if (ImGui.Button(" H1 "))
        {
            SGESettings.Instance.H1 = true;
            SGESettings.Instance.Save();
        }

        ImGui.SameLine();
        if (ImGui.Button(" H2 "))
        {
            SGESettings.Instance.H1 = false;
            SGESettings.Instance.Save();
        }

        ImGui.SliderInt("红豆保留数量", ref SGESettings.Instance.红豆保留数量, 1, 3);
        ImGui.SliderInt("发炎保留数量", ref SGESettings.Instance.发炎保留数量, 1, 2);
        ImGui.Text("保留QT是开了QT就绝对不打，动了也不打");
        if (ImGui.Button("失衡走位关"))
        {
            SGESettings.Instance.失衡走位 = 0;
            SGESettings.Instance.Save();
        }

        ImGui.SameLine();
        if (ImGui.Button("失衡走位开"))
        {
            SGESettings.Instance.失衡走位 = 1;
            SGESettings.Instance.Save();
        }

        ImGui.SameLine();
        ImGui.Text("失衡走位：");
        ImGui.SameLine();
       
        if (SGESettings.Instance.失衡走位 == 0)
        {
            ImGui.TextColored(new System.Numerics.Vector4(1.0f, 0.0f, 0.0f, 1.0f), "关"); // 绿色
        }
        else if (SGESettings.Instance.失衡走位 == 1)
        {
            ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "开"); // 蓝色
        }
        if (ImGui.Button("即刻贤炮关"))
        {
            SGESettings.Instance.即刻贤炮 = 0;
            SGESettings.Instance.Save();
        }
        ImGui.SameLine();
        if (ImGui.Button("即刻贤炮开"))
        {
            SGESettings.Instance.即刻贤炮 = 1;
            SGESettings.Instance.Save();
        }
        
        ImGui.SameLine();
        ImGui.Text("即刻贤炮热键：");
        ImGui.SameLine();
        if (SGESettings.Instance.即刻贤炮 == 0)
        {
            ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f), "关"); // 绿色
        }
        else if (SGESettings.Instance.即刻贤炮 == 1)
        {
            ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 0.0f, 1.0f), "开"); // 蓝色
        }
        ImGuiHelper.LeftInputInt("混合输血目标PM", ref SGESettings.Instance.混合输血目标,1, 8);
        
        var opener = "";
        switch (SGESettings.Instance.opener)
        {
            case 0:
                opener = "默认起手";
                break;
            case 1:
                opener = "贤炮3g团辅dot起手";
                break;
            case 2:
                opener = "贤炮直接dot起手";
                break;
            case 3:
                opener = "神兵起手";
                break;
        }
        if (ImGui.BeginCombo("起手选择", opener))
        {
            
        
            if (ImGui.Selectable("默认起手"))
            {
                SGESettings.Instance.opener = 0;
                SGESettings.Instance.Save();
            }

            if (ImGui.Selectable("贤炮3g团辅dot起手"))
            {
               SGESettings.Instance.opener = 1;
                SGESettings.Instance.Save();
            }
            if (ImGui.Selectable("贤炮直接dot起手"))
            {
               SGESettings.Instance.opener = 2;
                SGESettings.Instance.Save();
            }
            if (ImGui.Selectable("神兵起手"))
            {
                SGESettings.Instance.opener = 3;
                SGESettings.Instance.Save();
            }

            ImGui.EndCombo();
        }

        if (ImGui.CollapsingHeader("股批设置"))
        {
            ImGui.InputText("client", ref SGESettings.Instance.FFlogsClientId, 256);
            ImGui.InputText("Secret", ref SGESettings.Instance.FFlogsClientSecret, 256);
            ImGui.Text(Core.Me.Name.ToString());
            ImGui.Text(Core.Me.HomeWorld.ValueNullable?.Name.ExtractText() 
                       ?? "Unknown World");
            
            ImGui.Combo("查询类型", ref selectedMetricIndex, metricOptions, metricOptions.Length);
            if (ImGui.Button("查询绝神兵") && !isPartyQuerying)
            {
                resultText = "查询中...";
                characterName = Core.Me.Name.ToString();
                serverName = Core.Me.HomeWorld.ValueNullable?.Name.ExtractText() ?? "Unknown World";
                string metric = metricOptions[selectedMetricIndex];
                Task.Run(async () =>
                {
                    try
                    {
                        var api = new FFlogsAPI("", null);
                        var token = await api.GetAccessTokenAsync();
                        var api2 = new FFlogsAPI("", () => Task.FromResult(token));
                        // 这里每次都重新获取当前职业
                        string currentJob = Core.Me.CurrentJob().ToString();
                        var (value, kills, rankPercent) = await api2.GetCurrentJobBestMetricAsync(characterName, serverName, region, encounterID, currentJob, metric);
                        // 染色逻辑
                        float rtn = rankPercent;
                        System.Numerics.Vector4 color = new(0.5f, 0.5f, 0.5f, 1.0f);
                        if (rtn >= 25 && rtn < 50) color = new(0.2f, 0.8f, 0.2f, 1.0f);
                        else if (rtn >= 50 && rtn < 75) color = new(0.2f, 0.6f, 1.0f, 1.0f);
                        else if (rtn >= 75 && rtn < 95) color = new(0.7f, 0.3f, 1.0f, 1.0f);
                        else if (rtn >= 95 && rtn < 99) color = new(1.0f, 0.5f, 0.0f, 1.0f);
                        else if (rtn >= 99 && rtn < 100) color = new(1.0f, 0.4f, 0.7f, 1.0f);
                        else if (rtn >= 100) color = new(1.0f, 0.85f, 0.0f, 1.0f);
                        resultText = $"最高{metric}: {value}\n击杀数: {kills}\n排名百分比: {rtn:F2}";
                        // 不再拼接色名
                    }
                    catch (Exception ex)
                    {
                        resultText = $"查询失败: {ex.Message}";
                    }
                    isLoading = false;
                });
            }
            if (ImGui.Button("查询小队复活优先级") && !isPartyQuerying)
            {
                isPartyQuerying = true;
                string metric = metricOptions[selectedMetricIndex];
                int encounterId = encounterID;
                string region = "CN";
                // 不要清空 partyFflogsList
                Task.Run(async () =>
                {
                    var tempList = new List<(string Name, string World, float MetricValue, float RankPercent)>();
                    try
                    {
                        var api = new FFlogsAPI("", null);
                        var token = await api.GetAccessTokenAsync();
                        var api2 = new FFlogsAPI("", () => Task.FromResult(token));
                        var partyList = PartyHelper.Party;
                        /*foreach (var member in partyList)
                        {
                            if (member is IPlayerCharacter playerCharacter)
                            {
                                string name = playerCharacter.Name.ToString();
                                
                                string world = playerCharacter.HomeWorld.ValueNullable?.Name.ExtractText() ?? "Unknown";
                                string job = playerCharacter.CurrentJob().ToString();
                                LogHelper.Print($"{name},{world},{job}");
                                var (value, _, rankPercent) = await api2.GetCurrentJobBestMetricAsync(name, world, region, encounterId, job, metric);
                                tempList.Add((name, world, value, rankPercent));
                            }
                        }*/
                        foreach (var member in Svc.Party)
                        {
                            if (member is not null && member.Name != null)
                            {
                                string name = member.Name.ToString();
                                string world = member.World.ValueNullable?.Name.ExtractText() ?? "Unknown World";
                                string job = member.ClassJob.ValueNullable?.Name.ExtractText() ?? "Unknown Job";
                                LogHelper.Print($"{name},{world},{job}");
                                var (value, _, rankPercent) = await api2.GetCurrentJobBestMetricAsync(name, world, region, encounterId, job, metric);
                                tempList.Add((name, world, value, rankPercent));
                                // 直接用这些信息即可
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Print($"小队查询失败: {ex.Message}");
                    }
                    // 排序后一次性赋值（UI线程安全）
                    partyFflogsList = tempList
                        .OrderByDescending(x => x.MetricValue != -1 && x.RankPercent != -1) // 有权限的在前
                        .ThenByDescending(x => x.MetricValue) // 再按指标降序
                        .ToList();
                    isPartyQuerying = false;
                });
            }
            if (isPartyQuerying)
            {
                ImGui.Text("正在查询小队数据...");
            }
            if (partyFflogsList.Count > 0)
            {
                ImGui.Separator();
                ImGui.Text($"小队{metricOptions[selectedMetricIndex]}复活优先级（高→低）:");
                ImGui.Columns(4, "partyfflogs");
                ImGui.Text("#"); ImGui.NextColumn();
                ImGui.Text("名字@服务器"); ImGui.NextColumn();
                ImGui.Text($"{metricOptions[selectedMetricIndex]}"); ImGui.NextColumn();
                ImGui.Text("排名百分比"); ImGui.NextColumn();
                ImGui.Separator();
                int idx = 1;
                // 先有权限成员，后无权限成员
                var normal = partyFflogsList.Where(x => x.MetricValue != -1 && x.RankPercent != -1).OrderByDescending(x => x.MetricValue).ToList();
                var privates = partyFflogsList.Where(x => x.MetricValue == -1 || x.RankPercent == -1).ToList();
                foreach (var member in normal)
                {
                    ImGui.Text(idx.ToString()); ImGui.NextColumn();
                    ImGui.Text($"{member.Name}@{member.World}"); ImGui.NextColumn();
                    ImGui.Text($"{member.MetricValue:F1}"); ImGui.NextColumn();
                    // 染色
                    System.Numerics.Vector4 color = new(0.5f, 0.5f, 0.5f, 1.0f);
                    float rtn = member.RankPercent;
                    if (rtn >= 25 && rtn < 50) color = new(0.2f, 0.8f, 0.2f, 1.0f);
                    else if (rtn >= 50 && rtn < 75) color = new(0.2f, 0.6f, 1.0f, 1.0f);
                    else if (rtn >= 75 && rtn < 95) color = new(0.7f, 0.3f, 1.0f, 1.0f);
                    else if (rtn >= 95 && rtn < 99) color = new(1.0f, 0.5f, 0.0f, 1.0f);
                    else if (rtn >= 99 && rtn < 100) color = new(1.0f, 0.4f, 0.7f, 1.0f);
                    else if (rtn >= 100) color = new(1.0f, 0.85f, 0.0f, 1.0f);
                    ImGui.TextColored(color, $"{member.RankPercent:F2}"); ImGui.NextColumn();
                    idx++;
                }
                foreach (var member in privates)
                {
                    ImGui.Text(idx.ToString()); ImGui.NextColumn();
                    ImGui.Text($"{member.Name}@{member.World}"); ImGui.NextColumn();
                    ImGui.TextColored(new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 1.0f), "无权限/无记录"); ImGui.NextColumn();
                    ImGui.TextColored(new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 1.0f), "无权限/无记录"); ImGui.NextColumn();
                    idx++;
                }
                ImGui.Columns(1);
            }
            // 查询结果显示
            if (!string.IsNullOrEmpty(resultText))
            {
                var lines = resultText.Split('\n');
                bool isPrivate = false;
                float rtn = 0f;
                foreach (var line in lines)
                {
                    if (line.StartsWith("排名百分比:"))
                    {
                        var parts = line.Split(':');
                        if (parts.Length == 2 && float.TryParse(parts[1].Trim(), out rtn))
                        {
                            if (rtn == -1f)
                            {
                                isPrivate = true;
                                break;
                            }
                        }
                    }
                }
                if (isPrivate)
                {
                    ImGui.TextColored(new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 1.0f), "无权限/无记录");
                }
                else
                {
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("排名百分比:"))
                        {
                            var parts = line.Split(':');
                            if (parts.Length == 2 && float.TryParse(parts[1].Trim(), out rtn))
                            {
                                System.Numerics.Vector4 color = new(0.5f, 0.5f, 0.5f, 1.0f);
                                if (rtn >= 25 && rtn < 50) color = new(0.2f, 0.8f, 0.2f, 1.0f);
                                else if (rtn >= 50 && rtn < 75) color = new(0.2f, 0.6f, 1.0f, 1.0f);
                                else if (rtn >= 75 && rtn < 95) color = new(0.7f, 0.3f, 1.0f, 1.0f);
                                else if (rtn >= 95 && rtn < 99) color = new(1.0f, 0.5f, 0.0f, 1.0f);
                                else if (rtn >= 99 && rtn < 100) color = new(1.0f, 0.4f, 0.7f, 1.0f);
                                else if (rtn >= 100) color = new(1.0f, 0.85f, 0.0f, 1.0f);
                                ImGui.Text("排名百分比: ");
                                ImGui.SameLine();
                                ImGui.TextColored(color, $"{rtn:F2}");
                            }
                        }
                        else
                        {
                            ImGui.Text(line);
                        }
                    }
                }
            }
            ImGui.SameLine();
            if (ImGui.Button("刷新FFlogs数据"))
            {
                Core.Resolve<MemApiChatMessage>().Toast("刷新功能待实现");
            }
        }
        


        if (ImGui.Button("获取画图绝神兵宝宝椅链接"))
        {
            Core.Resolve<MemApiChatMessage>().Toast2("感谢使用零师傅宝宝椅\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
            Core.Resolve<MemApiSendMessage>().SendMessage("/e https://d.feiliupan.com/t/105019529578418176/零师傅远程触发器.xml");
        }
        if (ImGui.CollapsingHeader("底裤功能,轮盘赌通过才可开启"))
        {
            // 获取当前时间
            float time = (float)ImGui.GetTime();


            // 创建脉动效果
            float scale = 1.0f + 0.05f * (float)Math.Sin(time * 2.0f);

            // 设置按钮样式
            ImGui.PushStyleColor(ImGuiCol.Button, new System.Numerics.Vector4(0.1f, 0.7f, 0.4f, 1.0f)); // 按钮的背景颜色
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new System.Numerics.Vector4(0.1f, 0.9f, 0.6f, 1.0f)); // 鼠标悬停时的背景颜色
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new System.Numerics.Vector4(0.1f, 0.5f, 0.3f, 1.0f)); // 按钮按下时的背景颜色

            // 创建按钮并检查是否被点击，手动设置按钮的大小以实现缩放效果
            Vector2 buttonSize = new Vector2(140 * scale, 60 * scale); // 为按键设置动态大小

            if (ImGui.Button("来一枪解锁底裤功能", new Vector2(180, 40)))
            {
                轮盘赌();

            }


            if (showHiddenImgui)
            {
                ImGui.Text("恭喜你，你赢了！成功解锁底裤功能！");
                ImGui.SliderInt("修改额外暴击率", ref decorativeSliderValue, 0, 15);
                ImGui.SliderInt("修改额外直击率", ref decorativeSliderValue1, 0, 15);
                ImGui.SliderInt("稀有物品爆率提升(坐骑等)", ref decorativeSliderValue2, 0, 20);
                ImGui.SliderInt("伤害浮动固定(最高提升5%)", ref decorativeSliderValue3, 0, 5);
                ImGui.SliderInt("挖宝下底加成", ref decorativeSliderValue4, 0, 5);
                ImGui.Button("批量生成高级码(lv1以上可用)", new Vector2(300, 40));
                ImGui.Button("一键金100logs", new Vector2(180, 40));
                ImGui.Button("对选中目标批量发送举报加速封号", new Vector2(300, 40));


            }



            // 恢复样式
            ImGui.PopStyleColor(3);
        }

        ImGui.Text("输入密码来绕过轮盘赌");

        // 创建一个文本输入框，用于输入密码
        ImGui.InputText("密码", ref password, 256, ImGuiInputTextFlags.Password);

        // 创建一个按钮，点击后检查密码
        if (ImGui.Button("提交"))
        {
            if (password == correctPassword)
            {
                showHiddenImgui = true;

            }

            if (password == correctPasswordPro)
            {
                showHiddenImgui = true;
                showHiddenImguiPro = true;
            }
            else
            {
                // 可以在这里添加密码错误的提示
                ImGui.TextColored(new System.Numerics.Vector4(1, 0, 0, 1), "错误的密码!");
            }
        }

        // 如果showHiddenImgui为true，显示隐藏的内容
        if (showHiddenImgui)
        {
            ImGui.Text("已经解锁!");
        }

        void 轮盘赌()
        {
            // 先进行随机数判断
            if (new Random().Next(1, 3) == 1)
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/shout 全体目光向我看齐，我宣布个事，我是个啥b!!!");
            }
            else
            {
                Core.Resolve<MemApiChatMessage>().Toast2("没中,奖励一下！", 1, 2000);
                showHiddenImgui = true;

            }
        }
    }
  



    
}