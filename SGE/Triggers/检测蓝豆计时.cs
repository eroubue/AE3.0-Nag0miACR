using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using ECommons.LanguageHelpers;
using ImGuiNET;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Nagomi.SGE.Triggers;

public class 贤者时间轴蓝豆计时 : ITriggerCond
{
    [LabelName("蛇胆数量")]
    public int BlueTime { get;set; }

    public string DisplayName => "SGE/检测量谱-返回蓝豆计时".Loc();

    public string Remark { get; set; }
    private int BlueTimer = 0;

    public bool Draw()
    {
        ImGuiHelper.LeftInputInt("蓝豆计时", ref BlueTimer, 0, 20000);
        ImGui.Text("当前蓝豆计时大于输入值返回true");
        ImGui.Text("蓝豆计时为0-20000递增，到达20000时重置为0并增加一个蓝豆");
        return true;
    }

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return Core.Resolve<JobApi_Sage>().AddersgallTimer >= BlueTimer;
    }
}