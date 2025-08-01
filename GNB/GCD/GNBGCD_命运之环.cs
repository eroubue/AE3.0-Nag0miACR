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

public class GNBGCD_命运之环 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (QT.QTGET(QTKey.停手)) { return -100; }
        if (!GNBSpells.命运之环.GetSpell().IsReadyWithCanCast()) return -2;
        if (!QT.QTGET(QTKey.AOE)) return -5;
        if (!QT.QTGET(QTKey.命运之环)) return -5;
        
        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        if (aoeCount < 2) return -4;
        if (QT.QTGET(QTKey.倾泻爆发)&&Core.Resolve<JobApi_GunBreaker>().Ammo >= (byte) GNBSettings.Instance.保留子弹数+1) return 1;
        if (!Core.Me.HasAura(GNBBuffs.无情) && !Core.Me.HasAura(GNBBuffs.Medicated))//都不存在溢出再打
        { 
            if (Core.Me.Level < 88 && Core.Resolve<JobApi_GunBreaker>().Ammo < 2 &&
                (Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.残暴弹
                 || Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.恶魔切)) return -3;
            if (Core.Me.Level >= 88 && Core.Resolve<JobApi_GunBreaker>().Ammo < 3 &&
                (Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.残暴弹
                 || Core.Resolve<MemApiSpell>().GetLastComboSpellId() != GNBSpells.恶魔切)) return -3;
        }
        
        if (QT.QTGET(QTKey.仅使用爆发击卸除子弹)) return -20;
        if (!Core.Me.HasAura(GNBBuffs.无情) && Core.Me.HasAura(GNBBuffs.Medicated) &&
            GNBSpells.无情.GetSpell().IsReadyWithCanCast()) return -21;//吃药还没放无情不打

        
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(GNBSpells.命运之环.GetSpell());
    }

}