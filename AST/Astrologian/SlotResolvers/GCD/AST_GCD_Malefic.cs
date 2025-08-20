using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

public class AST_GCD_Malefic : BaseSlotResolver
{
    public static AST_GCD_Malefic Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Malefic.AdjustActionID();
    public override SlotMode Mode => SlotMode.Gcd;

    protected override int RunCheck()
    {
        if (!MsSpellHelper.CanCastSpell()) return PreCheckCode.NotCanCast;

        return 0;
    }
}