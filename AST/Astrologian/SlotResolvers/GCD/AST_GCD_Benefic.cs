using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

public class AST_GCD_Benefic : BaseSlotResolver
{
    public static AST_GCD_Benefic Instance { get; } = new();
    private static uint Benefic => SpellsDefine.Benefic;

    public override uint SpellId => SpellsDefine.Benefic2.IsLevelEnough() ? SpellsDefine.Benefic2 : Benefic;

    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseGCDHeal)) return -100;

        // if (!Benefic2.IsAllReady() && !Benefic.IsAllReady()) return PreCheckCode.NotReady;

        if (!MsSpellHelper.CanCastSpell()) return PreCheckCode.NotCanCast;

        if (AST_Settings.Instance.ForceHealTarget)
        {
            var t = Core.Me.GetCurrTarget();
            if (t != null && t.IsValid() && !t.IsEnemy())
            {
                Target = t;
                if (t.CurrentHpPercent() < 0.7) return 666;
            }
        }

        if (MsPartyHelper.GetPartyCountHpLessThanIn15(0.75f, true) >= 2) return -6;

        if (MsAcrHelper.DutyMembersNumber() > 4 && AST_BattleData.Instance.AnotherHealer != null)
        {
            Target = AST_BattleData.Instance.PartyMemberWithLowestHpTank;

            if (Target != null && Target.CurrentHpPercent() < 0.6) return 3;

            Target = AST_BattleData.Instance.PartyMemberWithLowestHpNotTank;

            if (AST_View.UI.GetQt(AST_QT_Key.OnlyHealTank) && Target != null && !Target.IsTank()) return -101;

            if (Target != null && Target.CurrentHpPercent() < 0.5) return 4;
        }
        else
        {
            Target = AST_BattleData.Instance.PartyMemberWithLowestHpTank;

            if (Target != null && Target.CurrentHpPercent() < 0.65) return 3;

            Target = AST_BattleData.Instance.PartyMemberWithLowestHpNotTank;

            if (Target != null && Target.CurrentHpPercent() < 0.6) return 4;
        }

        return -1;
    }
}