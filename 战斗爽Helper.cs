using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist. MemoryApi;

namespace Nagomi.utils;

public static class 战斗爽Helper
    {
    public static bool 战斗爽 ()
        {
        // 如果当前目标的生命值百分比小于0.05f（即5%），并且当前目标不是Boss,返回不爽
        if (GameObjectExtension. CurrentHpPercent (Core. Me. GetCurrTarget ()) < 0.05f
            && !TargetHelper. IsBoss (GameObjectExtension. GetCurrTarget (Core. Me)))
            return false;

        // 如果当前目标在10秒内会被击杀（TTK），并且当前目标是Boss，返回不爽
        // 用TTk里设置的值的话就把time改成SettingMgr.GetSetting<GeneralSettings>().TimeToKillCheckTime
        if (TTKHelper. IsTargetTTK (Core. Me. GetCurrTarget (), 10000, true)
            && TargetHelper. IsBoss (GameObjectExtension. GetCurrTarget (Core. Me)))
            return false;

        if (判断周围敌人TTK ())
            return false;

        return true;
        }

        // 新增方法，用于判断周围敌人的TTK
        private static bool 判断周围敌人TTK()
        {
            // 遍历25米范围内的所有敌人
            foreach (var enemy in TargetMgr.Instance.EnemysIn25)
            {
                // 检查每个敌人是否在指定时间内会被击杀
                if (TTKHelper.IsTargetTTK(enemy.Value, 10000, false) 
                    && !TargetHelper.IsBoss(GameObjectExtension.GetCurrTarget(Core.Me)))
                {
                    // 如果发现任何一个敌人在指定时间内会被击杀，返回不爽
                    return false;
                }
            }

            // 如果所有敌人都在指定时间内不会被击杀，返回爽
            return true;
        }
    public static bool CheckSchedule ()
    {
        // 获取 DutySchedule 对象
        var d = Core. Resolve<MemApiDuty> ()?.GetSchedule ();

        // 如果 d 为 null，默认返回 true
        if (d == null)
        {
            return true;
        }

        // 检查 CountPoint 和 NowPoint 的差值
        if (d. CountPoint - d. NowPoint > 1)
        {
            return false;
        }

        // 默认返回 true
        return true;
    }
    }

