using AEAssist.CombatRoutine.Module;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.GCD;

public class GNBGCD_base : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public int Check()
    {
        if (Core.Me.Distance(Core.Me.GetCurrTarget(),DistanceMode.IgnoreAll)
            > (float)SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            return -12;
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        slot.Add(spell);
    }

    private Spell GetSpell()
    {
        if (QT.QTGET(QTKey.AOE) && GNBSpells.恶魔切.IsReady())
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
            var level = Core.Me.Level;
            if (aoeCount >= 2 && level >= 72)
            {
                if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.恶魔切 && GNBSpells.恶魔杀.IsReady())
                    return GNBSpells.恶魔杀.GetSpell();
                return GNBSpells.恶魔切.GetSpell();
            }
            if (aoeCount >= 3 && level < 72)
            {
                if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.恶魔切 && GNBSpells.恶魔杀.IsReady())
                    return GNBSpells.恶魔杀.GetSpell();
                return GNBSpells.恶魔切.GetSpell();
            }
        }

        if (Core.Me.Distance(Core.Me.GetCurrTarget(),DistanceMode.IgnoreAll) <= SettingMgr.GetSetting<GeneralSettings>().AttackRange)
        {
            if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹 && GNBSpells.迅连斩.IsReady())
                return GNBSpells.迅连斩.GetSpell();
            if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.利刃斩 && GNBSpells.残暴弹.IsReady())
                return GNBSpells.残暴弹.GetSpell();
            return GNBSpells.利刃斩.GetSpell();
        }
        else return null;
    }
}