using System.Numerics;
using AEAssist.Helper;
using AEAssist.IO;

// ReSharper disable MemberCanBePrivate.Global
namespace Nagomi.SGE.utils;

public class GlobalSetting
{
    public static GlobalSetting? Instance = null;

    private static string path;
    public bool HotKey配置窗口 = false;
    public static string title = "";
    public static string desc = "";
    public bool QtShow = true;
    public bool HotKeyShow = true;
    public bool TempQtShow = true;
    public bool TempHotShow = true;
    public static bool QT快捷栏随主界面隐藏 = true;
    public bool 缩放同时隐藏QT = false;
    public bool 缩放同时隐藏Hotkey = false;
    public Vector2 缩放后窗口大小 = new Vector2(300f, 100f);
    public bool 关闭动效 = false;


    public static string StyleStr =
        "DS1H4sIAAAAAAAACqVY23LbNhD9FQ+fHQ8JEACpt9hq484kHU/sNE3fIAmWWNGiSlHKxZN/7wLEgiBIJbLiFwjgnoO9YbHwcySjSXIVX0azaPIc/R1NMj35ZMbvl9E8msR6YWFHZaViKxVfMZB6BI7LaGmXV3Ys7Chn9se/FpxaMDVbrO3X0o5PgSKpkdoE2Ha1ClaJWd0OlNSr/8FnY2kN2pofOyvSwIL5sbcLB8v42c6/2PGrI2ae9d9Gt5PSLtOetfMK7HyOHtSXxn5P7Hcz/mPHj2YEeS04LXZyVqrFcHcDMKMDfCw2i+rz9dIJJ4SleUwshMQiS2mSWiQV5is1BPFVHqe5ECnQ3KyKcuGxONMssNWxNfWu2u63vmyGwhlKZ7iB5r6u6oWqnTjJOBEpQw0ZT0XMcovUyoKS3LOwhd+vJNj5E/1awO+1fFKefiSPk5wn6BGcGZybGXXTDn1bHVTtxyBFfdGX+keIej1vikN3aDjNScKERbqZgbuZ4TBRoiYUD0VT+uonQuScEae/yBghOWqRwkcqcufwmGUZobzjCVQ63ftjZDdVWcrtzvPLmXzv1GZ/LWvfzKRNTkuUcEPMMU7c/I2YeT+vQalZj+tH2evk39S6UllE2pqBVujQcLQCY+OlWI9kkCrU/HEXdgbhckcQ1NbzY2RBuF7KdbNS8/U7Wa+DagNWcFMK0J+Q9Xruq1EWcM56Xjma9CEizHuX8i7bhX+i901TbY6cT6oDy1BPE5VkgA1d3tU1w5EirOXwzrhPEp6Ml6hxq6Rf01hGRRJr737Caexy181MDtLEwQeJM1rnIcwOEfoZ3Yxq+sFRW1nLpvIKL8W0thaatHYm4kUxZAj1PJso9PgLed6rXfFNvamL7sY/n+J8o2AGghmnPb6zbYOP+kxDmdd11jt9CdzNhBJ3lecij2PukiMmhBOXIqYeMnN/DCtSDNkb5xnmdtKeFjLeFPSpArNewGQbGjn7sHms5nv/0kiEviYwe0nLiST6AgEN0iMkv6rQtJqvi83yrlaHQnUNRdKWR+dsF75Wp6FvLM9vT9vmq99sIAEWWm/ru7Jq3hYbtesOMOrtGpJenXKAfkC7ou7qOWUB7LbYNdUSuhK3F6aiGGmZeohjm2GV6fWrulNta5N/l7tbPB9xg8HYrq6pq43nPJZAMHOXGDClHLelgjHO+DjR22K5wg5bR8+FEfdnIe59r2v+UcfQib8uf9bFA4LbNv5elWreKL+r1p2QbTDsPq5xo/ppMa3lclpX2wdZL9WxrTrlcoD8KQ+3YHvZt//YPq39gGmfDZC/Ifi4YSJATosnzzR8A+ATAO0iutmrFrJscaeBwBn6DQgtdTSJ/pKbArrOiw9/XLy6uCmVrC+uy72K4JXaPq7kIFOD5tE9iZwHuiIrglPh9ynwGj5B6rR3mjrp+ffYuUY45wjnHv3LyS6HLV6M4fY5V4Mk5yM7FwMp3DVpW1BPFh/2ekdbqo8+xtZHs9gv8GXHGLoa3j6eYFfOcFP3EPKkut4SrtXRw9PK4f8TOqNT2rqw75yu34BXW/eyRg17zoH/OwRhYTEWd5+z68uyDI9r6jg580PdXRiZPsi98Bh2KwmiULph9fv/zDrolOkRAAA=";


    public static void Build(string settingPath, string t, bool rebuild)
    {
        if (!rebuild)
        {
            if (Instance != null) return;
        }

        init(settingPath, t);
    }

    private static void init(string settingPath, string t)
    {
        title = t;
        path = Path.Combine(settingPath, "SGEGlobalSettings.json");
        if (!File.Exists(path))
        {
            Instance = new GlobalSetting();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<GlobalSetting>(File.ReadAllText(path));
        }
        catch (Exception ex)
        {
            Instance = new GlobalSetting();
            LogHelper.Error(ex.ToString());
        }
        finally
        {
            Instance.TempQtShow = true;
            Instance.TempHotShow = true;
        }
    }


    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? throw new InvalidOperationException());
            File.WriteAllText(path, JsonHelper.ToJson(this));
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.ToString());
        }
    }
}