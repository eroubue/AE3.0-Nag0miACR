using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     星位合图
/// </summary>
public class AST_Ability_Synastry : BaseSlotResolver
{
    public static AST_Ability_Synastry Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Synastry;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal)) return -100;

        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CanHeal() && (!r.IsTank() ? r.CurrentHpPercent() <= 0.6 : r.CurrentHpPercent() <= 0.5)
            orderby r.IsTank() descending, r.CurrentHpPercent()
            select r).FirstOrDefault();

        if (t == null) return -1;

        if (AST_View.UI.GetQt(AST_QT_Key.OnlyHealTank) && Target != null && !Target.IsTank()) return -101;

        Target = t;

        return 0;
    }
}