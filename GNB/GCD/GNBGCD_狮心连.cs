using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.GNB.utils;

#nullable enable
namespace Nagomi.GNB.GCD
{
  public class GNBGCD_狮心连 : ISlotResolver
  {
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    

    public int Check()
    {
      if (!QT.QTGET(QTKey.爆发))
      {
        return -10;
      }
      if (!QT.QTGET(QTKey.狮心连)) { return -10; }
      if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
      if (!Core.Me.HasAura(GNBBuffs.无情)&&Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.崛起之心) == GNBSpells.崛起之心) return -41;
      if (!Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.崛起之心).GetSpell().IsReadyWithCanCast()) return -6;
      if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.崛起之心) == GNBSpells.支配之心 || Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.崛起之心) == GNBSpells.终结之心)
        return 5;//继续打
      if (QT.QTGET(QTKey.子弹连)&&Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙).GetSpell().IsReadyWithCanCast()&&Core.Resolve<JobApi_GunBreaker>().Ammo!=0) return -60;
      if (GNBSettings.Instance.额外技能距离!=0&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
        return -12;
      if (GNBSettings.Instance.额外技能距离==0&&Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
          SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
      if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙) == GNBSpells.猛兽爪 || Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.烈牙) == GNBSpells.凶禽爪)
        return -25;//子弹连中不打
      if (!Core.Me.HasAura(GNBBuffs.无情) && Core.Me.HasAura(GNBBuffs.Medicated) &&
          GNBSpells.无情.GetSpell().IsReadyWithCanCast()) return -21;//吃药还没放无情不打
      
      
      /* if (SpellExtension.IsReadyWithCanCast(GNBSpells.音速破.GetSpell()) ||
          SpellExtension.IsUnlockWithCDCheck(GNBSpells.倍攻) 
          ||SpellExtension.IsUnlockWithCDCheck(GNBSpells.烈牙))
      {
        return -60; // 所有技能都在冷却中
      }*/

      return 1;


    }

    public void Build(Slot slot)
    {
      slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.崛起之心.GetSpell().Id).GetSpell());
    }
  }
}
