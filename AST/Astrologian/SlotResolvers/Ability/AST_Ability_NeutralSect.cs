using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
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
///     中间学派
/// </summary>
public class AST_Ability_NeutralSect : BaseSlotResolver
{
    public static AST_Ability_NeutralSect Instance { get; } = new();
    public override uint SpellId => SpellsDefine.NeutralSect;
    public override SlotMode Mode => SlotMode.OffGcd;


    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseRangeHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        if (!AST_View.UI.GetQt(AST_QT_Key.UseNeutralSect)) return PreCheckCode.NotQT;

        if (Core.Me.HasLocalPlayerAura(AurasDefine.Macrocosmos)) return -2;

        var n = TargetMgr.Instance.EnemysIn25.Count(r =>
            r.Value.IsBoss() && r.Value.IsInEnemiesList() && TargetHelper.TargercastingIsbossaoe(r.Value, 3));

        if (n > 0) return 2;

        if (MsPartyHelper.GetPartyCountHpLessThanIn15(0.75f, true) < 2)
            return -1;

        return 1;
    }
}