// Decompiled with JetBrains decompiler
// Type: Linto.PvPSettings
// Assembly: Linto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDB54278-8776-48ED-88DE-295E616BB94A
// Assembly location: D:\XL\XIVLauncherCN\Roaming\devPlugins\3.0\ACR\Linto\Linto.dll

using AEAssist.Helper;
using AEAssist.IO;
using System;
using System.IO;

#nullable enable
namespace Nagomi
{
    public class PvPSettings
    {
        public static PvPSettings Instance;
        private static string path;
        public bool 自动选中 = true;
        public bool 技能自动选中 = true;
        public bool 最合适目标 = true;
        public bool 脱战嗑药 = true;

        public static void Build(string settingPath)
        {
            PvPSettings.path = Path.Combine(settingPath, "PvPSettings.json");
            if (!File.Exists(PvPSettings.path))
            {
                PvPSettings.Instance = new PvPSettings();
                PvPSettings.Instance.Save();
            }
            else
            {
                try
                {
                    PvPSettings.Instance = JsonHelper.FromJson<PvPSettings>(File.ReadAllText(PvPSettings.path));
                }
                catch (Exception ex)
                {
                    PvPSettings.Instance = new PvPSettings();
                    LogHelper.Error(ex.ToString());
                }
            }
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(PvPSettings.path));
            File.WriteAllText(PvPSettings.path, JsonHelper.ToJson((object) this));
        }
    }
}