using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;
using Nagomi.PCT;

namespace Nagomi.PCT.GCD;

public class PCTGCD_天星 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (Core.Me.HasAura(PCTBuffs.天星棱光预备) && PCTSpells.天星棱光.IsUnlockWithCDCheck())
        {
            return 0;
        }

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.天星棱光).GetSpell());
    }

}