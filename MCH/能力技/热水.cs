using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;


namespace Nagomi.PVPMCH.能力;

public class 药 : ISlotResolver
{
    public uint 技能药 = 29711;

    public SlotMode SlotMode { get; } = (SlotMode) 2;

    public int Check()
    {
        return -1;

    }

    public void Build(Slot slot) => slot.Add(PVPHelper.不等服务器Spell(29711U, (IBattleChara) Core.Me));
}