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
        if (GNBSettings.Instance.启用覆盖额外距离&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        return 0;
    }
    
    private Spell GetSpell()
    {
            if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹 && GNBSpells.迅连斩.IsUnlockWithCDCheck())
                return GNBSpells.迅连斩.GetSpell();
            if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.利刃斩 && GNBSpells.残暴弹.IsUnlockWithCDCheck())
                return GNBSpells.残暴弹.GetSpell();
            return GNBSpells.利刃斩.GetSpell();
      
    }
     public void Build(Slot slot)
    {
        var spell = GetSpell();
        slot.Add(spell);
    }
}