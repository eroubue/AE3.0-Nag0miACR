using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

/// <summary>
///     康复
/// </summary>
public class AST_GCD_Esuna : BaseSlotResolver
{
    public static AST_GCD_Esuna Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Esuna;
    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseEsuna)) return PreCheckCode.NotQT;

        if (!MsSpellHelper.CanCastSpell()) return PreCheckCode.NotCanCast;

        Target = (from VAR in PartyHelper.CastableAlliesWithin30
            where VAR.HasCanDispel() && VAR.CurrentHpPercent() > 0.5
            select VAR).FirstOrDefault();

        if (Target == null) return -1;
        return 0;
    }
}