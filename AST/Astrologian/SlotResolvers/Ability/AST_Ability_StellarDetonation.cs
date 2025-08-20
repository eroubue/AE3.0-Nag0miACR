using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.Define;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

public class AST_Ability_StellarDetonation : BaseSlotResolver
{
    public static AST_Ability_StellarDetonation Instance { get; } = new();
    public override uint SpellId => SpellsDefine.StellarDetonation;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        // if (JobView.UI.GetQt(QTKey.OnlyGCDHeal)) return -100;

        if (SpellId.RecentlyUsed(2500)) return -2;

        if (!Core.Me.HasAura(AurasDefine.EarthlyDominance) && !Core.Me.HasAura(AurasDefine.GiantDominance)) return -1;

        //if (Core.Me.HasLocalPlayerAura(Define.AurasDefine.Divination) && Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, Define.AurasDefine.Divination, true) < 3000)
        //{
        //    return 1;
        //}

        if (TargetMgr.Instance.EnemysIn25.Any(r => r.Value.IsBoss() && r.Value.CurrentHpPercent() < 0.05)) return 3;

        if (!TargetMgr.Instance.EnemysIn25.Any(r => r.Value.IsBoss() && r.Value.IsInEnemiesList()) &&
            TargetMgr.Instance.EnemysIn25.Any(r => r.Value.IsInEnemiesList()) &&
            TargetMgr.Instance.EnemysIn25.Where(r => r.Value.IsInEnemiesList())
                .Average(r => r.Value.CurrentHpPercent()) < 0.2)
            return 4;

        if (Core.Me.HasAura(AurasDefine.GiantDominance) &&
            PartyHelper.CastableParty.Count(r => r.CurrentHpPercent() < 0.75) >= 2)
            return 2;

        return -111;
    }
}