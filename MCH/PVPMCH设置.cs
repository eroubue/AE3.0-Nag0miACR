using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
using AEAssist;
namespace Nagomi.PVPMCH;

public class PVPMCHSettings
{
    public static PVPMCHSettings Instance;
    /// <summary>
    /// 配置文件适合放一些一般不会在战斗中随时调整的开关数据
    /// 如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
    /// 非开关类型的配置都放配置里 比如诗人绝峰能量配置
    /// </summary>
    #region 标准模板代码 可以直接复制后改掉类名即可
    private static string path;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, nameof(PVPMCHSettings), ".json");
        if (!File.Exists(path))
        {
            Instance = new PVPMCHSettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<PVPMCHSettings>(File.ReadAllText(path));
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


    public int 药血量 = 70;
    public bool 脱战嗑药 = true;
    public bool 钻头分析 = true;
    public bool 毒菌分析 = false;
    public bool 回转飞锯分析 = true;
    public bool 空气锚分析 = false;
    public bool TargetDefend = true;
    public bool 分析可用 = true;
    public bool 过热野火 = true;
    public JobViewSave JobViewSave = new JobViewSave()
    {
        MainColor = new Vector4(0.7294118f, 0.549019635f, 0.384313732f, 0.8f)
    };

    
}