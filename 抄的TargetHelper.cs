using System.Runtime.CompilerServices;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace Nagomi.utils;

public partial class 抄的Targethelper
{
    public static IBattleChara? 圆形智能AOE目标(int minTargetCount, IBattleChara? currentTarget, 
        float maxCastRange, float aoeRadius)
    {
        var player = Svc.ClientState.LocalPlayer;
        var enemies = Data.AllHostileTargets?
            .Where(e => e != null && e.IsValid() && e.DistanceToPlayer() <= maxCastRange + aoeRadius)
            .ToList() ?? new List<IBattleChara>();
        if (!enemies.Any()) return currentTarget;

        List<IBattleChara> bestTargets = new List<IBattleChara>();
        int maxHitCount = 0;

        foreach (var potentialTarget in enemies)
        {
            if (potentialTarget.DistanceToPlayer() > maxCastRange) continue;

            int hitCount = enemies.Count(e => Vector3.Distance(e.Position, potentialTarget.Position) <= aoeRadius);

            if (hitCount > maxHitCount)
            {
                maxHitCount = hitCount;
                bestTargets.Clear();
                bestTargets.Add(potentialTarget);
            }
            else if (hitCount == maxHitCount)
            {
                bestTargets.Add(potentialTarget);
            }
        }

        // 如果找到的最佳目标可以击中足够多的敌人
        if (bestTargets.Any() && maxHitCount >= minTargetCount)
        {
            // 从最佳目标中选择血量最多的
            return bestTargets.OrderByDescending(t => t.CurrentHp).FirstOrDefault();
        }

        // 如果没有找到满足条件的目标，返回当前选中的目标
        return currentTarget;
    }
    
    // public static IBattleChara? SmartTargetCircleAOE(uint skill, int count, IBattleChara currentTarget, int spellCastRange,
    //     int damageRange) //技能，可攻击目标数
    // {
     //    var canTargetObjects = TargetHelper.GetMostCanTargetObjects(skill, count); //可被该技能命中的最大目标数
    //     if (canTargetObjects != null && canTargetObjects.IsValid() && canTargetObjects.DistanceToPlayer() <= spellCastRange)
    //     {
    //         return canTargetObjects;
    //     }
    //     if (currentTarget != null &&
    //              TargetHelper.GetNearbyEnemyCount(currentTarget, spellCastRange, damageRange) >= count)
    //     {
    //         return currentTarget;
    //     }
    //
    //     return currentTarget;
    // }
    
public static IBattleChara? 直线智能AOE目标(int count, IBattleChara currentTarget, float length, 
    float width, int spellCastRange = 10)
{
    IBattleChara? bestTargetGroup = 线性智能AOE目标(length, width, spellCastRange);
    if (bestTargetGroup != null && bestTargetGroup.IsValid() && bestTargetGroup.DistanceToPlayer() <= spellCastRange)
    {
        return bestTargetGroup;
    }
    
    if (currentTarget != null)
    {
        // 获取当前目标附近的敌人沿直线排列的数量
        int nearbyEnemiesInLine = TargetHelper.GetEnemyCountInsideRect(Core.Me, currentTarget, length, width);
    
        // 如果附近的敌人数量足够，选择当前目标
        if (nearbyEnemiesInLine >= count)
        {
            return currentTarget;
        }
    }

    // 如果没有找到合适的目标，返回 null
    return currentTarget;
}

private static IBattleChara? 线性智能AOE目标(float length, float width, int spellCastRange)
{
    var player = Svc.ClientState.LocalPlayer;
    if (player == null) return null;

    var enemies = Data.AllHostileTargets.Where(e => e.DistanceToPlayer() <= length).ToList();
    if (!enemies.Any()) return null;

    List<IBattleChara> optimalTargets = new List<IBattleChara>();
    int maxHitCount = 0;

    foreach (var potentialTarget in enemies)
    {
        if (potentialTarget.DistanceToPlayer() > spellCastRange) continue;

        Vector3 direction = potentialTarget.Position - player.Position;
        direction.Y = 0; // 忽略高度差
        direction = Vector3.Normalize(direction);

        int hitCount = 0;
        foreach (var enemy in enemies)
        {
            if (在矩形里(player.Position, direction, length, width, enemy.Position))
            {
                hitCount++;
            }
        }

        if (hitCount > maxHitCount)
        {
            maxHitCount = hitCount;
            optimalTargets.Clear();
            optimalTargets.Add(potentialTarget);
        }
        else if (hitCount == maxHitCount)
        {
            optimalTargets.Add(potentialTarget);
        }
    }

    // 从最佳目标中选择血量最多的
    return optimalTargets.OrderByDescending(t => t.CurrentHp).FirstOrDefault();
}

private static bool 在矩形里(Vector3 start, Vector3 direction, float length, float width, Vector3 point)
{
    Vector3 toPoint = point - start;
    float dotProduct = Vector3.Dot(toPoint, direction);

    if (dotProduct < 0 || dotProduct > length)
        return false;

    Vector3 projection = start + direction * dotProduct;
    float distanceToLine = Vector3.Distance(point, projection);

    return distanceToLine <= width / 2;
}

    
public static IBattleChara? 扇形智能AOE目标(float angle, float maxDistance, int targetCount, IBattleChara currentTarget)
{
    var player = Core.Me;
    if (player == null) return null;

    var enemies = Data.AllHostileTargets.Where(e => e.DistanceToPlayer() <= maxDistance).ToList();
    if (!enemies.Any()) return null;

    List<IBattleChara> optimalTargets = new List<IBattleChara>();
    int maxHitCount = 0;

    foreach (var potentialTarget in enemies)
    {
        Vector3 direction = potentialTarget.Position - player.Position;
        direction.Y = 0; // 忽略高度差
        direction = direction.Normalized;

        int hitCount = 0;
        foreach (var enemy in enemies)
        {
            if (在扇形里(player.Position, direction, angle, maxDistance, enemy.Position))
            {
                hitCount++;
            }
        }

        if (hitCount > maxHitCount)
        {
            maxHitCount = hitCount;
            optimalTargets.Clear();
            optimalTargets.Add(potentialTarget);
        }
        else if (hitCount == maxHitCount)
        {
            optimalTargets.Add(potentialTarget);
        }
    }

    // 如果找到的最佳目标命中数量不小于指定的目标数量，返回血量最多的目标
    if (optimalTargets.Any() && maxHitCount >= targetCount)
    {
        return optimalTargets.OrderByDescending(t => t.CurrentHp).FirstOrDefault();
    }

    // 如果当前目标在范围内且命中数量足够，返回当前目标
    if (currentTarget != null && currentTarget.DistanceToPlayer() <= maxDistance)
    {
        Vector3 currentDirection = currentTarget.Position - player.Position;
        currentDirection.Y = 0;
        currentDirection = currentDirection.Normalized;

        int currentHitCount = enemies.Count(e => 在扇形里(player.Position, currentDirection, angle, maxDistance, e.Position));
        if (currentHitCount >= targetCount)
        {
            return currentTarget;
        }
    }

    // 如果没有找到合适的目标，返回当前目标
    return currentTarget;
}

private static bool 在扇形里(Vector3 origin, Vector3 direction, float angle, float maxDistance, Vector3 point)
{
    Vector3 toPoint = point - origin;
    toPoint.Y = 0; // 忽略高度差

    float distance = toPoint.Magnitude;
    if (distance > maxDistance)
        return false;

    toPoint = toPoint.Normalized;
    float dotProduct = Vector3.Dot(direction, toPoint);
    double angleRadians = angle * Math.PI / 360f; // 将角度转换为弧度，并除以2（因为是半角）

    return Math.Acos(dotProduct) <= angleRadians;
}
    
}

