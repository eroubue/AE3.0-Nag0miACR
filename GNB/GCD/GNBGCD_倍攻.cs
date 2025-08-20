using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.GNB.utils;
using Nagomi.utils;

#nullable enable
namespace Nagomi.GNB.GCD
{
  public class GNBGCD_倍攻 : ISlotResolver
  {
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
      if(!QT.QTGET(QTKey.倍攻))return -18;
      if (!QT.QTGET(QTKey.爆发))
      {
        return -10;
      }
      if (!SpellExtension.IsReadyWithCanCast(GNBSpells.倍攻.GetSpell()))
        return -3;
      if (Core.Resolve<JobApi_GunBreaker>().Ammo < (byte) 1)
        return -2;
      if ((double) GameObjectExtension.Distance((IGameObject) Core.Me, (IGameObject) GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me), DistanceMode.IgnoreHitbox) > 5.0)
        return -4;
      if (SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)&&QT.QTGET(QTKey.无情)) return -1;
      if (GNBSpells.无情.CoolDownInGCDs(2) && !QT.QTGET(QTKey.倾泻爆发))
        return -6;
      if (SpellExtension.IsReadyWithCanCast(GNBSpells.音速破.GetSpell()) && QT.QTGET(QTKey.零弹))//零弹120优先dot
        return -7;
      if (QT.QTGET(QTKey.倾泻爆发)&& Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1&&!QT.QTGET(QTKey.仅使用爆发击卸除子弹))
        return 10;
      if (QT.QTGET(QTKey.仅使用爆发击卸除子弹)) return -20;
      
      if(!Core.Me.HasAura(GNBBuffs.无情))return -88;
      if (Helper.技能0dot6s内是否用过(GNBSpells.无情)&&QT.QTGET(QTKey.落地无情)&&!Core.Me.HasAura(GNBBuffs.无情)) return -21;
     
      
      return  0;
    }

    public void Build(Slot slot) => slot.Add(GNBSpells.倍攻.GetSpell());
  }
}
