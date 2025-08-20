using AEAssist;
using AEAssist.CombatRoutine.Module;
using Millusion.ACR.Astrologian.Setting;
using Millusion.Define;
using Millusion.Enum;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     醒梦
/// </summary>
public class AST_Ability_LucidDreaming : BaseSlotResolver
{
    public static AST_Ability_LucidDreaming Instance { get; } = new();
    public override uint SpellId => SpellsDefine.LucidDreaming;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (Core.Me.CurrentMp > AST_Settings.Instance.LucidDreamingMp) return -3;

        return PreCheckCode.Success;
    }
}