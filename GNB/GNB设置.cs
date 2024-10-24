using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
using AEAssist;
namespace Nagomi.GNB;

public class GNBSettings
{
    public static GNBSettings Instance;
    /// <summary>
    /// 配置文件适合放一些一般不会在战斗中随时调整的开关数据
    /// 如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
    /// 非开关类型的配置都放配置里 比如诗人绝峰能量配置
    /// </summary>
    #region 标准模板代码 可以直接复制后改掉类名即可
    private static string path;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, nameof(GNBSettings), ".json");
        if (!File.Exists(path))
        {
            Instance = new GNBSettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<GNBSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }
    #endregion
//这里填写你需要的设置，bool=开关 int=数值
   // public bool 音效 = false;
    //public bool 智能aoe目标 = false;
    //public static int 画画百分比 = 10;

    public JobViewSave GNBViewSave = new(); // QT设置存档
}