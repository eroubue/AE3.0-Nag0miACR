using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     星天交错
/// </summary>
public class AST_Ability_CelestialIntersection : BaseSlotResolver
{
    public static AST_Ability_CelestialIntersection Instance { get; } = new();
    public override uint SpellId => SpellsDefine.CelestialIntersection;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        if (!Core.Me.InCombat()) return PreCheckCode.NotInCombat;

        if (SpellId.RecentlyUsed()) return PreCheckCode.RecentlyUsed;

        if (Core.Me.CurrentHpPercent() < 0.5)
        {
            Target = Core.Me;
            return 1;
        }

        var t = PartyHelper.CastableTanks.Where(r => r.DistanceToPlayer() < 30)
            .OrderBy(r => r.CurrentHpPercent())
            .FirstOrDefault();

        if (t == null || !t.IsValid()) return -2;

        if (t.CurrentHpPercent() > 0.9) return -3;

        if (t.HasLocalPlayerAura(AurasDefine.CelestialIntersection)) return -4;

        Target = t;
        return PreCheckCode.Success;
    }
}