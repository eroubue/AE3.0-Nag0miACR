using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     先天禀赋
/// </summary>
public class AST_Ability_EssentialDignity : BaseSlotResolver
{
    public static AST_Ability_EssentialDignity Instance { get; } = new();
    public override uint SpellId => SpellsDefine.EssentialDignity;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        if (SpellId.RecentlyUsed()) return -5;

        if (AST_Settings.Instance.ForceHealTarget)
        {
            var c = Core.Me.GetCurrTarget();
            if (c != null && c.IsValid() && !c.IsEnemy())
            {
                if (c.CurrentHpPercent() < 0.6)
                {
                    Target = c;
                    return 666;
                }
            }
        }

        var hp = 0.6;

        if (SpellId.GetSpell().Charges > 2) hp += +0.1;

        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CanHeal() && r.CurrentHpPercent() <= hp
            orderby r.IsTank() descending, r.CurrentHpPercent()
            select r).FirstOrDefault();
        if (t == null || !t.IsValid()) return -2;

        if (AST_View.UI.GetQt(AST_QT_Key.OnlyHealTank) && Target != null && !Target.IsTank()) return -101;

        Target = t;

        return 0;
    }
}