using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     小奥秘卡
/// </summary>
public class AST_Ability_MinorArcana : BaseSlotResolver
{
    public static AST_Ability_MinorArcana Instance { get; } = new();
    public override uint SpellId => SpellsDefine.MinorArcana.AdjustActionID();
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (SpellId.RecentlyUsed()) return -9;

        var card = Core.Resolve<JobApi_Astrologian>().DrawnCrownCard;

        if (card == CardType.LORD)
        {
            if (TargetHelper.GetNearbyEnemyCount(20) == 0) return -2;

            if (SpellsDefine.Divination.IsUnlock() && Core.Me.HasLocalPlayerAura(AurasDefine.Divination) &&
                Core.Me.GetCurrTarget().IsBoss() && Data.IsInHighEndDuty) return 1;

            if (!SpellsDefine.Divination.IsUnlock() || !Core.Me.GetCurrTarget().IsBoss() ||
                !Data.IsInHighEndDuty) return 2;
        }

        if (card == CardType.LADY)
        {
            if (PartyHelper.CastableAlliesWithin20.Count(r => r.CurrentHpPercent() < 0.85) > 2) return 3;

            if (TargetHelper.GetNearbyEnemyCount(20) >= 3 &&
                PartyHelper.CastableTanks.Any(r => r.DistanceToPlayer() < 20 && r.CurrentHpPercent() < 0.75)) return 4;
        }

        return -1;
    }
}