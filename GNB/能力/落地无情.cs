using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.能力;

public class 落地无情 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public int Check()
    {
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!GNBSpells.无情.GetSpell().IsReadyWithCanCast())
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
        if (!QT.QTGET(QTKey.无情))
        {
            return -10;
        }
        if (!QT.QTGET(QTKey.落地无情))
        {
            return -1;
        }
        
        return 1;
            
    }
    public void Build(Slot slot)
    {
        slot.Insert(new SlotAction(GNBSpells.无情.GetSpell()));
    }
}
