using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.GCD;

public class GNBGCD_音速破 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public int Check()
    {
        
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        } 
        if (QT.QTGET(QTKey.倾泻爆发))
        {
            return 100;
        }
        if (!QT.QTGET(QTKey.爆发))
        {
            return -10;
        }
        if (!QT.QTGET(QTKey.dot))
        {
            return -10;
        }
        if (GNBSettings.Instance.启用覆盖额外距离&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        if (!GNBSpells.音速破.IsUnlockWithCDCheck())
        {
            return -66;
        }
        if  (!Core.Me.HasAura(GNBBuffs.音速破预备)) return -88;
        if (GNBSpells.裂牙.GetSpell().IsReadyWithCanCast()) return -6;
        if(Core.Resolve<MemApiSpell>().GetLastComboSpellId() ==GNBSpells.残暴弹)
            return -7;
        return 0;
    }
    
    private Spell GetSpell()
    {
        return GNBSpells.音速破.GetSpell();

    }
     public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}