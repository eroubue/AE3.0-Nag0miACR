using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;
using Nagomi.PCT;
using Nagomi.utils.Helper;

namespace Nagomi.PCT.GCD;

public class PCTGCD_锤子 : ISlotResolver
{
    public int Check()
    {
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (!Core.Me.HasAura(PCTBuffs.重锤连击) || !Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.重锤敲章).IsUnlockWithCDCheck() || !QT.QTGET(QTKey.锤连击))
        {
            return -1;
        }

        if (QT.QTGET(QTKey.保留1层锤) && Helper.目标的指定BUFF层数(Core.Me,PCTBuffs.重锤连击) == 1)
        {
            return -2;
        }

        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.重锤敲章).GetSpell());
    }

}