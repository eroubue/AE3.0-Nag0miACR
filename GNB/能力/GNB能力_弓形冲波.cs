using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;

using Nagomi.GNB.utils;

namespace Nagomi.GNB.能力;

public class GNB能力_弓形冲波 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    public int Check()
    {
        if (!GNBSpells.弓形冲波.GetSpell().IsReadyWithCanCast())
        {
            return -66;
        }
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
        if (!QT.QTGET(QTKey.弓形))
        {
            return -10;
        }
        if(!Core.Me.HasAura(GNBBuffs.无情)&&!QT.QTGET(QTKey.弓形冲波允许错开无情))return -88;
        if (Helper.GCD剩余时间() <= 600) return -26;
        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            5) return -5;
        if (aoeCount < 3&&QT.QTGET(QTKey.小于3目标时不用弓形)) return -4;
        
        
        

        return 0;
    }


    public void Build(Slot slot)
    {
        slot.Add(GNBSpells.弓形冲波.GetSpell());
    }
    
}