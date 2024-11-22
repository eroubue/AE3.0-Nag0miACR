using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH.依赖;

namespace Nagomi.PVPMCH.GCD;

public class GCD钻头 : ISlotResolver
{
    public int Check()
    {
        if (!PVPHelper.CanActive())
        {
            return -999;
        }
        if (!PVPMCHRotationEntry.QT.GetQt(QTKey.钻头)) //QT是否开启
        {
            return -4;
        }
        if (!Core.Me.HasAura(PVPMCHBuffs.钻头启动))
        {
            return -2;
            
        }

        if (PVPHelper.通用距离检查(25))
        {
            return -3;
        }
        if (!PVPMCHSpells.钻头.IsReady())
        {
            return -2;
        }

        if (PVPMCHSettings.Instance.钻头分析 && !Core.Me.HasAura(PVPMCHBuffs.分析))
        {

            return -23;
        }
        if ( Core.Me.HasAura(PVPMCHBuffs.防御) ) //是否有buff
        {
            return -66;
        }

        return 1;

    }
    public static IBattleChara? Get钻头最近目标()
    {
        if (!LocalPlayerExtension.IsPvP(Core.Me))
            return (IBattleChara)Core.Me;
        Dictionary<uint, IBattleChara>.ValueCollection values = TargetMgr.Instance.EnemysIn25.Values;
        IBattleChara ibattleChara1 = (IBattleChara)null;
        float num = float.MaxValue;
        foreach (IBattleChara ibattleChara2 in values)
        {
            if (((IGameObject)ibattleChara2).IsTargetable)
            {
                float player = GameObjectExtension.DistanceToPlayer((IGameObject)ibattleChara2);
                if ((double)player < (double)num)
                {
                    ibattleChara1 = ibattleChara2;
                    num = player;
                }
            }
        }

        return (IBattleChara)((object)ibattleChara1 ?? (object)Core.Me);
    }
    public static IBattleChara? Get钻头最合适目标(int 技能距离)
    {
        if (!LocalPlayerExtension.IsPvP(Core.Me))
            return (IBattleChara)Core.Me;
        Dictionary<uint, IBattleChara>.ValueCollection values = TargetMgr.Instance.EnemysIn25.Values;
        IBattleChara ibattleChara1 = (IBattleChara)null;
        float num = float.MaxValue;
        foreach (IBattleChara ibattleChara2 in values)
        {
            if (((IGameObject)ibattleChara2).IsTargetable &&
                !GameObjectExtension.HasAura(ibattleChara2, 3039U, 0) &&
                !GameObjectExtension.HasAura(ibattleChara2, 2413U, 0) &&
                !GameObjectExtension.HasAura(ibattleChara2, 1301U, 0) &&
                (double)GameObjectExtension.DistanceToPlayer((IGameObject)ibattleChara2) <= (double)技能距离 &&
                (double)((ICharacter)ibattleChara2).CurrentHp < (double)num)
            {
                ibattleChara1 = ibattleChara2;
                num = (float)((ICharacter)ibattleChara2).CurrentHp;
            }
        }

        return (IBattleChara)((object)ibattleChara1 ?? (object)Core.Me);
    }

    public void Build(Slot slot)
    {
        if (PvPSettings.Instance.技能自动选中)
        {
            if (PvPSettings.Instance.最合适目标)
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.钻头, Get钻头最合适目标(25)));
            else
                slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.钻头, Get钻头最近目标()));
        }
        else
            slot.Add(PVPHelper.不等服务器Spell(PVPMCHSpells.钻头, GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me)));
    }
}