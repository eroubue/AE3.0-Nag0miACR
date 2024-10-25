using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.能力;

public class GNB能力_血壤 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    public int Check()
    {
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!GNBSpells.血壤.IsReady())
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
        
        if (Core.Resolve<JobApi_GunBreaker>().Ammo > 0) return -1;//晶壤数量大于0

        return 0;
    }


    public void Build(Slot slot)
    {
        slot.Add(GNBSpells.血壤.GetSpell());

    }
    
}