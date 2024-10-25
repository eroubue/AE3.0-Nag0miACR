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
using ECommons.ImGuiMethods;

namespace Nagomi.PCT;

public class 画家悬浮窗
{
    private static bool showHiddenImgui = false;
    private static int decorativeSliderValue = 0;
    private static int decorativeSliderValue1;
    private static int decorativeSliderValue2;
    private static int decorativeSliderValue3;
    private static int decorativeSliderValue4;
    private static string AECounts;
    public static async void aPDnGP6b1(List<string> _param0, Action<List<string>> _param1)
    {
        try
        {
            _param1(await QQ0kis1E1(string.Join("|", (IEnumerable<string>) _param0)));
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
        }
    }
    internal static async Task<List<string>> QQ0kis1E1(string _param0)
    {
        List<string> list;
        using (HttpClient client = new HttpClient((HttpMessageHandler) new HttpClientHandler()
               {
                   UseProxy = false
               }))
        {
            string stringAsync = await client.GetStringAsync("http://175.178.228.101:20100/CheckAECount?playerCIDs=" + _param0);
            list = stringAsync.Length <= 0 ? (List<string>) null : ((IEnumerable<string>) stringAsync.Split("|")).ToList<string>();
        }
        return list;
    }


public static void ae人数查询(JobViewWindow jobViewWindow)
{
    try
    {
        void 轮盘赌()
        {
            // 先进行随机数判断
            if (new Random().Next(1, 2) == 1)
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/shout 全体目光向我看齐，我宣布个事，我是个啥b!!!");
            }
            else
            {
                Core.Resolve<MemApiChatMessage>().Toast2("没中,奖励一下！", 1, 2000);
                showHiddenImgui = true;
              
            }
        }
        if (ImGui.Button("来一枪解锁", new Vector2(180, 40)))
        {
            轮盘赌();
               
        }

        if (showHiddenImgui)
        {
            if (ImGui.Button("检测队伍AE用户数量"))
            {
                AECounts = string.Empty;
                if (Svc.Party != null && Svc.Party.Any())
                {
                    List<string> list = Svc.Party.Select(p => p.ContentId.ToString()).ToList();
                    if (list.Any())
                        aPDnGP6b1(list, ae人数);
                    else
                        LogHelper.Print("检测不到你的队伍或者你当前不在副本内!");
                }
                else
                {
                    LogHelper.Print("检测不到你的队伍或者你当前不在副本内!");
                }
            }

            if (string.IsNullOrEmpty(AECounts))
                return;

            ImGui.Text(AECounts);
        }

        
            
    }
    catch (Exception ex)
    {
        LogHelper.Print($"发生错误: {ex.Message}");
    }
}

public static void ae人数(List<string> _param1)
{
    try
    {
        if (_param1 == null)
        {
            AECounts = string.Empty;
            return;
        }

        if (Svc.Party?.PartyId == 0L)
            return;

        StringBuilder sb = new StringBuilder();
        foreach (var partyMember in Svc.Party)
        {
            if (_param1.Contains(partyMember.ContentId.ToString()))
            {
                sb.AppendLine(partyMember.Name.ToString());
            }
        }

        AECounts = sb.ToString();
    }
    catch (Exception ex)
    {
        LogHelper.Print($"发生错误: {ex.Message}");
    }
}


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

        if (ImGui.CollapsingHeader("LB"))
        {
            ImGui.Text("LB充能槽数量: " + Core.Me.LimitBreakBarCount().ToString());
            ImGui.Text("每条充能槽填满所需: " + Core.Me.LimitBreakBarValue().ToString());
            ImGui.Text("当前LB充能数值: " + Core.Me.LimitBreakCurrentValue().ToString());
        }

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
        
        if (ImGui.Button("获取触发器链接"))
        {
            Core.Resolve<MemApiChatMessage>().Toast2("感谢使用零师傅工具箱\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
            Core.Resolve<MemApiSendMessage>().SendMessage("/e https://11142.kstore.space/TriggernometryExport.xml");
        }
        ImGui.Text("导入到act高级触发器插件的远程触发器中，使用前请更新!");

        if (ImGui.CollapsingHeader("底裤功能,轮盘赌通过才可开启"))
        {
            // 获取当前时间
            float time = (float)ImGui.GetTime();
    

            // 创建脉动效果
            float scale = 1.0f + 0.05f * (float)Math.Sin(time * 2.0f);

            // 设置按钮样式
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.1f, 0.7f, 0.4f, 1.0f)); // 按钮的背景颜色
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.1f, 0.9f, 0.6f, 1.0f)); // 鼠标悬停时的背景颜色
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.1f, 0.5f, 0.3f, 1.0f)); // 按钮按下时的背景颜色

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
 
        ;
        
    }
}