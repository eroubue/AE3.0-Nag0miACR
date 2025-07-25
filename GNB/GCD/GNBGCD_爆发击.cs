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
        
        if (GNBSettings.Instance.额外技能距离!=0&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
        if (GNBSettings.Instance.额外技能距离==0&&Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        if (!GNBSpells.爆发击.GetSpell().IsReadyWithCanCast())
        {
            return -66;
        }

        // 优先判断子弹连和狮心连
        

        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
        if (aoeCount >= 2 && GNBSpells.命运之环.GetSpell().IsReadyWithCanCast()&&QT.QTGET(QTKey.AOE)) return -6;
        if (QT.QTGET(QTKey.倾泻爆发)&&Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1&&!QT.QTGET(QTKey.仅使用爆发击卸除子弹))
        {
            return 100;
        }
        if (QT.QTGET(QTKey.仅使用爆发击卸除子弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1) return 1;
        
        var gnashingFangSlot = new GNBGCD_子弹连();
        if (gnashingFangSlot.Check() >= 0)
            return -7; // 子弹连优先，爆发击不打
        var lionHeartSlot = new GNBGCD_狮心连();
        if (lionHeartSlot.Check() >= 0)
            return -8; // 狮心连优先，爆发击不打
       
        if (Core.Me.Level < 88) //两颗子弹
        {
            
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 2 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹) return 2;//溢出时打
            if (Core.Me.HasAura(GNBBuffs.无情)) return 1;
        }
        if (Core.Me.Level >= 88)//三颗子弹
        {
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹) return 2;//溢出时打
            if (Helper.技能22s内是否用过(GNBSpells.无情)) return 13;//无情里填充
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹&&!SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)) return 22;//120后填充期
            if (QT.QTGET(QTKey.二弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo != 2 && SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)&&SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 8)) return 23;//120卸子弹
            if (QT.QTGET(QTKey.零弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo != 0 && SpellExtension.CoolDownInGCDs(GNBSpells.无情, 4)&&SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 8)) return 23;
            if (QT.QTGET(QTKey.零弹) && SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 7)) return 1;//零弹120，打完120前的子弹连后有就打
        }
        
        
       /* if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3)
        {
            if (Core.Me.Level == 100) //100级有狮心连时
            {
                if (!Core.Me.HasAura(GNBBuffs.无情)&&Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹)
                    return -39;
            }
        }*/
        
            
                
        
   

       /* if (Core.Me.Level < 88 && !GNBSpells.血壤.GetSpell().IsReadyWithCanCast())
        {
            if (Core.Resolve<MemApiBuff>().HasMyAura(Core.Me, GNBBuffs.无情))
            {
                if ((Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙) != GNBSpells.烈牙 ||  GNBSpells.烈牙.CoolDownInGCDs(3)) || GNBSpells.烈牙.GetSpell().IsReadyWithCanCast()) return -2;
            }
            else
            {
                if (Core.Resolve<JobApi_GunBreaker>().Ammo < 2 ||
               Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.残暴弹) return -3;
            }
        }
        else if (Core.Me.Level >= 88 && !GNBSpells.血壤.GetSpell().IsReadyWithCanCast())
        {
            if(Core.Resolve<JobApi_GunBreaker>().Ammo == 3 && Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹 && GNBSpells.无情.CoolDownInGCDs(1) && !GNBSpells.无情.GetSpell().IsReadyWithCanCast()) return 8;
            if (Core.Resolve<MemApiBuff>().HasMyAura(Core.Me, GNBBuffs.无情))
            {
                if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙) != GNBSpells.烈牙 || GNBSpells.烈牙.CoolDownInGCDs(3) || GNBSpells.倍攻.CoolDownInGCDs(3)) return -2;
            }
            else
            {
                if (Core.Resolve<JobApi_GunBreaker>().Ammo < 3 || Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.残暴弹) return -3;
            }
        }
        else if (Core.Me.Level < 88 && GNBSpells.血壤.GetSpell().IsReadyWithCanCast())
        {
            if (!GNBSpells.烈牙.CoolDownInGCDs(Core.Resolve<JobApi_GunBreaker>().Ammo)) return 1;
        }
        else if (Core.Me.Level == 90 && GNBSpells.血壤.GetSpell().IsReadyWithCanCast())
        {
            if (!GNBSpells.倍攻.CoolDownInGCDs(Core.Resolve<JobApi_GunBreaker>().Ammo)) return 1;
        }*/
        
        //if (GNBSpells.无情.CoolDownInGCDs(1)) return -1;
        //if (GunbreakerRotationEntry.QT.GetQt("自动拉怪")) return -5;



        return -1;
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