using AEAssist.CombatRoutine.Module;
using AEAssist;
using AEAssist.Helper;
using AEAssist.JobApi;
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
        
        if (QT.QTGET(QTKey.倾泻爆发))
        {
            return 100;
        }
        if (!GNBSpells.弓形冲波.IsReady())
        {
            return -66;
        }
        if (!QT.QTGET(QTKey.弓形))
        {
            return -10;
        }
        

        return 0;
    }


    public void Build(Slot slot)
    {
        slot.Add(GNBSpells.弓形冲波.GetSpell());
    }
    
}