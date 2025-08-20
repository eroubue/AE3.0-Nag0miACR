using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

public class AST_GCD_AspectedBenefic : BaseSlotResolver
{
    public static AST_GCD_AspectedBenefic Instance { get; } = new();
    public override uint SpellId => SpellsDefine.AspectedBenefic;
    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseSingleHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseGCDHeal))
            return PreCheckCode.NotQT;


        if (AST_Settings.Instance.ForceHealTarget)
        {
            var t = Core.Me.GetCurrTarget();
            if (t != null && t.IsValid() && !t.IsEnemy())
            {
                Target = t;
                if (Target.HasLocalPlayerAura(AurasDefine.AspectedBenefic)) return -2;

                if (t.CurrentHpPercent() < 0.75) return 666;
            }
        }

        if (MsPartyHelper.GetPartyCountHpLessThanIn15(0.75f, true) >= 2) return -6;

        Target = AST_BattleData.Instance.PartyMemberWithLowestHpTank;

        if (Target == null) return -1;

        if (Target.CurrentHpPercent() < 0.5) return -10;

        if (Target.CurrentHpPercent() > 0.75) return -11;

        if (Target.HasLocalPlayerAura(AurasDefine.AspectedBenefic)) return -2;

        return 0;
    }
}