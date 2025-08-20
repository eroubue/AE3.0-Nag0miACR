using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

public class AST_Ability_Play2 : BaseSlotResolver
{
    public static AST_Ability_Play2 Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Play2.AdjustActionID();
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal)) return -100;

        if (SpellId.RecentlyUsed()) return -9;

        var card = Core.Resolve<JobApi_Astrologian>().DrawnCards[1];
        if (card == CardType.ARROW)
        {
            var t = GetArrowTarget();

            if (t != null)
            {
                Target = t;
                return 1;
            }
        }

        if (card == CardType.BOLE)
        {
            var t = GetBoleTarget();

            if (t == null) return -1;
            Target = t;
            return 2;
        }

        return -1;
    }

    public static IBattleChara GetArrowTarget()
    {
        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CurrentHp != 0 && r.CurrentHpPercent() < 0.9
            orderby r.IsTank() descending, r.CurrentHpPercent()
            select r).FirstOrDefault();
        if (t != null && t.IsValid()) return t;
        return null;
    }

    public static IBattleChara GetBoleTarget()
    {
        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CurrentHp != 0 && r.CurrentHpPercent() < 0.9
            orderby r.IsTank() descending, r.CurrentHpPercent()
            select r).FirstOrDefault();

        if (t != null && t.IsValid()) return t;
        return null;
    }
}