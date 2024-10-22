using System;
using System.IO;
using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace Nagomi.PCT
{
    /// <summary>
    /// 配置文件适合放一些一般不会在战斗中随时调整的开关数据
    /// 如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
    /// 非开关类型的配置都放配置里 比如诗人绝峰能量配置
    /// </summary>
    public class PCTSettings
    {
        public static PCTSettings Instance;

        #region 标准模板代码 可以直接复制后改掉类名即可
        private static string path;
        public static void Build(string settingPath)
        {
            path = Path.Combine(settingPath, nameof(PCTSettings), ".json");
            if (!File.Exists(path))
            {
                Instance = new PCTSettings();
                Instance.Save();
                return;
            }
            try
            {
                Instance = JsonHelper.FromJson<PCTSettings>(File.ReadAllText(path));
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

        public bool 音效 = false;
        //public bool 智能aoe目标 = false;
        public static int 画画百分比 = 10;
        public float Volume = 0.5f;
        public bool OpenRush = true;
        public int OpenRushTime = 200;
        public bool OpenLazy = true;
        public int 团辅提前 = 25;
        public int opener = 0;

        public JobViewSave JobViewSave = new(){MainColor = new Vector4(40 / 255f, 173 / 255f, 70 / 255f, 0.8f)}; // QT设置存档

    }
}