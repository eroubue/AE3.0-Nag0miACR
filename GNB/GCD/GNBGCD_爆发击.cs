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
        var aoeCount = TargetHelper.GetNearbyEnemyCount( 5);
        if (aoeCount >= 2 && GNBSpells.命运之环.GetSpell().IsReadyWithCanCast()&&QT.QTGET(QTKey.AOE)) return -6;
        if (QT.QTGET(QTKey.倾泻爆发)&&Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1&&!QT.QTGET(QTKey.仅使用爆发击卸除子弹))
        {
            return 100;
        }
        if (QT.QTGET(QTKey.仅使用爆发击卸除子弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1) return 1;
        // 优先判断子弹连和狮心连
        var gnashingFangSlot = new GNBGCD_子弹连();
        if (gnashingFangSlot.Check() >= 0)
            return -7; // 子弹连优先，爆发击不打
        var lionHeartSlot = new GNBGCD_狮心连();
        if (lionHeartSlot.Check() >= 0)
            return -8; // 狮心连优先，爆发击不打
        //if (Core.Resolve<JobApi_GunBreaker>().Ammo == 1 &&Helper.技能冷却能否在buff剩余时间内结束(GNBBuffs.Medicated, GNBSpells.烈牙)
          // &&!Core.Me.HasAura(GNBBuffs.无情)) return -45;//在无情过了但爆发药内有一颗子弹而且子弹连能转好时优先子弹连
          if (!Core.Me.HasAura(GNBBuffs.无情)&&Helper.技能0dot6s内是否用过(GNBSpells.无情)&&QT.QTGET(QTKey.落地无情)) return -21;
        if (!Core.Me.HasAura(GNBBuffs.无情) && Core.Me.HasAura(GNBBuffs.Medicated) &&
            GNBSpells.无情.GetSpell().IsReadyWithCanCast()) return -21;//吃药还没放无情不打
        if(Core.Me.HasAura(GNBBuffs.Medicated)&&Core.Me.HasAura(GNBBuffs.无情))return 1;//无情+药打
        if (Core.Me.HasAura(GNBBuffs.Medicated) && Helper.技能是否刚使用过(GNBSpells.无情, 30000)) return 2;//无情没了药还在就打
       
        if (Core.Me.Level < 88) //两颗子弹
        {
            
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 2 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹) return 2;//溢出时打
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 2 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.恶魔切) return 2;//溢出时打
            if (Core.Me.HasAura(GNBBuffs.无情))
            {
                // 子弹连无情内能转好，在没打子弹连之前都保留一个子弹，打完子弹连再打爆发击
                if (Core.Resolve<JobApi_GunBreaker>().Ammo == 1 && 
                    Helper.技能冷却能否在buff剩余时间内结束(GNBBuffs.无情, GNBSpells.烈牙))
                {
                    return -45; // 保留子弹，不打爆发击
                }
            }
            
        }
        if (Core.Me.Level >= 88)//三颗子弹
        {
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹) return 2;//溢出时打
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.恶魔切) return 2;//溢出时打
            if (Helper.技能22s内是否用过(GNBSpells.无情)) return 13;//无情里填充
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3 &&
                Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹&&!SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)) return 22;//120后填充期
            if (QT.QTGET(QTKey.二弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo != 2 && SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)&&SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 8)) return 23;//120卸子弹
            if (QT.QTGET(QTKey.二弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo == 2 && SpellExtension.CoolDownInGCDs(GNBSpells.无情, 1)&&SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 8)&&(Helper.上一个连击技能()==16139||Helper.上一个连击技能()==16141)) return 25;//120转好无情前如果无情前面1g能打成三弹，提前卸一个爆发击确保二弹进无情
            if (QT.QTGET(QTKey.零弹)&&Core.Resolve<JobApi_GunBreaker>().Ammo != 0 && SpellExtension.CoolDownInGCDs(GNBSpells.无情, 4)&&SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 8)) return 23;
            if (QT.QTGET(QTKey.零弹) && SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 7)) return 1;//零弹120，打完120前的子弹连后有就打
            
            // 无情buff内，如果子弹连能转好，保留子弹不打爆发击
            if (Core.Me.HasAura(GNBBuffs.无情))
            {
                if (Core.Resolve<JobApi_GunBreaker>().Ammo == 1 && 
                    Helper.技能冷却能否在buff剩余时间内结束(GNBBuffs.无情, GNBSpells.烈牙))
                {
                    return -45; // 保留子弹，不打爆发击
                }
            }
        }
        

        

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