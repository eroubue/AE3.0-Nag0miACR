using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.Setting;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

internal class AST_GCD_Gravity : BaseSlotResolver
{
    public static AST_GCD_Gravity Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Gravity.AdjustActionID();
    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!MsSpellHelper.CanCastSpell()) return -15;

        if (AST_Settings.Instance.GravityAutoTarget) Target = TargetHelper.GetMostCanTargetObjects(SpellId);
        Target ??= Core.Me.GetCurrTarget();

        if (Target == null || !Target.IsValid()) return -2;

        if (TargetHelper.GetNearbyEnemyCount(Target, 25, 5) < 3) return -3;

        return 0;
    }
}