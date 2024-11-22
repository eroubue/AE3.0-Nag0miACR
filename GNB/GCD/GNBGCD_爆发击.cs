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

public class GNBGCD_爆发击 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public int Check()
    {
        if (Core.Resolve<JobApi_GunBreaker>().Ammo <= 0) return -20;
        
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        } 
        if (!QT.QTGET(QTKey.爆发击))
        {
            return -10;
        }
        if (GNBSettings.Instance.启用覆盖额外距离&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        if (!GNBSpells.爆发击.IsUnlockWithCDCheck())
        {
            return -66;
        }
       if (Core.Me.Distance(Core.Me.GetCurrTarget(),DistanceMode.IgnoreAll)
                > (float)SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            return -12;
        if (!GNBSpells.爆发击.IsUnlockWithCDCheck()) return -2;
        if (Core.Resolve<JobApi_GunBreaker>().Ammo < 1) return -3;
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
        if (aoeCount >= 2 && GNBSpells.命运之环.IsUnlockWithCDCheck()) return -6;

        if (Core.Me.Level < 88 && !GNBSpells.血壤.IsUnlockWithCDCheck())
        {
            if (Core.Resolve<MemApiBuff>().HasMyAura(Core.Me, GNBBuffs.无情))
            {
                if ((Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.裂牙) != GNBSpells.裂牙 ||  GNBSpells.裂牙.CoolDownInGCDs(3)) || GNBSpells.裂牙.IsUnlockWithCDCheck()) return -2;
            }
            else
            {
                if (Core.Resolve<JobApi_GunBreaker>().Ammo < 2 ||
               Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.残暴弹) return -3;
            }
        }
        else if (Core.Me.Level >= 88 && !GNBSpells.血壤.IsUnlockWithCDCheck())
        {
            if(Core.Resolve<JobApi_GunBreaker>().Ammo == 3 && Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹 && GNBSpells.无情.CoolDownInGCDs(1) && !GNBSpells.无情.IsUnlockWithCDCheck()) return 8;
            if (Core.Resolve<MemApiBuff>().HasMyAura(Core.Me, GNBBuffs.无情))
            {
                if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.裂牙) != GNBSpells.裂牙 || GNBSpells.裂牙.CoolDownInGCDs(3) || GNBSpells.倍攻.CoolDownInGCDs(3)) return -2;
            }
            else
            {
                if (Core.Resolve<JobApi_GunBreaker>().Ammo < 3 || Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.残暴弹) return -3;
            }
        }
        else if (Core.Me.Level < 88 && GNBSpells.血壤.IsUnlockWithCDCheck())
        {
            if (!GNBSpells.裂牙.CoolDownInGCDs(Core.Resolve<JobApi_GunBreaker>().Ammo)) return 1;
        }
        else if (Core.Me.Level == 90 && GNBSpells.血壤.IsUnlockWithCDCheck())
        {
            if (!GNBSpells.倍攻.CoolDownInGCDs(Core.Resolve<JobApi_GunBreaker>().Ammo)) return 1;
        }
        if (GNBSpells.无情.CoolDownInGCDs(1)) return -1;
        //if (GunbreakerRotationEntry.QT.GetQt("自动拉怪")) return -5;



        return 0;
    }
    
    private Spell GetSpell()
    {
        return GNBSpells.爆发击.GetSpell();

    }
     public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}