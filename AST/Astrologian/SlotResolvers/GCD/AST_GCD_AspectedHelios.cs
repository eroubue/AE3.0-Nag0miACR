using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.SlotResolvers.Ability;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

public class AST_GCD_AspectedHelios : BaseSlotResolver
{
    public static AST_GCD_AspectedHelios Instance { get; } = new();
    public override uint SpellId => SpellsDefine.AspectedHelios.AdjustActionID();
    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseRangeHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseGCDHeal))
            return PreCheckCode.NotQT;

        if (!MsSpellHelper.CanCastSpell()) return PreCheckCode.NotCanCast;

        if (SpellId.RecentlyUsed(3000)) return -9;

        if (TargetHasAspectedHeliosAura(Core.Me)) return -5;

        if (AST_Ability_Horoscope.Instance.CheckCode >= 0 && AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal)) return -666;

        if (Core.Me.HasAura(AurasDefine.GiantDominance)) return -222;

        if (MsAcrHelper.DutyMembersNumber() > 4 && AST_BattleData.Instance.AnotherHealer != null)
        {
            if (MsPartyHelper.GetPartyCountHpLessThanIn15(0.65f, true) >= 2) return 1;
        }
        else
        {
            if (MsPartyHelper.GetPartyCountHpLessThanIn15(0.75f, true) >= 2) return 1;
        }

        return -1;
    }

    public static bool TargetHasAspectedHeliosAura(IBattleChara target)
    {
        return target.HasLocalPlayerAura(AurasDefine.AspectedHelios) ||
               target.HasLocalPlayerAura(AurasDefine.HeliosConjunction);
    }
}