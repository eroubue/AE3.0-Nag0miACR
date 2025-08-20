using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

/// <summary>
///     大宇宙
/// </summary>
public class AST_GCD_Macrocosmos : BaseSlotResolver
{
    public static AST_GCD_Macrocosmos Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Macrocosmos;
    public override SlotMode Mode => SlotMode.Gcd;
    public override SpellEffectType EffectType => SpellEffectType.Heal;
    public override uint Power => 200;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseRangeHeal) || !AST_View.UI.GetQt(AST_QT_Key.UseAbilityHeal))
            return PreCheckCode.NotQT;

        if (!AST_View.UI.GetQt(AST_QT_Key.UseMacrocosmos)) return PreCheckCode.NotQT;
        

        if (Core.Me.HasLocalPlayerAura(AurasDefine.NeutralSect)) return -2;

        if (TargetMgr.Instance.EnemysIn25.Any(r =>
                r.Value.IsBoss() && r.Value.IsInEnemiesList() &&
                TargetHelper.targetCastingIsBossAOE(r.Value, 3000))) return 2;

        return -1;
    }
}