using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using System.Runtime;
using Nagomi.GNB.utils;
using Nagomi.SGE;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;


namespace Nagomi.SGE.GCD;
//复活的调用
public class GCD康复 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }

        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (!SGESpells.康复.IsUnlockWithCDCheck())
        {
            return -66;
        }
        //康复QT没开不康复
        if (!QT.QTGET(QTKey.康复)) return -2;
        //在移动不康复
        if (Core.Resolve<MemApiMove>().IsMoving() && !Core.Me.HasAura(SGEBuffs.即刻咏唱)) return -3;
        //有人身上有可以解除的buff了，准备康复
        if (PartyHelper.CastableAlliesWithin30.Any(agent => agent.HasCanDispel())) return 1;
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.Esuna, PartyHelper.CastableAlliesWithin30.FirstOrDefault(agent => agent.HasCanDispel())));
    }
}


public class GCD绝亚康复 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public static IBattleChara GetH1康复目标()
    {
        IBattleChara[] partyMembers =
        {
            Core.Me,
            PartyHelper.CastableTanks[0],
            PartyHelper.CastableTanks[1],
            PartyHelper.CastableDps[0],
            PartyHelper.CastableDps[1],
            PartyHelper.CastableDps[2],
            PartyHelper.CastableDps[3],
            PartyHelper.CastableDps[4],
            PartyHelper.CastableHealers[1],
        };

        foreach (var member in partyMembers)
        {
            if (member.HasAura(938u) || member.HasAura(700u))
            {
                return member;
            }
        }
        return Core.Me;
    }
    public static IBattleChara GetH2康复目标()
    {
        IBattleChara[] partyMembers =
        {
            Core.Me,
            PartyHelper.CastableDps[3],
            PartyHelper.CastableDps[2],
            PartyHelper.CastableDps[1],
            PartyHelper.CastableDps[0],
            PartyHelper.CastableTanks[1],
            PartyHelper.CastableTanks[0],
            PartyHelper.CastableHealers[0],
        };

        foreach (var member in partyMembers)
        {
            if (member.HasAura(938u) || member.HasAura(700u))
            {
                return member;
            }
        }
        return Core.Me;
    }
    public int Check()
    {
        if (Core.Resolve<MemApiZoneInfo>().GetCurrTerrId() != 887u)
        {
            return -100;
        }
        if (PartyHelper.CastableAlliesWithin30.Any(agent => agent.HasCanDispel()))
        {
            return 1;
        }
        return -1;
    }

    public void Build(Slot slot)
    {
        if (SGESettings.Instance.H1)
        {
            slot.Add(new Spell(SpellsDefine.Esuna, GetH1康复目标()));
        }
        else
        {
            slot.Add(new Spell(SpellsDefine.Esuna, GetH2康复目标()));
        }
    }

}