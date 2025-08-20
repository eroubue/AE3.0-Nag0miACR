using AEAssist.CombatRoutine.Module;
using Millusion.Define;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     小宇宙
/// </summary>
public class AST_Ability_Microcosmos : BaseSlotResolver
{
    public static AST_Ability_Microcosmos Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Microcosmos;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        return 0;
    }
}