using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.依赖.Helper;

namespace Nagomi.PVPMCH.GCD;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi;
using Nagomi.PVPMCH.依赖;

public class GCD回转飞锯 : ISlotResolver
{
    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -999;
        }
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.回转飞锯)) //QT是否开启
        {
            return -4;
        }

        if (PVPHelper.通用距离检查(25))
        {
            return -3;
        }
        if (!PVPMCHSpells.回转飞锯.IsReady())
        {
            return -2;
        }
        if (!Core.Me.HasAura(PVPMCHBuffs.回转飞锯启动))
        {
            return -2;
            
        }

        if (PVPMCHSettings.Instance.回转飞锯分析 && !Core.Me.HasAura(PVPMCHBuffs.分析))
        {

            return -23;
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
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.回转飞锯, PVPHelper.Get最合适目标(25)));
            else
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.回转飞锯, PVPHelper.Get最近目标()));
        }
        else
            slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.回转飞锯, GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me)));
    }
}