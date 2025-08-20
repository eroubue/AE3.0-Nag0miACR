using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     擢升
/// </summary>
public class AST_Ability_Exaltation : BaseSlotResolver
{
    public static AST_Ability_Exaltation Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Exaltation;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        var bt = Core.Me.GetCurrTarget();
        if (bt != null && bt.IsValid() && bt.IsBoss() && TargetHelper.TargercastingIsDeathSentence(bt, 6000))
        {
            var tt = bt.GetCurrTarget();
            if (tt != null && tt.IsValid() && tt.DistanceToPlayer() < 30)
            {
                Target = tt;
                return 1;
            }
        }

        var t = PartyHelper.CastableTanks.Where(r => r.DistanceToPlayer() < 30)
            .OrderBy(r => r.CurrentHpPercent())
            .FirstOrDefault();
        if (t != null && t.IsValid() && TargetHelper.GetNearbyEnemyCount(t, 30, 5) >= 2 && t.CurrentHpPercent() < 0.75)
        {
            Target = t;
            return 2;
        }

        if (Core.Me.CurrentHpPercent() < 0.5)
        {
            Target = Core.Me;
            return 3;
        }

        return -111;
    }
}