using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;

namespace Nagomi.PVPMCH.GCD;

public class GCD热冲击 : ISlotResolver
{
    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -999;
        }

        if ( !PVPMCHSpells.热冲击.IsReady())
        {
            return -1;
            
        }
        if (!Core.Me.HasAura(PVPMCHBuffs.过热))
        {
            return -2;
            
        }
        if (Core.Me.HasAura(PVPMCHBuffs.防御) ) //是否有buff
        {
            return -66;
        }
        if (PVPHelper.通用距离检查(25))
        {
            return -3;
        }



        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.热冲击))
        {
            return -7;
        }

        return 1;
            
    }

    public void Build(Slot slot)
    {
        if (PvPSettings.Instance.技能自动选中)
        {
            if (PvPSettings.Instance.最合适目标)
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.热冲击, PVPHelper.Get最合适目标(25)));
            else
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.热冲击, PVPHelper.Get最近目标()));
        }
        else
            slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.热冲击, GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me)));
    }
}