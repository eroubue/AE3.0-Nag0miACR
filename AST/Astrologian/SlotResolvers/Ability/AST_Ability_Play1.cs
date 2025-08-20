using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.Setting;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

public class AST_Ability_Play1 : BaseSlotResolver
{
    public static AST_Ability_Play1 Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Play1.AdjustActionID();
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (SpellId.RecentlyUsed()) return -9;

        var card = Core.Resolve<JobApi_Astrologian>().DrawnCards[0];

        if (card == CardType.NONE) return -10;

        if (!AST_BattleData.Instance.IsBossBattle && AST_BattleData.Instance.BattleRemainingTime < 15000) return -110;

        if (card == CardType.BALANCE)
        {
            var t = GetBalanceTarget();

            if (t != null)
            {
                Target = t;
                if (!Data.IsInHighEndDuty) return 1;

                if (Core.Me.HasLocalPlayerAura(AurasDefine.Divination)) return 11;

                if (AI.Instance.BattleData.CurrBattleTimeInMs < 10000)
                    if (!GCDHelper.Is2ndAbilityTime())
                        return 10;

                if ((Core.Me.HasAura(AurasDefine.Lightspeed) || Core.Me.HasAura(AurasDefine.Swiftcast)) &&
                    SpellsDefine.Divination.GetSpell().Cooldown.TotalMilliseconds + GCDHelper.GetElapsedGCD() > 1000 &&
                    SpellsDefine.Divination.GetSpell().Cooldown.TotalMilliseconds + GCDHelper.GetElapsedGCD() < 2000)
                    return 12;
            }
        }

        if (card == CardType.SPEAR)
        {
            var t = GetSpearTarget();

            if (t != null)
            {
                Target = t;
                if (!Data.IsInHighEndDuty) return 1;

                if (Core.Me.HasLocalPlayerAura(AurasDefine.Divination)) return 11;
            }
        }


        return -1;
    }

    public static IBattleChara GetBalanceTarget()
    {
        IBattleChara target = null;
        if (AST_Settings.Instance.BalanceTargetName != "" || AST_Settings.Instance.BalanceTargetName != "优先级目标")
            target = PartyHelper.Party.FirstOrDefault(r =>
                r.Name.ToString() == AST_Settings.Instance.BalanceTargetName);

        if (target != null && target.IsValid() && target.DistanceToPlayer() <= 30) return target;

        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CurrentHp != 0
            orderby AST_Settings.Instance.BalanceCardTargetList.IndexOf(r.CurrentJob()) == -1
                    ? 1000
                    : AST_Settings.Instance.BalanceCardTargetList.IndexOf(r.CurrentJob()), r.IsDps() descending,
                r.IsMelee() descending
            select r).FirstOrDefault();
        if (t != null && t.IsValid()) return t;
        return null;
    }

    public static IBattleChara GetSpearTarget()
    {
        IBattleChara target = null;
        if (AST_Settings.Instance.SpareTargetName != "" || AST_Settings.Instance.SpareTargetName != "优先级目标")
            target = PartyHelper.Party.FirstOrDefault(r =>
                r.Name.ToString() == AST_Settings.Instance.SpareTargetName);

        if (target != null && target.IsValid() && target.DistanceToPlayer() <= 30) return target;

        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CurrentHp != 0
            orderby AST_Settings.Instance.SpareCardTargetList.IndexOf(r.CurrentJob()) == -1
                    ? 1000
                    : AST_Settings.Instance.SpareCardTargetList.IndexOf(r.CurrentJob()), r.IsDps() descending,
                r.IsCaster() descending, r.IsRanged() descending
            select r).FirstOrDefault();
        if (t != null && t.IsValid()) return t;
        return null;
    }
}