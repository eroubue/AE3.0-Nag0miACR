using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Nagomi.GNB.utils;
using Nagomi.PCT;
// ReSharper disable UnusedMember.Global


namespace Nagomi;

public static class Helper
{
    public static IPlayerCharacter 自身 => Core.Me;

    public static uint 自身血量 => Core.Me.CurrentHp;

    public static uint 自身蓝量 => Core.Me.CurrentMp;

    public static float 自身血量百分比 => Core.Me.CurrentHpPercent();

    public static float 自身蓝量百分比 => Core.Me.CurrentMpPercent();

    public static int 队伍成员数量 => PartyHelper.Party.Count;

    public static uint 自身当前等级 => Core.Me.Level;


    public static bool 是否在副本中()
    {
        return Core.Resolve<MemApiCondition>().IsBoundByDuty();
    }
    public static bool 团辅是否快转好()
    {
        var 团辅cd=Core.Resolve<MemApiSpell>().GetCooldown(PCTSpells.星空构想);
        return 团辅cd.TotalSeconds-PCTSettings.Instance.团辅提前 <= 0;
    }

    public static uint 当前地图id => Core.Resolve<MemApiZoneInfo>().GetCurrTerrId();

    public static int 副本人数()
    {
        return Core.Resolve<MemApiDuty>().DutyMembersNumber();
    }

    public static bool 自身是否在移动()
    {
        return Core.Resolve<MemApiMove>().IsMoving();
    }

    public static bool 自身是否在读条()
    {
        return Core.Me.IsCasting;
    }

    public static int 自身周围单位数量(int range)
    {
        return TargetHelper.GetNearbyEnemyCount(range);
    }

    public static bool 自身存在Buff(uint id)
    {
        return Core.Me.HasAura(id);
    }

    public static bool 自身存在其中Buff(List<uint> auras, int msLeft = 0)
    {
        return Core.Me.HasAnyAura(auras, msLeft);
    }
    
    public static uint 自身命中其中Buff(List<uint> auras, int msLeft = 0)
    {
        return Core.Me.HitAnyAura(auras, msLeft);
    }

    public static bool 自身存在Buff大于时间(uint id, int time)
    {
        return Core.Me.HasMyAuraWithTimeleft(id, time);
    }

    public static int 自身buff剩余时间(uint id)
    {
        return Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, id,true);
    }

    public static bool 自己的Buff剩余时间是否大于指定值(uint id,int time)
    {
        return Core.Me.HasMyAuraWithTimeleft(id, time);
    }

  /*  public static bool 自身是否正在读长条()
    {
        return PCTList.读长条.Contains(Core.Me.CastActionId);
    }
    */

    /** -----------------他人状态相关----------------- **/
    public static float 目标血量百分比 => Core.Me.GetCurrTarget().CurrentHpPercent();

    public static bool 目标战斗状态(IBattleChara target)
    {
        return target.InCombat();
    }


    public static bool 目标是否可见或在技能范围内(uint actionId)
    {
        return Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS(actionId) is not (566 or 562);
    }

    // public static bool 目标是否可见或在技能范围内(uint spellId, CharacterAgent target)
    // {
    //
    //     Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS()
    //     var localPlayer = (GameObject*)Svc.ClientState.LocalPlayer.Address;
    //     if (ActionManager.GetActionInRangeOrLoS(spellId, localPlayer, SignatureHook.Instance.从ObjectID获取GameObject(target.ObjectId)) is 562 or 566) return false;
    //     return true;
    // }
    //
    // public static bool 是否能对目标使用技能(uint spellId, CharacterAgent target)
    // {
    //     return ActionManager.CanUseActionOnTarget(spellId, SignatureHook.Instance.从ObjectID获取GameObject(target.ObjectId));
    // }


    public static float 目标到自身距离()
    {
        return Core.Me.GetCurrTarget().Distance(Core.Me);
    }

    public static float 到自身近战距离(IBattleChara target)
    {
        return Core.Me.Distance(target);
    }

    public static float 到自身距离(IBattleChara target)
    {
        return Core.Me.Distance(target);
    }

    public static bool 目标是否为假人()
    {
        return Core.Me.GetCurrTarget().IsDummy();
    }

    public static bool 目标的指定buff剩余时间是否大于(uint id, int timeLeft = 0)
    {
        return Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(id, timeLeft);
    }

    public static bool 指定buff剩余时间是否大于(IBattleChara target, uint id, int timeLeft = 0)
    {
        return target.HasMyAuraWithTimeleft(id, timeLeft);
    }

    public static int 目标的指定BUFF层数(IBattleChara target, uint buff)
    {
        return Core.Resolve<MemApiBuff>().GetStack(target, buff);
    }

    public static bool 目标可选状态(IBattleChara target)
    {
        return target.IsTargetable;
    }


    public static bool 目标是否拥有BUFF(uint id)
    {
        return Core.Me.GetCurrTarget().HasAura(id);
    }

    public static bool 目标是否准备放aoe(IBattleChara target, int timeLeft)
    {
        return TargetHelper.targetCastingIsBossAOE(target, timeLeft);
    }

    public static bool 目标是否拥有其中的BUFF(List<uint> auras, int timeLeft = 0)
    {
        return Core.Me.GetCurrTarget().HasAnyAura(auras, timeLeft);
    }

    public static bool 是否拥有其中的BUFF(IBattleChara target, List<uint> auras, int timeLeft = 0)
    {
        return target.HasAnyAura(auras, timeLeft);
    }

    public static bool 目标是否存在于DOT黑名单中(IBattleChara r)
    {
        return DotBlacklistHelper.IsBlackList(r);
    }

    public static bool 队员是否拥有可驱散状态()
    {
        return PartyHelper.CastableAlliesWithin30.Any(
            agent => agent.HasCanDispel() && agent.Distance(Core.Me) <= 30);
    }

    public static bool 队员是否拥有BUFF(uint buff)
    {
        return PartyHelper.CastableAlliesWithin30.Any(agent => agent.HasAura(buff));
    }

    public static int 二十米视线内血量低于设定的队员数量(float hp)
    {
        return PartyHelper.CastableAlliesWithin20.Count(
            r => r.CurrentHp != 0 && r.CurrentHpPercent() <= hp
        );
    }

    public static int 三十米视线内血量低于设定的队员数量(float hp)
    {
        return PartyHelper.CastableAlliesWithin30.Count(
            r => r.CurrentHp != 0 && r.CurrentHpPercent() <= hp
        );
    }

    public static int 十十米视线内血量低于设定的队员数量(float hp)
    {
        return PartyHelper.CastableAlliesWithin10.Count(
            r => r.CurrentHp != 0 && r.CurrentHpPercent() <= hp
        );
    }


    /** -----------------目标相关----------------- **/
    public static IBattleChara 自身目标 => Core.Me.GetCurrTarget();

    public static IBattleChara? 自身目标的目标 => Core.Me.GetCurrTargetsTarget();
    

    public static IBattleChara? 没有复活状态的死亡队友()
    {
        return PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(PCTBuffs.复活) && r.IsTargetable);
    }

    public static IBattleChara 获取可驱散队员()
    {
        return PartyHelper.CastableAlliesWithin30.LastOrDefault(agent =>
            agent.HasCanDispel() && agent.Distance(Core.Me) <= 30);
    }

    public static IBattleChara 获取拥有buff队员(uint buff)
    {
        return PartyHelper.CastableAlliesWithin30.LastOrDefault(agent => agent.HasAura(buff));
    }

    public static IBattleChara 获取血量最低成员()
    {
        if (PartyHelper.CastableAlliesWithin30.Count == 0)
            return Core.Me;
        return PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHp > 0).MinBy(r => r.CurrentHpPercent());
    }

    public static IBattleChara 获取血量最低成员_排除buff(uint buffId)
    {
        if (PartyHelper.CastableAlliesWithin30.Count == 0)
            return Core.Me;
        return PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHp > 0 && !r.HasAura(buffId)).MinBy(r => r.CurrentHpPercent());
    }

    public static IBattleChara 获取最低血量T()
    {
        if (PartyHelper.CastableTanks.Count == 0)
            return Core.Me;
        if (PartyHelper.CastableTanks.Count == 2 && PartyHelper.CastableTanks[1].CurrentHpPercent() <
            PartyHelper.CastableTanks[0].CurrentHpPercent())
            return PartyHelper.CastableTanks[1];
        return PartyHelper.CastableTanks[0];
    }

    public static IBattleChara 获取最低血量T_排除buff(uint buffId)
    {
        if (PartyHelper.CastableTanks.Count == 0)
            return Core.Me;
        if (PartyHelper.CastableTanks.Count == 2 &&
            PartyHelper.CastableTanks[1].CurrentHpPercent() < PartyHelper.CastableTanks[0].CurrentHpPercent() &&
            !PartyHelper.CastableTanks[1].HasAura(buffId))
            return PartyHelper.CastableTanks[1];
        return PartyHelper.CastableTanks[0];
    }

    public static IBattleChara 获取距离最远成员()
    {
        IBattleChara RescueTarget = PartyHelper.CastableAlliesWithin30
            .Where(r => r.CurrentHp > 0).MaxBy(r => r.Distance(PartyHelper.CastableAlliesWithin30.FirstOrDefault()));

        return RescueTarget;
    }

    public static IBattleChara 获取第几号小队列表(int index)
    {
        return PartyHelper.CastableParty[index - 1];
    }


    /** -----------------技能相关----------------- **/
    public static Spell 获取技能(uint id)
    {
        return id.GetSpell();
    }
    

    public static uint 技能状态码(uint id)
    {
        return Core.Resolve<MemApiSpell>().GetActionState(id);
    }

    public static Spell 获取会变化的技能(uint id)
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(id).GetSpell();
    }

    public static uint 获取会变化的技能id(uint id)
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(id);
    }


    public static int 高优先级gcd队列中技能数量()
    {
        return AI.Instance.BattleData.HighPrioritySlots_GCD.Count;
    }
    

    public static uint 上一个连击技能()
    {
        return Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    }
    public static byte 上一个绝枪123层数()
    {
        uint lastSpellId = Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    
        if (lastSpellId == GNBSpells.利刃斩)
            return 2;
        if (lastSpellId == GNBSpells.残暴弹)
            return 1;
        if (lastSpellId == GNBSpells.迅连斩)
            return 3;
    
        return 3 ; // 默认值，表示没有匹配到任何指定技能
    }

    public static int GCD剩余时间()
    {
        return GCDHelper.GetGCDCooldown();
    }

    public static bool GCD可用状态()
    {
        return GCDHelper.CanUseGCD();
    }

    public static uint 上一个gcd技能()
    {
        return Core.Resolve<MemApiSpellCastSuccess>().LastGcd;
    }

    public static uint 上一个能力技能()
    {
        return Core.Resolve<MemApiSpellCastSuccess>().LastAbility;
    }


    public static bool 技能是否刚使用过(this uint spellId, int time )
    {
        return spellId.GetSpell().RecentlyUsed(time);
    }
    public static bool 技能22s内是否用过(this uint spellId, int time=22500)
    {
        return spellId.GetSpell().RecentlyUsed(time);
    }
    public static bool 技能0dot6s内是否用过(this uint spellId)
    {
        return spellId.GetSpell().RecentlyUsed(600);
    }
    

    public static float 充能层数(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().GetCharges(spellId);
    }

    /// <summary>
    /// 检测在自己的buff剩余时间内某个技能冷却能否结束
    /// </summary>
    /// <param name="buffId">要检查的buff ID</param>
    /// <param name="spellId">要检查的技能ID</param>
    /// <returns>true表示buff剩余时间内技能冷却能结束，false表示不能</returns>
    public static bool 技能冷却能否在buff剩余时间内结束(uint buffId, uint spellId)
    {
        // 获取buff剩余时间（毫秒）
        var buffTimeLeft = Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, buffId, true);
        
        // 获取技能剩余冷却时间（毫秒）
        var spellCooldown = Core.Resolve<MemApiSpell>().GetCooldown(spellId);
        var spellCooldownMs = (int)spellCooldown.TotalMilliseconds;
        
        // 如果技能没有冷却，直接返回true
        if (spellCooldownMs <= 0)
            return true;
        
        // 比较buff剩余时间和技能冷却时间
        return buffTimeLeft >= spellCooldownMs;
    }

    /// <summary>
    /// 检测在自己的buff剩余时间内某个技能冷却能否结束（带时间容差）
    /// </summary>
    /// <param name="buffId">要检查的buff ID</param>
    /// <param name="spellId">要检查的技能ID</param>
    /// <param name="timeBuffer">时间容差（毫秒），默认500ms</param>
    /// <returns>true表示buff剩余时间内技能冷却能结束，false表示不能</returns>
    public static bool 技能冷却能否在buff剩余时间内结束_带容差(uint buffId, uint spellId, int timeBuffer = 500)
    {
        // 获取buff剩余时间（毫秒）
        var buffTimeLeft = Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, buffId, true);
        
        // 获取技能剩余冷却时间（毫秒）
        var spellCooldown = Core.Resolve<MemApiSpell>().GetCooldown(spellId);
        var spellCooldownMs = (int)spellCooldown.TotalMilliseconds;
        
        // 如果技能没有冷却，直接返回true
        if (spellCooldownMs <= 0)
            return true;
        
        // 比较buff剩余时间和技能冷却时间（考虑时间容差）
        return buffTimeLeft >= (spellCooldownMs + timeBuffer);
    }

    /// <summary>
    /// 检测技能是否能在buff剩余时间内冷却完毕（简化版）
    /// </summary>
    /// <param name="buffId">要检查的buff ID</param>
    /// <param name="spellId">要检查的技能ID</param>
    /// <returns>true表示buff剩余时间内技能冷却能结束，false表示不能</returns>
    public static bool 技能冷却能否在buff剩余时间内结束_简化版(uint buffId, uint spellId)
    {
        // 获取buff剩余时间（毫秒）
        var buffTimeLeft = Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, buffId, true);
        
        // 获取技能剩余冷却时间（毫秒）
        var spellCooldown = Core.Resolve<MemApiSpell>().GetCooldown(spellId);
        var spellCooldownMs = (int)spellCooldown.TotalMilliseconds;
        
        // 如果技能没有冷却，直接返回true
        if (spellCooldownMs <= 0)
            return true;
        
        // 比较buff剩余时间和技能冷却时间
        return buffTimeLeft >= spellCooldownMs;
    }

 
   

}