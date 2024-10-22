using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace Nagomi.PCT;

public class 画家悬浮窗
{
    private static bool showHiddenImgui = false;
    private static int decorativeSliderValue = 0;
    private static int decorativeSliderValue1;
    private static int decorativeSliderValue2;
    private static int decorativeSliderValue3;
    private static int decorativeSliderValue4;
    public static void DrawDev(JobViewWindow jobViewWindow)
    {
        ImGui.TextUnformatted($"豆子: {Core.Resolve<JobApi_Pictomancer>().豆子}");
        ImGui.TextUnformatted($"能量: {Core.Resolve<JobApi_Pictomancer>().能量}");
        ImGui.TextUnformatted($"动物画: {Core.Resolve<JobApi_Pictomancer>().生物画}");
        ImGui.TextUnformatted($"风景画: {Core.Resolve<JobApi_Pictomancer>().风景画}");
        ImGui.TextUnformatted($"武器画: {Core.Resolve<JobApi_Pictomancer>().武器画}");
        ImGui.TextUnformatted($"马丁: {Core.Resolve<JobApi_Pictomancer>().蔬菜准备}");
        ImGui.TextUnformatted($"莫古: {Core.Resolve<JobApi_Pictomancer>().莫古准备}");
        ImGui.TextUnformatted($"动物充能: {Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.动物构想).GetSpell().Charges}");
        //ImGui.TextUnformatted($"CanCast: {PctData.Spells.短1.GetSpell().CanCastEx()}");
        ImGui.TextUnformatted($"团辅: {PCTSpells.风景构想.GetSpell().Charges}");
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

            if (ImGui.Button("来一枪", new Vector2(120, 40)))
            {
                轮盘赌();
               
            }

            var level = Share.VIP.Level;
            if (showHiddenImgui&&level >0)
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
            else
            {
                ImGui.Text("恭喜你，你赢了！但是因为是通用码未能成功解锁底裤功能！");
                ImGui.Text("因为伊侑白雪@水晶塔直接把这个拿来发大群跳脸绿玩了捏");
            }


          

            // 恢复样式
            ImGui.PopStyleColor(3);
        }
        void 轮盘赌()
        {
            // 先进行随机数判断
            if (new Random().Next(1, 2) == 1)
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/xlkill");
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