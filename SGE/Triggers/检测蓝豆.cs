using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using ECommons.LanguageHelpers;
using PCT.utils.Helper;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Nagomi.SGE.Triggers;

public class 贤者时间轴蓝豆状态 : ITriggerCond
{
    [LabelName("蛇胆数量")]
    public int Blue { get; set; }

    public string DisplayName => "SGE/检测量谱-蓝豆大于等于指定数值".Loc();

    public string Remark { get; set; }

    public bool Draw()
    {
        return false;
    }

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return Core.Resolve<JobApi_Sage>().Addersgall >= Blue;
    }
}