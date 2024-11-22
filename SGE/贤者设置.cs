using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace Nagomi.SGE.Settings;

public class SGESettings
{
    public static SGESettings Instance;

    #region 标准模板代码 可以直接复制后改掉类名即可
    private static string path;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, nameof(SGESettings), "SGESettings.json");
        if (!File.Exists(path))
        {
            Instance = new SGESettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<SGESettings>(File.ReadAllText(path));
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

    
    public bool H1 = false;
    public float 额外技能距离 = 0;
    public int 失衡走位 = 0;
    public int 即刻贤炮 = 0;
    public float 不上dot阈值 = 0.03f;
    public int opener = 0;
    public int 预读时间 = 1500;
    public int 红豆保留数量 = 1;
    public int 发炎保留数量 = 1;
    public string targetName = "";
    //public bool 智能aoe目标 = false;

    public JobViewSave JobViewSave = new(){ MainColor = new Vector4(40 / 255f, 173 / 255f, 70 / 255f, 0.8f) }; // QT设置存档

}