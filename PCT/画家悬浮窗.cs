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
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons.GameFunctions;
using ECommons.ImGuiMethods;
using Nagomi.Shared;

namespace Nagomi.PCT;

public class 画家悬浮窗
{
    // 存储AE Counts的字符串变量
    //private static string AECounts;

    /// <summary>
    /// 异步处理玩家CID列表，调用外部API获取信息，并通过回调函数处理结果
    /// </summary>
    /// <param name="_param0">玩家CID列表</param>
    /// <param name="_param1">回调函数，处理API返回的信息</param>
    public static async void http请求分割(List<string> _param0, Action<List<string>> _param1)
    {
        try
        {
            // 调用函数，并在获取结果后通过_param1回调
            _param1(await http请求(string.Join("|", (IEnumerable<string>) _param0)));
        }
        catch (Exception ex)
        {
            // 异常处理
            LogHelper.Error(ex.ToString());
        }
    }

    /// <summary>
    /// 根据玩家CID获取AE Counts信息
    /// </summary>
    /// <param name="_param0">玩家CID字符串，多个CID用'|'分隔</param>
    /// <returns>AE Counts信息列表</returns>
    internal static async Task<List<string>> http请求(string _param0)
    {
        List<string> list;
        // 使用HttpClient获取远程API信息
        using (HttpClient client = new HttpClient((HttpMessageHandler) new HttpClientHandler()
               {
                   UseProxy = false
               }))
        {
            // 异步获取字符串信息
            string stringAsync = await client.GetStringAsync("http://175.178.228.101:20100/CheckAECount?playerCIDs=" + _param0);
            // 分隔并转换信息为字符串列表
            list = stringAsync.Length <= 0 ? (List<string>) null : ((IEnumerable<string>) stringAsync.Split("|")).ToList<string>();
        }
        return list;
    }

    /// <summary>
    /// 查询AE人数的主函数
    /// </summary>
    /// <param name="jobViewWindow">JobViewWindow实例</param>
    /*public static void ae人数查询(JobViewWindow jobViewWindow)
    {
        try
        {

            // 如果showHiddenImgui标志为true，则显示AE人数查询界面
            if (showHiddenImguiPro)
            {
                // 如果用户点击查询按钮
                if (ImGui.Button("检测队伍AE用户数量"))
                {
                    AECounts = string.Empty;
                    // 检查队伍是否存在，并获取队伍成员的ContentId列表
                    if (Svc.Party != null && Svc.Party.Any())
                    {
                        List<string> list = Svc.Party.Select(p => p.ContentId.ToString()).ToList();
                        // 如果列表不为空，则调用aPDnGP6b1函数并传递回调函数ae人数
                        if (list.Any())
                            http请求分割(list, ae人数);
                        else
                            LogHelper.Print("检测不到你的队伍或者你当前不在副本内!");
                    }
                    else
                    {
                        LogHelper.Print("检测不到你的队伍或者你当前不在副本内!");
                    }
                }
                if (ImGui.Button("检测目标是否开ae"))
                {
                    AECounts = string.Empty;
                    // 检查队伍是否存在，并获取队伍成员的ContentId列表
                    if (Svc.Targets.Target is IPlayerCharacter c)
                    {
                        unsafe
                        {
                            var cid = c.Struct()->ContentId;
                            string cidStr = cid.ToString();
                            List<string> list = new List<string> { cidStr };
                            // 如果列表不为空，则调用aPDnGP6b1函数并传递回调函数ae人数
                            if (list.Any())
                                http请求分割(list,ae单人);
                            else
                                LogHelper.Print("检测不到你的目标或发生错误!");
                        }
                        
                        
                    }
                    else
                    {
                        LogHelper.Print("检测不到你的目标或发生错误!");
                    }
                }

                // 如果AECounts不为空，则显示AECounts信息
                if (string.IsNullOrEmpty(AECounts))
                    return;

                ImGui.Text(AECounts);
            }
            
            {if (!showHiddenImguiPro)
                ImGui.Text("未解锁");
            }
        }
        catch (Exception ex)
        {
            // 异常处理
            LogHelper.Print($"发生错误: {ex.Message}");
        }
    }

    /// <summary>
    /// 处理AE人数查询结果的回调函数
    /// </summary>
    /// <param name="_param1">AE人数信息列表</param>
    public static void ae人数(List<string> _param1)
    {
        try
        {
            // 如果参数为空，清空AECounts并返回
            if (_param1 == null)
            {
                AECounts = string.Empty;
                return;
            }

            // 如果队伍ID为0，则直接返回
            if (Svc.Party?.PartyId == 0)
                return;

            // 构建AE人数信息字符串
            StringBuilder sb = new StringBuilder();
            foreach (var partyMember in Svc.Party)
            {
                if (_param1.Contains(partyMember.ContentId.ToString()))
                {
                    sb.AppendLine(partyMember.Name.ToString());
                }
            }

            // 将构建的字符串赋值给AECounts
            AECounts = sb.ToString();
        }
        catch (Exception ex)
        {
            // 异常处理
            LogHelper.Print($"发生错误: {ex.Message}");
        }
    }
    public static void ae单人(List<string> _param1)
    {
        try
        {
            // 如果参数为空，清空AECounts并返回
            if (_param1 == null || !_param1.Any())
            {
                AECounts = string.Empty;
                LogHelper.Print("绿玩!");
                Core.Resolve<MemApiChatMessage>().Toast2("绿玩", 1, 1000);
                return;
            }
            if (Svc.Targets.Target is IPlayerCharacter c)
                unsafe
                {
                    var cid = c.Struct()->ContentId;
                    LogHelper.Print($"目标cid: {cid}");
                }
             
            LogHelper.Print("目标已开AE");
            Core.Resolve<MemApiChatMessage>().Toast2("挂勾！", 2, 1000);
            
        }
        catch (Exception ex)
        {
            // 异常处理
            LogHelper.Print($"发生错误: {ex.Message}");
        }
    }*/

   public static void DrawDev(JobViewWindow jobViewWindow)
    {
        ImGui.TextUnformatted($"豆子: {Core.Resolve<JobApi_Pictomancer>().豆子}");
        ImGui.TextUnformatted($"能量: {Core.Resolve<JobApi_Pictomancer>().能量}");
        ImGui.TextUnformatted($"动物画: {Core.Resolve<JobApi_Pictomancer>().生物画}");
        ImGui.TextUnformatted($"风景画: {Core.Resolve<JobApi_Pictomancer>().风景画}");
        ImGui.TextUnformatted($"武器画: {Core.Resolve<JobApi_Pictomancer>().武器画}");
        ImGui.TextUnformatted($"马丁: {Core.Resolve<JobApi_Pictomancer>().蔬菜准备}");
        ImGui.TextUnformatted($"莫古: {Core.Resolve<JobApi_Pictomancer>().莫古准备}");
        ImGui.TextUnformatted(
            $"动物充能: {Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.动物构想).GetSpell().Charges}");
        //ImGui.TextUnformatted($"CanCast: {PctData.Spells.短1.GetSpell().CanCastEx()}");
        ImGui.TextUnformatted($"团辅: {PCTSpells.风景构想.GetSpell().Charges}");
        
        // 使用共享模块
        共享悬浮窗模块.DrawCountdownInfo();
        共享悬浮窗模块.DrawCombatInfo();
        共享悬浮窗模块.DrawLBInfo();

        if (ImGui.Button("打印小队CID"))
        {
            foreach (Dalamud.Game.ClientState.Party.IPartyMember partyMember in
                     (IEnumerable<Dalamud.Game.ClientState.Party.IPartyMember>)Svc.Party)
                LogHelper.Print(partyMember.ContentId.ToString());
            LogHelper.Print(Svc.ClientState.LocalContentId.ToString());
        }
    }

    public static void 通用(JobViewWindow jobViewWindow)
    {
        ImGui.Checkbox("锁定前冲步面板", ref PCTSettings.Instance.isEnAvantPanelLocked);
        
        // 使用共享模块
        共享悬浮窗模块.DrawGetLinksButtons();
        共享悬浮窗模块.DrawHiddenFeatures("PCT");
    }
}