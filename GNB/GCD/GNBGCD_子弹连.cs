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


    public int Check()
    {
        
        
        if (QT.QTGET(QTKey.停手)) { return -100; }
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (GNBSettings.Instance.额外技能距离!=0&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
        if (GNBSettings.Instance.额外技能距离==0&&Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        if (!SpellExtension.IsReadyWithCanCast(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙).GetSpell()))
            return -5;
        if (!QT.QTGET(QTKey.爆发))
        {
            return -10;
        }
        if (QT.QTGET(QTKey.倾泻爆发)&& Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1&&!QT.QTGET(QTKey.仅使用爆发击卸除子弹))
            return 10;
        if (QT.QTGET(QTKey.仅使用爆发击卸除子弹)) return -20;
        
        if (!QT.QTGET(QTKey.子弹连)) { return -10; }
        
        
        if (SpellExtension.IsReadyWithCanCast(GNBSpells.倍攻.GetSpell())&&QT.QTGET(QTKey.倍攻))//优先倍攻
            return -3;
        if (Core.Resolve<MemApiSpell>().CheckActionChange(36937U) == 36938U || Core.Resolve<MemApiSpell>().CheckActionChange(36937U) == 36939U)
            return -25;//正在狮心连里不打子弹连
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙) == GNBSpells.猛兽爪 || Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙) == GNBSpells.凶禽爪)
            return 5;//继续打
        
        
        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        if (Core.Me.Level >= 72 && aoeCount >= 3&& GNBSpells.命运之环.GetSpell().IsReadyWithCanCast()&&QT.QTGET(QTKey.AOE)&&QT.QTGET(QTKey.命运之环)) //敌人数量大于3不打子弹连
            return -9;
        if (SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)&&QT.QTGET(QTKey.无情)) return -1;
        
      
      if (QT.QTGET(QTKey.零弹)&&SpellExtension.IsReadyWithCanCast(GNBSpells.音速破.GetSpell())&&QT.QTGET(QTKey.dot))
        return -5;
      if (!Core.Me.HasAura(GNBBuffs.无情) && Core.Me.HasAura(GNBBuffs.Medicated) &&
          GNBSpells.无情.GetSpell().IsReadyWithCanCast()) return -21;//吃药还没放无情不打
     
     

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙).GetSpell());
    }


}