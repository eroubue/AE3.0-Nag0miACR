using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using Millusion.Define;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

public class AST_Ability_Oracle : BaseSlotResolver
{
    public static AST_Ability_Oracle Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Oracle;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!Core.Me.HasAura(3893)) return -1;

        return 0;
    }
}