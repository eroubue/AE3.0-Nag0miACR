using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.GCD;

public class GNBGCD_子弹连 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    private bool CheckSpell()
    {
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.裂牙.GetSpell().Id).IsUnlockWithCDCheck()) return true;
        return false;
    }
    public int Check()
    {
        
        if (QT.QTGET(QTKey.停手)) { return -100; }
        if (GNBSettings.Instance.启用覆盖额外距离&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        if (!CheckSpell()) return -1;
        if (!GNBSpells.裂牙.GetSpell().IsReadyWithCanCast())
        {//技能没准备好 返回-45

            // if (AEAssist.Helper.SpellExtension.IsReady(GNBSpells.子弹2) && (!SpellExtension.IsReady(GNBSpells.DoubleDown))) return 1;
            if (GNBSpells.猛兽爪.GetSpell().IsReadyWithCanCast() && (!GNBSpells.倍攻.GetSpell().IsReadyWithCanCast())) return 1;

            // if (AEAssist.Helper.SpellExtension.IsReady(GNBSpells.子弹3) && (!SpellExtension.IsReady(GNBSpells.DoubleDown))) return 1;
            if (GNBSpells.凶禽爪.GetSpell().IsReadyWithCanCast() && (!GNBSpells.倍攻.GetSpell().IsReadyWithCanCast())) return 1;
            return -45;


        }
         if (QT.QTGET(QTKey.倾泻爆发)&&Core.Resolve<JobApi_GunBreaker>().Ammo > 0)
        {
            return 100;
        }
        if (!QT.QTGET(QTKey.爆发))
        {
            return -10;
        }
        if (!QT.QTGET(QTKey.子弹连)) { return -10; }
        if (Core.Resolve<JobApi_GunBreaker>().Ammo <= 0) return -20;
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
        if (Core.Me.Level >= 72 && aoeCount >= 3) //敌人数量大于3不打子弹连
            return -9;
        if (SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)) return -1;

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.裂牙).GetSpell());
    }


}