using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;

namespace Nagomi.PVPMCH.GCD;

public class GCD霰弹枪 : ISlotResolver
{
    public int Check()
    {
        if (!PvP.PVPApi.PVPHelper.CanActive())
        {
            return -999;
        }
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.霰弹枪)) //QT是否开启
        {
            return -4;
        }

        if (PVPHelper.通用距离检查(12))
        {
            return -3;
        }
        if (!PVPMCHSpells.霰弹枪.IsReady())
        {
            return -2;
        }
        if ( Core.Me.HasAura(PVPMCHBuffs.防御) ) //是否有buff
        {
            return -66;
        }

        return 1;

    }

    public void Build(Slot slot)
    {
        if (PvPSettings.Instance.技能自动选中)
        {
            if (PvPSettings.Instance.最合适目标)
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.霰弹枪, PVPHelper.Get最合适目标(12)));
            else
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.霰弹枪, PVPHelper.Get最近目标()));
        }
        else
            slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.霰弹枪, GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me)));
    }
}