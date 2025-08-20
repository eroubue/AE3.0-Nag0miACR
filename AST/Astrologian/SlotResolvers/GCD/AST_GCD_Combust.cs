using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
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

public class AST_GCD_Combust : BaseSlotResolver
{
    public static AST_GCD_Combust Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Combust.AdjustActionID();
    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseDot)) return PreCheckCode.NotQT;

        if (!Core.Me.HasSwiftcastAuras() && Core.Me.IsMoving() && GCDHelper.GetGCDCooldown() == 0 &&
            MsData.Instance.MovedDuration > AST_Settings.Instance.MovedDotDelay) return 1;

        var battleChar = Core.Me.GetCurrTarget();

        var t = PartyHelper.CastableMainTanks.FirstOrDefault(r => r.InCombat())?.GetCurrTarget();
        if (t != null && t.IsValid() && !HasCombustAuraWithTimeleft(t) && t.InCombat())
        {
            Target = t;
            return 3;
        }

        var b = TargetMgr.Instance.EnemysIn25.FirstOrDefault(r =>
            !HasCombustAuraWithTimeleft(r.Value) && r.Value.IsInEnemiesList()).Value;
        if (!AST_BattleData.Instance.IsBossBattle && b != null && b.IsValid() && Core.Me.IsMoving())
        {
            Target = b;
            return 2;
        }

        if (battleChar == null || HasCombustAuraWithTimeleft(battleChar))
            return -2;

        Target = battleChar;
        return 0;
    }

    public static bool HasCombustAuraWithTimeleft(IBattleChara target, int timeleft = 4000)
    {
        return target.HasMyAuraWithTimeleft(AurasDefine.Combust, timeleft) ||
               target.HasMyAuraWithTimeleft(AurasDefine.Combust2, timeleft) ||
               target.HasMyAuraWithTimeleft(AurasDefine.Combust3, timeleft);
    }
}