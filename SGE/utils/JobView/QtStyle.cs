using System.Numerics;
using AEAssist.CombatRoutine;
using ImGuiNET;
using Nagomi.utils;

// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConvertToConstant.Global

namespace Nagomi.SGE.utils.JobView;

public class QtStyle
{
    // 保存对JobViewSave的引用
    public JobViewSave Save { get; }

    // 现代主题实例 - 从保存的设置中读取，而不是硬编码
    private ModernTheme modernTheme;
    public ModernTheme ModernTheme => modernTheme;

    // 标记现代主题是否已应用
    private bool isModernThemeApplied;

    // 主题预设
    private ModernTheme.ThemePreset lastTheme;

    /// <summary>
    /// 检测主题是否发生变化
    /// </summary>
    public bool CurrentThemeChanged
    {
        get
        {
            var changed = lastTheme != Save.CurrentTheme;
            return changed;
        }
    }

    /// <summary>
    /// 更新最后记录的主题
    /// </summary>
    public void UpdateLastTheme()
    {
        lastTheme = Save.CurrentTheme;
    }

    public ModernTheme.ThemePreset CurrentTheme
    {
        get => Save.CurrentTheme;
        set
        {
            if (Save.CurrentTheme != value)
            {
                Save.CurrentTheme = value;
                lastTheme = value;
                modernTheme.ApplyPreset(value);
            }
        }
    }

    // 构造函数中正确初始化主题
    public QtStyle(JobViewSave save)
    {
        this.Save = save;
        // 从保存的设置中读取主题，如果没有保存过则使用默认值
        var savedTheme = save.CurrentTheme;
        modernTheme = new ModernTheme(savedTheme);
        lastTheme = savedTheme; // 初始化lastTheme
    }

    public float OverlayScale => SettingMgr.GetSetting<GeneralSettings>().OverlayScale;
    public Vector2 QtButtonSize => Save.QtButtonSize * OverlayScale;

    public Vector2 QtButtonSizeOrigin
    {
        get => Save.QtButtonSize;
        set => Save.QtButtonSize = value;
    }

    public Vector2 HotkeySizeOrigin
    {
        get => Save.QtHotkeySize;
        set => Save.QtHotkeySize = value;
    }

    //标题栏风格 无滚动条 无标题 不可调整大小
    public const ImGuiWindowFlags QtWindowFlag =
        ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoTitleBar |
        ImGuiWindowFlags.NoResize;

    //默认风格
    public static Vector4 DefaultMainColor = new(161 / 255f, 47 / 255f, 114 / 255f, 0.8f);
    public static float DefaultQtWindowBgAlpha = 0.3f;
    public static Vector2 DefaultButtonSize = new(95, 40);
    public static Vector2 DefaultHotkeySize = new(56, 56);

    /// Qt窗口背景透明度
    public float QtWindowBgAlpha
    {
        get => Save.QtWindowBgAlpha;
        set => Save.QtWindowBgAlpha = value;
    }

    /// <summary>
    /// 初始化主窗口风格
    /// </summary>
    public void SetMainStyle()
    {
        if (!isModernThemeApplied)
        {
            modernTheme.Apply();
            isModernThemeApplied = true;
        }
    }

    /// <summary>
    /// 注销主窗口风格
    /// </summary>
    public void EndMainStyle()
    {
        if (isModernThemeApplied)
        {
            modernTheme.Restore();
            isModernThemeApplied = false;
        }
    }

    public void SetStyle()
    {
        ImGui.SetNextWindowSize(new Vector2(300, 450), ImGuiCond.FirstUseEver);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(8, 8));
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(6, 6));
        ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0, 0, 0, QtWindowBgAlpha));
    }

    public void EndStyle()
    {
        ImGui.PopStyleVar(2);
        ImGui.PopStyleColor(1);
    }


    public void Reset()
    {
        Save.CurrentTheme = ModernTheme.ThemePreset.深色模式;
        lastTheme = ModernTheme.ThemePreset.深色模式;
        modernTheme.ApplyPreset(ModernTheme.ThemePreset.深色模式);
        GlobalSetting.Instance.Save();
    }
}