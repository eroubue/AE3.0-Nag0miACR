using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Define;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     天宫图
/// </summary>
public class AST_Ability_Horoscope : BaseSlotResolver
{
    public static AST_Ability_Horoscope Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Horoscope.AdjustActionID();
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseRangeHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        if (SpellId.RecentlyUsed()) return PreCheckCode.RecentlyUsed;
        ;
        if (Core.Me.HasAura(AurasDefine.Horoscope))
        {
            if (MsPartyHelper.GetPartyCountHpLessThanIn20(0.4f, true) >= 2)
                return 9;
            return -9;
        }

        if (Core.Me.HasAura(AurasDefine.HoroscopeHelios))
        {
            if (MsPartyHelper.GetPartyCountHpLessThanIn20(0.75f, true) >= 2)
                return 8;
            return -8;
        }

        if (MsPartyHelper.GetPartyCountHpLessThanIn20(0.75f, true) >= 2) return 7;

        return PreCheckCode.NotReady;
    }
}