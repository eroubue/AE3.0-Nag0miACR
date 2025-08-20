using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using AurasDefine = Millusion.Define.AurasDefine;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

internal class AST_GCD_Ascend : BaseSlotResolver
{
    public static AST_GCD_Ascend Instance { get; } = new();
    private static uint Ascend => SpellsDefine.Ascend;
    public override uint SpellId => SpellsDefine.Ascend;
    public override SlotMode Mode => SlotMode.Always;

    public override void Build(Slot slot)
    {
        if (AST_View.UI.GetQt(AST_QT_Key.SwiftcastAscend) &&
            SpellsDefine.Swiftcast.GetSpell().Cooldown.Milliseconds <= 0)
            slot.Add(SpellsDefine.Swiftcast.GetSpell());
        slot.Add(new Spell(Ascend, Target));
    }

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.CastAscend) &&
            !AST_View.UI.GetQt(AST_QT_Key.SwiftcastAscend)) return -2;

        if (SpellsDefine.Ascend.RecentlyUsed(2500)) return -9;

        if (Core.Me.CurrentMp < 2400) return PreCheckCode.NotEnoughMp;

        var deadChar =
            PartyHelper.DeadAllies.FirstOrDefault(r =>
                !r.HasAura(AurasDefine.Raise) && r.IsTargetable && r.DistanceToPlayer() < 30);
        if (deadChar == null || !deadChar.IsValid()) return -1;

        if (!AST_View.UI.GetQt(AST_QT_Key.SwiftcastAscend) && !SpellsDefine.Swiftcast.IsUnlockWithCD()) return -3;

        Target = deadChar;

        if (!Core.Resolve<MemApiSpell>().CheckActionInRangeOrLoS(Ascend, Target)) return -4;

        if (AST_BattleData.Instance.DeathPartyMember.TryGetValue(Target.EntityId, out var info))
            if (TimeHelper.Now() - info.DeathTime < 1000)
                return -5;

        if (AST_View.UI.GetQt(AST_QT_Key.SwiftcastAscend) && SpellsDefine.Swiftcast.IsUnlockWithCD()) return 2;

        if (AST_View.UI.GetQt(AST_QT_Key.CastAscend)) return 1;

        return -10;
    }
}