using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Millusion.Define;
using Millusion.Enum;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     抽卡
/// </summary>
public class AST_Ability_Draw : BaseSlotResolver
{
    public static AST_Ability_Draw Instance { get; } = new();
    public override uint SpellId => SpellsDefine.AstralDraw.AdjustActionID();
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (SpellId.RecentlyUsed()) return PreCheckCode.RecentlyUsed;

        if (SpellsDefine.MinorArcana.IsUnlock() &&
            Core.Resolve<JobApi_Astrologian>().DrawnCrownCard == CardType.LORD) return -2;

        return PreCheckCode.Success;
    }
}