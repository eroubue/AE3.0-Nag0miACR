#nullable enable
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.DalamudServices;
using ECommons.GameFunctions;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using Lumina.Excel;
using Action = Lumina.Excel.GeneratedSheets.Action;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.Helper;

public static class MsSpellHelper
{
    public static ExcelSheet<Action>? Actions { get; set; } = Svc.Data.GetExcelSheet<Action>();

    // public static bool IsPreReady(this uint spellId)
    // {
    //     var spell = spellId.GetSpell();
    //
    //     if (!spell.PreCheck()) return false;
    //
    //     if (!spellId.IsUnlock()) return false;
    //
    //     if (spell.Charges >= 1.0) return true;
    //
    //
    //     if (spell.IsAbility() && spell.Cooldown.TotalMilliseconds <= 100) return true;
    //
    //     // var num = SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs;
    //     if (spell.Cooldown.TotalMilliseconds <= GCDHelper.GetGCDCooldown()) return true;
    //
    //     return false;
    // }


    /// <summary>
    ///     技能是否准备好，包括MP是否足够，等级是否足够，充能数是否足够，cd是否满足等
    /// </summary>
    /// <param name="spellId">技能ID</param>
    /// <returns></returns>
    public static bool IsUnlockWithCD(this uint spellId)
    {
        if (!Player.Available) return false;

        if (Core.Resolve<MemApiSpell>().MPNeed(spellId) > Player.Object.CurrentMp) return false;

        if (!spellId.IsUnlock()) return false;

        if (spellId.Charges() >= 1.0) return true;

        if (spellId.IsAbility() && spellId.Cooldown().TotalMilliseconds <= 100) return true;

        if (!spellId.IsAbility() && spellId.Cooldown().TotalMilliseconds <= GCDHelper.GetGCDCooldown()) return true;

        return false;
    }

    /// <summary>
    ///     判断技能是否完全准备好，是否可以对目标施放
    /// </summary>
    /// <param name="spell"></param>
    /// <returns></returns>
    public static bool IsAllReady(this Spell spell)
    {
        if (!spell.PreCheck()) return false;

        if (!IsReadyWithoutCheckCD(spell)) return false;

        if (spell.Charges >= 1.0) return true;

        if (spell.IsAbility() && spell.Cooldown.TotalMilliseconds <= 100) return true;

        if (!spell.IsAbility() && spell.Cooldown.TotalMilliseconds <= GCDHelper.GetGCDCooldown()) return true;

        return false;
    }

    public static bool IsAbility(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().GetSpellType(spellId) == SpellType.Ability;
    }

    public static TimeSpan Cooldown(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().GetCooldown(spellId);
    }

    public static float Charges(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().GetCharges(spellId);
    }

    /// <summary>
    ///     目标是否在施法范围内
    /// </summary>
    /// <param name="spellId">技能ID</param>
    /// <param name="target">目标</param>
    /// <returns></returns>
    public static bool IsInRange(this uint spellId, IBattleChara? target)
    {
        if (target == null) return false;

        var spell = spellId.GetSpell();
        return target.DistanceToPlayer() <= spell.ActionRange;
    }

    /// <summary>
    ///     是否可以读条施法，包括是否在移动中，是否有瞬发等
    /// </summary>
    /// <returns></returns>
    public static bool CanCastSpell()
    {
        return Core.Me.HasSwiftcastAuras() || !Core.Me.IsMoving();
    }

    private static bool IsReadyWithoutCheckCD(this Spell spell)
    {
        if (!spell.IsUnlock()) return false;

        var target = spell.GetTarget();
        return CanCast(spell.Id, target);

        // return true;
    }

    /// <summary>
    ///     判断技能是否可以对目标施放
    /// </summary>
    /// <param name="spellId">技能ID</param>
    /// <param name="target">目标</param>
    /// <returns></returns>
    public static bool CanCastToTarget(this uint spellId, IBattleChara? target)
    {
        if (target == null) return false;

        return CanCast(spellId, target);
    }

    /// <summary>
    ///     判断技能是否可以对目标施放
    /// </summary>
    /// <param name="spell">技能</param>
    /// <returns></returns>
    public static bool CanCastToTarget(this Spell spell)
    {
        var target = spell.GetTarget();

        if (target == null) return false;

        return CanCast(spell.Id, target);
    }

    private static bool CanCast(uint actionId, IBattleChara? target = null)
    {
        // var time = TimeHelper.Now();
        var r1 = CanCastEx(actionId, target);
        // LogHelper.Info("CanCastEx:" + (TimeHelper.Now() - time));
        // time = TimeHelper.Now();
        var r2 = CheckActionInRangeOrLoS(actionId, target);
        // LogHelper.Info("CheckActionInRangeOrLoS:" + (TimeHelper.Now() - time));
        return r1 && r2;
    }

    private static unsafe bool CheckActionInRangeOrLoS(uint actionId, IGameObject? target = null)
    {
        if (Actions?.GetRow(actionId) is { TargetArea: true })
            return true;
        if (target == null)
        {
            if (Svc.Targets.Target != null && Core.Resolve<MemApiTarget>().CanAttack(Svc.Targets.Target, actionId))
            {
                target = Core.Me.TargetObject;
            }
            else
            {
                if (!Core.Resolve<MemApiTarget>().CanAttack(Svc.ClientState.LocalPlayer!, actionId))
                    return false;
                target = Core.Me;
            }
        }

        if (target == null) return false;

        var los = ActionManager.GetActionInRangeOrLoS(actionId, Core.Me.GameObject(), target.Struct());

        if (los == 0) return true;

        if (los != 565) return false;
        var ascendLos = ActionManager.GetActionInRangeOrLoS(SpellsDefine.Ascend, Core.Me.GameObject(), target.Struct());
        return ascendLos != 562;
    }

    private static unsafe bool CanCastEx(uint actionId, IGameObject? target, bool checkCastingActive = false)
    {
        try
        {
            var id = ActionManager.Instance()->GetAdjustedActionId(actionId);
            var action = Actions?.GetRow(id) ??
                         throw new KeyNotFoundException($"未发现技能 id:{actionId}");
            if (target == null) return false;
            if (!action.TargetArea && !ActionManager.CanUseActionOnTarget(id, target.Struct())) return false;
            if (action.UnlockLink != 0 && !QuestManager.IsQuestComplete(action.UnlockLink)) return false;
            return ActionManager.Instance()->GetActionStatus(ActionType.Action, id,
                action.TargetArea ? 3758096384u : target.EntityId, false, checkCastingActive, null) == 0;
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
            return false;
        }
    }

    public static unsafe uint GetActionState(uint actionId, IGameObject? target, bool checkRecastActive = true,
        bool checkCastingActive = true)
    {
        try
        {
            var action = Actions?.GetRow(actionId) ??
                         throw new KeyNotFoundException($"未发现技能 id:{actionId}");
            var targetId = 3758096384u;
            if (target != null && !action.TargetArea) targetId = target.EntityId;
            return ActionManager.Instance()->GetActionStatus(ActionType.Action, actionId, targetId, checkRecastActive,
                checkCastingActive, null);
        }
        catch (Exception e)
        {
            LogHelper.Error(e.ToString());
            return 999;
        }
    }
}