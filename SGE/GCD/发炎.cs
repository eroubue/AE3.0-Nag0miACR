using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using System.Runtime;
using Nagomi.GNB.utils;
using Nagomi.SGE;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
namespace Nagomi.SGE.GCD;

public class 发炎 : ISlotResolver

{
    
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级没到不打

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.发炎).IsUnlockWithCDCheck())
        {
            return -66;
        }
        if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 6+SGESettings.Instance.额外技能距离) return -1;
        if (QT.QTGET(QTKey.保留发炎) && Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.发炎).GetSpell().Charges <= SGESettings.Instance.发炎保留数量) return -6;
        if (QT.QTGET(QTKey.爆发)) return 100;
        if (!QT.QTGET(QTKey.发炎)) return -2;
        if (Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.发炎).GetSpell().Charges > 1.9)
        {
            return 2;
        }
        if (Core.Resolve<MemApiMove>().IsMoving())
        { 
             return 1; 
        }
        return -1;
    }
    public Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.发炎).GetSpell();
    }


    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}