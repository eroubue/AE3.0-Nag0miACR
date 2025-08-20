using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.Helper;
using Millusion.Structs;

namespace Millusion.ACR.Astrologian.BattleData;

public class AST_BattleData
{
    public static AST_BattleData Instance { get; private set; } = new();

    public long LastTime { get; set; }

    public Dictionary<uint, DeathBattleCharaInfo> DeathPartyMember { get; set; } = new();

    /// <summary>
    ///     最低血量的队友
    /// </summary>
    public IBattleChara PartyMemberWithLowestHp { get; set; }

    /// <summary>
    ///     最低血量的坦克队友
    /// </summary>
    public IBattleChara PartyMemberWithLowestHpTank { get; set; }

    /// <summary>
    ///     最低血量的非坦克队友
    /// </summary>
    public IBattleChara PartyMemberWithLowestHpNotTank { get; set; }

    /// <summary>
    ///     是否是Boss战
    /// </summary>
    public bool IsBossBattle { get; set; }

    /// <summary>
    ///     当前战斗剩余时间预测
    /// </summary>
    public int BattleRemainingTime { get; set; }

    /// <summary>
    ///     另一个活着的治疗, 用于配合使用
    /// </summary>
    public IBattleChara AnotherHealer { get; set; }

    public int CanHealCount { get; set; }

    public static void Reset()
    {
        Instance = new AST_BattleData();
    }

    public static void Update()
    {
        var list = PartyHelper.CastableAlliesWithin30.Where(r => r.CanHeal())
            .OrderBy(r => r.CurrentHpPercent())
            .ToList();

        Instance.PartyMemberWithLowestHp = list.FirstOrDefault();
        Instance.PartyMemberWithLowestHpTank = list.FirstOrDefault(r => r.IsTank());
        Instance.PartyMemberWithLowestHpNotTank = list.FirstOrDefault(r => !r.IsTank());

        Instance.AnotherHealer = PartyHelper.CastableHealers.FirstOrDefault(v => !v.IsMe() && !v.IsDead);

        Instance.IsBossBattle =
            TargetMgr.Instance.Enemys.Any(r => r.Value.IsBoss() && r.Value.IsInEnemiesList());

        var remainingTime = 0;
        foreach (var chara in TargetMgr.Instance.Enemys)
        {
            if (!TargetMgr.Instance.TargetStats.TryGetValue(chara.Value.EntityId, out var stat)) continue;

            if (stat.DeathPrediction > remainingTime) remainingTime = stat.DeathPrediction;
        }

        Instance.BattleRemainingTime = remainingTime;


        foreach (var charaInfo in Instance.DeathPartyMember)
            if (!charaInfo.Value.Chara.IsValid() || !charaInfo.Value.Chara.CanRaise())
                Instance.DeathPartyMember.Remove(charaInfo.Key);

        foreach (var battleChara in PartyHelper.Party)
            if (!Instance.DeathPartyMember.ContainsKey(battleChara.EntityId) && battleChara.CanRaise())
                Instance.DeathPartyMember.Add(battleChara.EntityId, new DeathBattleCharaInfo
                {
                    Chara = battleChara,
                    DeathTime = TimeHelper.Now()
                });
    }
}