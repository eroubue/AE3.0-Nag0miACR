using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;
using PCT.utils.Helper;

namespace Nagomi.PVPMCH.能力;

public class 药 : ISlotResolver
{
    public uint 技能药 = 29711;

    public SlotMode SlotMode { get; } = (SlotMode) 2;

    public int Check()
    {
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.喝热水))
            return -9;
        if (!PVPHelper.CanActive())
            return -3;
        if (!SpellExtension.IsReady(this.技能药))
            return -2;
        if(Helper.自身蓝量<2500)return -1;
        return (double) GameObjectExtension.CurrentHpPercent((ICharacter) Core.Me) <= (double) PVPMCHSettings.Instance.药血量 / 100.0 ? 0 : -1;
    }

    public void Build(Slot slot) => slot.Add(PVPHelper.不等服务器Spell(29711U, (IBattleChara) Core.Me));
}