using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace Millusion.ACR.Astrologian.Setting;

/// <summary>
///     配置文件适合放一些一般不会在战斗中随时调整的开关数据
///     如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
///     非开关类型的配置都放配置里 比如诗人绝峰能量配置
/// </summary>
public class AST_Settings
{
    public static AST_Settings Instance { get; private set; }
    private static string _path;
    public JobViewSave JobViewSave = new(); // QT设置存档

    /// <summary>
    ///     ACR模式，1为日随，2为高难
    /// </summary>
    public int ACRMode = 1;

    public List<Jobs> BalanceCardTargetList = [];

    public List<Jobs> SpareCardTargetList = [];

    /// <summary>
    ///     太阳神优先级目标
    /// </summary>
    public string BalanceTargetName = "优先级目标";

    /// <summary>
    ///     放浪神优先级目标
    /// </summary>
    public string SpareTargetName = "优先级目标";

    /// <summary>
    ///     是否优先把地星放在场地中心
    /// </summary>
    public bool EarthlyStarPriorityCenter = true;

    /// <summary>
    ///     地星放置目标,当前目标或自己
    /// </summary>
    public int EarthlyStarTarget = (int)SpellTargetType.Target;

    /// <summary>
    ///     是否自动选择重力目标
    /// </summary>
    public bool GravityAutoTarget = true;

    /// <summary>
    ///     醒梦使用蓝量
    /// </summary>
    public int LucidDreamingMp = 7000;

    /// <summary>
    ///     最大治疗百分比，及直接治疗技能不会让治疗量超过这个百分比太多，会有小幅度变动
    /// </summary>
    public float MaxHealPercent = 0.9f;

    /// <summary>
    ///     是否自动光速
    /// </summary>
    public bool MovedLightspeed = false;

    /// <summary>
    ///     是否强制治疗当前选择的目标
    /// </summary>
    public bool ForceHealTarget = false;

    /// <summary>
    ///     移动中Dot使用延迟
    /// </summary>
    public int MovedDotDelay = 1000;


    public static void Build(string settingPath)
    {
        // LogHelper.Info(settingPath);
        // Path.GetDirectoryName(settingPath);
        var defaultSetting = new AST_DefaultSetting();
        _path = Path.Combine(settingPath, nameof(AST_Settings) + ".json");
        if (!File.Exists(_path))
        {
            Instance = new AST_Settings
            {
                BalanceCardTargetList = defaultSetting.BalanceCardTargetList,
                SpareCardTargetList = defaultSetting.SpareCardTargetList
            };
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<AST_Settings>(File.ReadAllText(_path));
            if (Instance.BalanceCardTargetList == null || Instance.BalanceCardTargetList.Count !=
                defaultSetting.BalanceCardTargetList.Count)
                Instance.BalanceCardTargetList = defaultSetting.BalanceCardTargetList;

            if (Instance.SpareCardTargetList == null || Instance.SpareCardTargetList.Count !=
                defaultSetting.SpareCardTargetList.Count)
                Instance.SpareCardTargetList = defaultSetting.SpareCardTargetList;

            // LogHelper.Print(Instance.BalanceCardTargetList?.Count.ToString() ?? string.Empty);
        }
        catch (Exception e)
        {
            Instance = new AST_Settings
            {
                BalanceCardTargetList = defaultSetting.BalanceCardTargetList,
                SpareCardTargetList = defaultSetting.SpareCardTargetList
            };
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_path));
        File.WriteAllText(_path, JsonHelper.ToJson(this));
    }
}