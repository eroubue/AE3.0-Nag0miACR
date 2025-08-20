using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Define;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     天星冲日
/// </summary>
public class AST_Ability_CelestialOpposition : BaseSlotResolver
{
    public static AST_Ability_CelestialOpposition Instance { get; } = new();
    public override uint SpellId => SpellsDefine.CelestialOpposition;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseRangeHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal)) return -100;

        if (TargetHelper.GetNearbyEnemyCount(15) >= 3 && (from r in PartyHelper.CastableTanks
                where r.DistanceToPlayer() < 15 && r.CurrentHpPercent() < 0.8
                select r).Any())
            return 2;

        if (MsPartyHelper.GetPartyCountHpLessThanIn15(0.8f, true) < 2)
            return -1;

        return PreCheckCode.Success;
    }
}