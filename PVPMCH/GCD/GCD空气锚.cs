using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.依赖.Helper;

namespace Nagomi.PVPMCH.GCD;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Nagomi.PVPMCH.依赖;

public class GCD空气锚 : ISlotResolver
{
    
    

        public int Check()
        {
            if (!PVPHelper.CanActive())
            {
                return -999;
            }

            if (!PVPMCHRotationEntry.QT.GetQt(QTKey.空气锚))
            {
                return -4;
            }
          
            if (PVPHelper.通用距离检查(25))
            {
                return -3;
            }  
            if (!PVPMCHSpells.空气锚.IsReady())
            {
                return -2;
            }
            if ( Core.Me.HasAura(PVPMCHBuffs.防御) ) //是否有buff
            {
                return -66;
            }
            if (PVPMCHSettings.Instance.空气锚分析 && !Core.Me.HasAura(PVPMCHBuffs.分析))
            {

                return -23;
            }
            if (!Core.Me.HasAura(PVPMCHBuffs.空气锚启动) ) //是否有buff
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
                    slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.空气锚, PVPHelper.Get最合适目标(25)));
                else
                    slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.空气锚, PVPHelper.Get最近目标()));
            }
            else
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.空气锚, GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me)));
        }
    
}