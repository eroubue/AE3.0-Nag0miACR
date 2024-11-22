using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;

namespace Nagomi.PVPMCH.能力;

public class 净化 : ISlotResolver
{
    public uint 技能净化 = 20956;

    public SlotMode SlotMode { get; } = (SlotMode) 2;

    public int Check()
    {
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.自动净化))
        {
            return -7;
        }
        if (!SpellExtension.IsReady(this.技能净化))
            return -2;
        return GameObjectExtension.HasAura((IBattleChara) Core.Me, 1347U, 0) || GameObjectExtension.HasAura((IBattleChara) Core.Me, 1343U, 0) ? 0 : -3;
    }

    public void Build(Slot slot) => slot.Add(PVPHelper.不等服务器Spell(29056U, (IBattleChara) Core.Me));
}
