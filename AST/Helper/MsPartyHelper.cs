using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

namespace Millusion.Helper;

public static class MsPartyHelper
{
    /// <summary>
    ///     获取当前队伍中血量低于指定百分比的队友数量
    /// </summary>
    /// <param name="hpPercent"></param>
    /// <returns></returns>
    public static int GetPartyCountHpLessThan(float hpPercent)
    {
        return (from r in PartyHelper.CastableAlliesWithin30
            where r.CanHeal() && r.CurrentHpPercent() <= hpPercent
            select r).Count();
    }

    /// <summary>
    ///     获取30米内当前队伍成员平均血量
    /// </summary>
    /// <returns></returns>
    public static float GetPartyAverageHpIn30()
    {
        var totalHp = 0f;
        var count = 0;
        foreach (var r in PartyHelper.CastableAlliesWithin30.Where(r => r.CanHeal()))
        {
            totalHp += r.CurrentHpPercent();
            count++;
        }

        return count == 0 ? 0 : totalHp / count;
    }

    /// <summary>
    ///     获取15米内当前队伍受伤成员平均血量
    /// </summary>
    /// <param name="hpPercent">血量百分比，用于计算低于此血量的人数</param>
    /// <param name="notTank">是否排除坦克</param>
    /// <returns></returns>
    public static (int, float) GetPartyAverageHpIn15(float hpPercent, bool notTank = false)
    {
        var totalHp = 0f;
        var count = 0;
        var hpPercentCount = 0;
        foreach (var r in PartyHelper.CastableAlliesWithin15.Where(r => r.CanHeal()))
        {
            var chp = r.CurrentHpPercent();
            if (chp <= hpPercent) hpPercentCount++;
            if (chp >= 0.99) continue;
            if (notTank && r.IsTank()) continue;
            totalHp += chp;
            count++;
        }

        var averageHp = count == 0 ? 0 : totalHp / count;

        return (hpPercentCount, averageHp);
    }

    public static int GetPartyCountHpLessThanIn15(float hpPercent, bool notTank = false)
    {
        return (from r in PartyHelper.CastableAlliesWithin15
            where r.CanHeal() && r.CurrentHpPercent() <= hpPercent && (!notTank || !r.IsTank())
            select r).Count();
    }

    public static int GetPartyCountHpLessThanIn20(float hpPercent, bool notTank = false)
    {
        return (from r in PartyHelper.CastableAlliesWithin20
            where r.CanHeal() && r.CurrentHpPercent() <= hpPercent && (!notTank || !r.IsTank())
            select r).Count();
    }

    /// <summary>
    ///     获取当前队伍中血量最低的队友
    /// </summary>
    /// <returns></returns>
    public static IBattleChara GetPartyMemberWithLowestHp()
    {
        IBattleChara target = null;
        var lowestHp = 1f;
        foreach (var r in PartyHelper.CastableAlliesWithin30.Where(r => r.CanHeal()))
        {
            if (r.CurrentHpPercent() >= lowestHp) continue;
            lowestHp = r.CurrentHpPercent();
            target = r;
        }

        return target;
    }

    /// <summary>
    ///     获取当前队伍中血量最低的坦克队友
    /// </summary>
    /// <returns></returns>
    public static IBattleChara GetPartyMemberWithTankLowestHp()
    {
        IBattleChara target = null;
        var lowestHp = 1f;
        foreach (var r in PartyHelper.CastableAlliesWithin30.Where(r => r.CanHeal() && r.IsTank()))
        {
            if (r.CurrentHpPercent() >= lowestHp) continue;
            lowestHp = r.CurrentHpPercent();
            target = r;
        }

        return target;
    }

    public static IBattleChara GetPartyMemberWithNoTankLowestHp()
    {
        IBattleChara target = null;
        var lowestHp = 1f;
        foreach (var r in PartyHelper.CastableAlliesWithin30.Where(r => r.CanHeal() && !r.IsTank()))
        {
            if (r.CurrentHpPercent() >= lowestHp) continue;
            lowestHp = r.CurrentHpPercent();
            target = r;
        }

        return target;
    }
}