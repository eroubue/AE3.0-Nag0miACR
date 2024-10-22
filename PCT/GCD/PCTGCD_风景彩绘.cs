using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;

namespace Nagomi.PCT.GCD;

public class PCTGCD_风景彩绘 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.IsMoving()&&!Core.Me.HasAura(167)) return -1;
        if (Core.Resolve<JobApi_Pictomancer>().风景画)
        {
            return -2;
        }
        if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘) == PCTSpells.风景彩绘)
        {
            return -4;
        }
        if (Core.Me.HasAura(PCTBuffs.绘灵幻景))
        {
            return -1;
        }
        if (!QT.QTGET(QTKey.风景彩绘) || !Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘).IsReady())
        {
            return -3;
        }
        var 血量 = 0.0;
        if (Core.Me.GetCurrTarget() != null)
            血量 = Core.Me.GetCurrTarget().CurrentHpPercent()*100;

        if (血量 <= PCTSettings.画画百分比 && Core.Me.GetCurrTarget() != null) return -5;

        return 1;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        slot.Add(spell);
    }

    private Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘).GetSpell();
    }
}