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

public class AST_Ability_Play3 : BaseSlotResolver
{
    public static AST_Ability_Play3 Instance { get; } = new();

    public override uint SpellId => SpellsDefine.Play3.AdjustActionID();

    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal)) return -100;

        if (SpellId.RecentlyUsed()) return -9;

        if (!Core.Me.InCombat()) return -6;

        var card = Core.Resolve<JobApi_Astrologian>().DrawnCards[2];
        if (card == CardType.SPIRE)
        {
            var t = GetSpireTarget();
            if (t != null)
            {
                Target = t;
                return 2;
            }
        }

        if (card == CardType.EWER)
        {
            var t = GetEwerTarget();

            if (t != null)
            {
                Target = t;
                return 3;
            }
        }

        return -1;
    }

    public static IBattleChara GetSpireTarget()
    {
        var t = (from r in PartyHelper.CastableAlliesWithin30
            where (r.CurrentHp != 0 && r.CurrentHpPercent() < 0.9 && r.IsTank()) ||
                  (r.IsMe() && r.CurrentHpPercent() < 0.5)
            orderby r.CurrentHpPercent()
            select r).FirstOrDefault();
        if (t != null && t.IsValid()) return t;
        return null;
    }

    public static IBattleChara GetEwerTarget()
    {
        var t = (from r in PartyHelper.CastableAlliesWithin30
            where r.CurrentHp != 0 && r.CurrentHpPercent() < 0.8
            orderby r.CurrentHpPercent()
            select r).FirstOrDefault();

        if (t != null && t.IsValid()) return t;
        return null;
    }
}