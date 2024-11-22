using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;

namespace Nagomi.SMN.GCD;

public class SMNGCD_BASE : ISlotResolver
{
    public int Check()
    {
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.天星棱光).GetSpell());
    }

}