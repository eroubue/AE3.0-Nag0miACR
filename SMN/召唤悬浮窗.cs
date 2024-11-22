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

namespace Nagomi.SMN;

public class 召唤悬浮窗
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
    

   public static void DrawDev(JobViewWindow jobViewWindow)
    {
        ImGui.TextUnformatted($"召唤兽在场的时间: {Core.Resolve<JobApi_Summoner>().SummonTimerRemaining}");
        ImGui.TextUnformatted($"以太超流层数: {Core.Resolve<JobApi_Summoner>().AetherflowStacks}");
        ImGui.TextUnformatted($"召唤兽: {Core.Resolve<JobApi_Summoner>().ActivePetType}");
        ImGui.TextUnformatted($"召唤技能层数(修正): {Core.Resolve<JobApi_Summoner>().AttunementAdjust}");
        ImGui.TextUnformatted($"三神宝石的持续时间: {Core.Resolve<JobApi_Summoner>().AttunmentTimerRemaining}");
        ImGui.TextUnformatted($"有以太超流: {Core.Resolve<JobApi_Summoner>().HasAetherflowStacks}");
        ImGui.TextUnformatted($"有宝石兽: {Core.Resolve<JobApi_Summoner>().HasPet}");
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

    }

    public static void 通用(JobViewWindow jobViewWindow)
    {
        
        if (ImGui.Button("获取触发器链接"))
        {
            Core.Resolve<MemApiChatMessage>().Toast2("感谢使用零师傅工具箱\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
            Core.Resolve<MemApiSendMessage>().SendMessage("/e https://11142.kstore.space/TriggernometryExport.xml");
        }
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
 
        ;
        
    }
}