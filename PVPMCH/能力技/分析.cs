using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using GNB.utils.Helper;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;

namespace Nagomi.PVPMCH.能力;

public class 分析 : ISlotResolver
{

    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -999;
        }
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.分析))
        {
            return -7;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -3;
        }
        if (Core.Me.HasAura(PVPMCHBuffs.分析))
        {
            return -2;
            
        }
        if (Helper.充能层数(PVPMCHSpells.分析)<1)
        {
            return -4;
            
        }
        if (Helper.GCD剩余时间()<500)
        {
            return -999;
        }
        

        if (!PVPMCHSpells.分析.IsReady())
        {
            return -1;
        }
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(PVPMCHSpells.分析),Core.Me) { WaitServerAcq = false })
        ;
    }
}