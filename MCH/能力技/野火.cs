using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;
using Nagomi.依赖.Helper;

namespace Nagomi.PVPMCH.能力;

public class 野火 : ISlotResolver
{
    public SlotMode SlotMode { get; } = (SlotMode) 1;

    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -999;
        }
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.野火))
        {
            return -7;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -3;
        }
        if (PVPMCHSettings.Instance.过热野火&&!Helper.自身存在Buff(PVPMCHBuffs.过热))
        {
            return -9;
        }
        if (!PVPMCHSpells.野火.IsReady())
        {
            return -1;
        }

        return 0;
    }
    
    public void Build(Slot slot) => slot.Add(PVPHelper.不等服务器Spell(29409U, (IBattleChara) Core.Me));
}