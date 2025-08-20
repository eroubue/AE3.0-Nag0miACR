using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.UI;
using Millusion.Define;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     光速
/// </summary>
public class AST_Ability_Lightspeed : BaseSlotResolver
{
    public static AST_Ability_Lightspeed Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Lightspeed;
    public override SlotMode Mode => SlotMode.Always;

    protected override int RunCheck()
    {
        if (SpellId.RecentlyUsed(15000)) return PreCheckCode.RecentlyUsed;

        if (Core.Me.HasSwiftcastAuras()) return -3;

        if (!Core.Me.InCombat()) return -4;

        if (GCDHelper.CanUseOffGcd())
            if (SpellExtension.IsUnlock(SpellsDefine.Divination) &&
                AST_View.UI.GetQt(AST_QT_Key.UseDivination) &&
                SpellsDefine.Divination.GetSpell().Cooldown.TotalMilliseconds < 4000)
                return 1;


        if (AST_Settings.Instance.MovedLightspeed && AST_BattleData.Instance.IsBossBattle && Core.Me.IsMoving() &&
            GCDHelper.GetGCDCooldown() == 0) return 2;

        return -111;
    }
}