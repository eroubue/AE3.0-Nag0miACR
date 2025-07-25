using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.GCD;

public class GNBGCD_AOEbase : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public int Check()
    {
        var aoecount = TargetHelper.GetNearbyEnemyCount(5);
        
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        } 
        if (!QT.QTGET(QTKey.AOE))
        {
            return -10;
        }
        
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            5) return -5;
        if (Core.Me.Level < 84 && aoecount >= 2) return 0;
        if (Core.Me.Level >= 84 && aoecount >= 3) return 0;
        return -1;
    }
    
    private Spell GetSpell()
    {
        var aoecount = TargetHelper.GetNearbyEnemyCount(5);
            if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.恶魔切 && GNBSpells.恶魔杀.GetSpell().IsReadyWithCanCast())
                return GNBSpells.恶魔杀.GetSpell();
            if (aoecount>=2&&Core.Resolve<MemApiSpell>().GetLastComboSpellId() is not GNBSpells.恶魔切)
                return GNBSpells.恶魔切.GetSpell();
            return null;

    }
     public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}