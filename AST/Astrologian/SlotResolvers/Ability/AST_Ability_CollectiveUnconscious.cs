using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Define;
using Millusion.Enum;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     命运之轮
/// </summary>
public class AST_Ability_CollectiveUnconscious : BaseSlotResolver
{
    public static AST_Ability_CollectiveUnconscious Instance { get; } = new();
    public override uint SpellId => SpellsDefine.CollectiveUnconscious;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseRangeHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        var n = TargetMgr.Instance.EnemysIn25.Count(r =>
            r.Value.IsBoss() && TargetHelper.TargercastingIsbossaoe(r.Value, 3));

        if (n == 0) return -2;

        return PreCheckCode.Success;
    }
}