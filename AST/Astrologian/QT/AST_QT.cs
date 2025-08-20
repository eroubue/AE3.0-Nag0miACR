using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.MemoryApi;

namespace Millusion.ACR.Astrologian.QT;

public static class AST_QT
{
    public static void AddAllQT(JobViewWindow ui)
    {
        ui.AddQt(AST_QT_Key.Stop, false, b =>
        {
            if (b) Core.Resolve<MemApiSpell>().CancelCast();
        });
        ui.SetQtToolTip("停止一切行为");
        ui.AddQt(AST_QT_Key.UseDot, true, "自动DoT");
        ui.AddQt(AST_QT_Key.UseAOE, true, "自动AOE");
        ui.AddQt(AST_QT_Key.UseDivination, true, "占卜");
        ui.AddQt(AST_QT_Key.UseNeutralSect, true, "中立学派");
        ui.AddQt(AST_QT_Key.UseMacrocosmos, true, "大宇宙");
        // UI.AddQt(QTKey.EarthlyStarToSelf, false, "地星自己");
        // QT.AddQt(QTKey.UseLucidDreaming, true, "自动醒梦");
        ui.AddQt(AST_QT_Key.UseSingleHeal, true, "使用单体治疗");
        ui.AddQt(AST_QT_Key.UseRangeHeal, true, "使用群体治疗");
        ui.AddQt(AST_QT_Key.UseAbilityHeal, true, "使用能力技治疗");
        ui.AddQt(AST_QT_Key.UseGCDHeal, true, "使用GCD治疗");
        ui.AddQt(AST_QT_Key.OnlyHealTank, false, "单体治疗技能，只治疗坦克");
        // UI.AddQt(QTKey.OnlyGCDHeal, false, "只使用GCD治疗");
        ui.AddQt(AST_QT_Key.SwiftcastAscend, true, "即刻复活");
        ui.AddQt(AST_QT_Key.CastAscend, false, "读条复活");
        ui.AddQt(AST_QT_Key.UseEsuna, true, "驱散");
    }
}