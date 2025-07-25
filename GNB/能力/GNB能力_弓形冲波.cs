using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

using Nagomi.GNB.utils;

namespace Nagomi.GNB.能力;

public class GNB能力_弓形冲波 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    public int Check()
    {
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!GNBSpells.弓形冲波.GetSpell().IsReadyWithCanCast())
        {
            return -66;
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
        if(!Core.Me.HasAura(GNBBuffs.无情))return -88;
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
        if (aoeCount < 1) return -4;
        

        return 0;
    }


    public void Build(Slot slot)
    {
        slot.Add(GNBSpells.弓形冲波.GetSpell());
    }
    
}