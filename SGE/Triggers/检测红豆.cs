using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using ECommons.LanguageHelpers;
using Nagomi.utils.Helper;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Nagomi.SGE.Triggers;

public class 贤者时间轴红豆状态 : ITriggerCond
{
    [LabelName("蛇刺数量")]
    public int Red { get; set; }

    public string DisplayName => "SGE/检测量谱-红豆大于等于指定数值".Loc();

    public string Remark { get; set; }

    public bool Draw()
    {
        return false;
    }

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return Core.Resolve<JobApi_Sage>().Addersting >= Red;
    }
}