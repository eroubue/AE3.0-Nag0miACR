using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.Setting;
using Millusion.Enum;
using Millusion.Helper;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     地星
/// </summary>
internal class AST_Ability_EarthlyStar : BaseSlotResolver
{
    public static AST_Ability_EarthlyStar Instance { get; } = new();
    public override uint SpellId => SpellsDefine.EarthlyStar;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override Spell GetSpell()
    {
        if (Target == null) return SpellId.GetSpell();

        return AST_Settings.Instance.EarthlyStarPriorityCenter
            ? new Spell(SpellId, MsAcrHelper.MapCenter(Target.Position))
            : new Spell(SpellId, Target.Position);
    }

    protected override int RunCheck()
    {
        if (SpellId.RecentlyUsed()) return PreCheckCode.RecentlyUsed;

        if (Core.Me.IsMoving() && !AST_BattleData.Instance.IsBossBattle) return -3;

        if (AI.Instance.BattleData.CurrBattleTimeInMs < 1000) return -50;

        var t = Core.Me.GetCurrTarget();

        if (t != null && TTKHelper.IsTargetTTK(t, 15000, false) &&
            AI.Instance.BattleData.CurrBattleTimeInMs > 1000) return -4;

        if (!TargetMgr.Instance.EnemysIn25.Any(r => r.Value.IsBoss() && r.Value.IsInEnemiesList()))
        {
            var mostHpTarget = TargetMgr.Instance.EnemysIn25.Where(r => r.Value.IsInEnemiesList())
                .OrderByDescending(r => r.Value.CurrentHpPercent()).FirstOrDefault();

            if (mostHpTarget.Value != null && TTKHelper.IsTargetTTK(mostHpTarget.Value, 12000, true)) return -5;
        }

        if (AST_Settings.Instance.EarthlyStarTarget == (int)SpellTargetType.Target && t != null && t.IsValid())
        {
            Target = t;
            if (t.HitboxRadius >= 15) Target = Core.Me;
        }
        else
        {
            Target = Core.Me;
        }


        return PreCheckCode.Success;
    }
}