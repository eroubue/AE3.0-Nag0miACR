using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;

namespace Nagomi.PCT.GCD;

public class PCTGCD_彩虹 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.HasAura(PCTBuffs.彩虹点滴效果提高) && PCTSpells.彩虹点滴.IsReady())
        {
            return 0;
        }

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.彩虹点滴).GetSpell());
    }

}