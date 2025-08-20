using System.Numerics;
using AEAssist.Helper;
using ImGuiNET;

namespace Nagomi.SGE.utils.JobView;

/// <summary>
/// 现代化Qt窗口组件
/// </summary>
public static class ModernQtWindow
{
    private static ModernTheme? theme;
    private static Dictionary<string, float> buttonAnimations = new();
    private static Dictionary<string, long> buttonAnimationTimes = new();

    /// <summary>
    /// 确保theme不为null，如果为null则使用默认主题
    /// </summary>
    private static void EnsureThemeInitialized()
    {
        if (theme == null)
        {
            LogHelper.Debug("ModernQtWindow: theme为null，使用默认主题");
            theme = new ModernTheme();
        }
    }

    /// <summary>
    /// 绘制现代化Qt按钮
    /// </summary>
    public static bool DrawModernQtButton(string label, ref bool value, Vector2 size, Vector4? customColor = null)
    {
        EnsureThemeInitialized();

        switch (label)
        {
            case "GCD单体治疗":
                label = "GCD单奶";
                break;
            case "GCD群体治疗":
                label = "GCD群奶";
                break;
            case "能力技治疗":
                label = "能力技奶";
                break;
        }


        var buttonId = label + ImGui.GetID(label);

        // 初始化动画状态
        if (!buttonAnimations.ContainsKey(buttonId))
        {
            buttonAnimations[buttonId] = value ? 1f : 0f;
            buttonAnimationTimes[buttonId] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        // 更新动画
        UpdateButtonAnimation(buttonId, value);

        var animProgress = buttonAnimations[buttonId];
        var baseColor = customColor ?? theme.Colors.Primary;
        var currentColor = ModernTheme.BlendColor(
            theme.Colors.Surface,
            baseColor,
            animProgress
        );

        // 按钮样式
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, size.Y * 0.5f);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 0);
        ImGui.PushStyleColor(ImGuiCol.Button, currentColor);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, LightenColor(currentColor, 0.1f));
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, LightenColor(currentColor, 0.2f));

        var pos = ImGui.GetCursorScreenPos();

        // 绘制背景阴影
        if (value)
        {
            DrawButtonShadow(pos, size, animProgress);
        }

        // 按钮
        var clicked = ImGui.Button(label, size);
        if (clicked)
        {
            value = !value;
        }

        // 绘制状态指示器
        //DrawStateIndicator(pos, size, value, animProgress);

        // 绘制发光边框
        if (value)
        {
            DrawGlowBorder(pos, size, baseColor, animProgress);
        }

        ImGui.PopStyleColor(3);
        ImGui.PopStyleVar(2);

        return clicked;
    }

    /// <summary>
    /// 绘制现代化Qt窗口
    /// </summary>
    public static void DrawModernQtWindow(QtWindow qtWindow, QtStyle style, JobViewSave save)
    {
        if (!save.ShowQT)
            return;

        // 确保theme不为null
        EnsureThemeInitialized();

        // 动态获取主题，与主界面联动
        if (style.CurrentThemeChanged)
        {
            theme = new ModernTheme(style.CurrentTheme);
            if (style.CurrentThemeChanged)
            {
                style.UpdateLastTheme();
            }
        }

        if (!IsThemeMatching(style.CurrentTheme))
        {
            theme = new ModernTheme(style.CurrentTheme);
        }

        var qtNameList = qtWindow.QtNameList;
        var qtUnVisibleList = qtWindow.QtUnVisibleList;
        var visibleCount = qtNameList.Count - qtUnVisibleList.Count;

        if (visibleCount <= 0)
            return;

        // 计算窗口大小
        var qtLineCount = qtWindow.QtLineCount;
        var line = Math.Ceiling((float)visibleCount / qtLineCount);
        var row = visibleCount < qtLineCount ? visibleCount : qtLineCount;

        var buttonSize = style.QtButtonSize;
        var spacing = new Vector2(10, 10);
        var padding = new Vector2(15, 15);

        var windowWidth = padding.X * 2 + buttonSize.X * row + spacing.X * (row - 1);
        var windowHeight = padding.Y * 2 + buttonSize.Y * line + spacing.Y * (line - 1);

        // 设置窗口样式
        ImGui.SetNextWindowSize(new Vector2(windowWidth, (float)windowHeight));

        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 12f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, padding);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, spacing);
        ImGui.PushStyleColor(ImGuiCol.WindowBg, theme.Colors.Background with { W = style.QtWindowBgAlpha });
        ImGui.PushStyleColor(ImGuiCol.Border, theme.Colors.Border);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1f);

        var flag = qtWindow.LockWindow ? QtStyle.QtWindowFlag | ImGuiWindowFlags.NoMove : QtStyle.QtWindowFlag;
        ImGui.Begin($"###Qt_Window{qtWindow.name}", flag);


        // 绘制窗口背景效果
        DrawWindowBackground();

        // 绘制Qt按钮
        int index = 0;
        foreach (var qtName in qtNameList)
        {
            if (qtUnVisibleList.Contains(qtName))
                continue;

            var qtValue = qtWindow.GetQt(qtName);
            var customColor = GetQtCustomColor(qtName);

            if (DrawModernQtButton(qtName, ref qtValue, buttonSize, customColor))
            {
                qtWindow.SetQt(qtName, qtValue);
            }

            // 显示工具提示
            if (ImGui.IsItemHovered())
            {
                DrawModernTooltip(qtName, GetQtTooltip(qtName));
            }

            if (index % qtLineCount != qtLineCount - 1)
                ImGui.SameLine();

            index++;
        }

        ImGui.End();

        ImGui.PopStyleVar(4);
        ImGui.PopStyleColor(2);
    }

    /// <summary>
    /// 更新按钮动画
    /// </summary>
    private static void UpdateButtonAnimation(string buttonId, bool targetState)
    {
        var currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var lastTime = buttonAnimationTimes[buttonId];
        var deltaTime = (currentTime - lastTime) / 1000f;

        buttonAnimationTimes[buttonId] = currentTime;

        var current = buttonAnimations[buttonId];
        var target = targetState ? 1f : 0f;
        var animSpeed = 5f; // 动画速度

        if (Math.Abs(current - target) > 0.01f)
        {
            buttonAnimations[buttonId] = current + (target - current) * Math.Min(1f, deltaTime * animSpeed);
        }
        else
        {
            buttonAnimations[buttonId] = target;
        }
    }

    /// <summary>
    /// 绘制按钮阴影
    /// </summary>
    private static void DrawButtonShadow(Vector2 pos, Vector2 size, float intensity)
    {
        var drawList = ImGui.GetWindowDrawList();
        var shadowColor = new Vector4(0, 0, 0, 0.3f * intensity);
        var shadowOffset = new Vector2(0, 2);
        var shadowBlur = 4f;

        for (int i = 0; i < 3; i++)
        {
            var alpha = shadowColor.W * (1f - i / 3f);
            var offset = shadowOffset + new Vector2(0, i * shadowBlur);
            var blur = i * shadowBlur;

            drawList.AddRectFilled(
                pos + offset - new Vector2(blur),
                pos + size + offset + new Vector2(blur),
                ImGui.GetColorU32(shadowColor with { W = alpha }),
                size.Y * 0.5f + blur
            );
        }
    }

    /// <summary>
    /// 绘制发光边框
    /// </summary>
    private static void DrawGlowBorder(Vector2 pos, Vector2 size, Vector4 color, float intensity)
    {
        var drawList = ImGui.GetWindowDrawList();
        var time = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0f;
        var pulse = (float)(Math.Sin(time * 2) * 0.2 + 0.8);

        for (int i = 0; i < 2; i++)
        {
            var alpha = intensity * pulse * (0.3f - i * 0.1f);
            var offset = i * 2f;

            drawList.AddRect(
                pos - new Vector2(offset),
                pos + size + new Vector2(offset),
                ImGui.GetColorU32(color with { W = alpha }),
                size.Y * 0.5f,
                ImDrawFlags.None,
                2f
            );
        }
    }

    /// <summary>
    /// 绘制窗口背景效果
    /// </summary>
    private static void DrawWindowBackground()
    {
        EnsureThemeInitialized();

        var drawList = ImGui.GetWindowDrawList();
        var windowPos = ImGui.GetWindowPos();
        var windowSize = ImGui.GetWindowSize();

        // 绘制微妙的渐变背景
        ModernTheme.DrawGradient(
            windowPos,
            windowSize,
            theme.Colors.Background with { W = 0f },
            theme.Colors.Background with { W = 0.05f }
        );

        // 绘制装饰性图案
        var patternColor = theme.Colors.Primary with { W = 0.02f };
        for (int i = 0; i < 3; i++)
        {
            var offset = i * 50f;
            drawList.AddCircle(
                windowPos + new Vector2(windowSize.X - 30 - offset, 30 + offset),
                20f + i * 10f,
                ImGui.GetColorU32(patternColor),
                32,
                1f
            );
        }
    }

    /// <summary>
    /// 绘制现代化工具提示
    /// </summary>
    private static void DrawModernTooltip(string title, string content)
    {
        if (string.IsNullOrEmpty(content))
            return;

        EnsureThemeInitialized();

        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 8f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(12, 8));
        ImGui.PushStyleColor(ImGuiCol.PopupBg, theme.Colors.Surface);
        ImGui.PushStyleColor(ImGuiCol.Border, theme.Colors.Border);
        ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, 1f);

        ImGui.BeginTooltip();

        ImGui.TextColored(theme.Colors.Primary, title);
        ImGui.TextColored(theme.Colors.TextSecondary, content);

        ImGui.EndTooltip();

        ImGui.PopStyleVar(3);
        ImGui.PopStyleColor(2);
    }

    /// <summary>
    /// 获取Qt按钮的自定义颜色
    /// </summary>
    private static Vector4? GetQtCustomColor(string qtName)
    {
        EnsureThemeInitialized();

        if (qtName.Contains("治疗") || qtName.Contains("医"))
            return theme.Colors.Success;
        if (qtName.Contains("攻击") || qtName.Contains("输出"))
            return theme.Colors.Error;
        if (qtName.Contains("防御") || qtName.Contains("减伤"))
            return theme.Colors.Secondary;

        return null;
    }

    /// <summary>
    /// 获取Qt按钮的工具提示
    /// </summary>
    private static string GetQtTooltip(string qtName)
    {
        switch (qtName)
        {
            case "GCD单体治疗":
                return "实际QT名:GCD单体治疗";
            case "GCD群体治疗":
                return "实际QT名:GCD群体治疗";
            case "能力技治疗":
                return "实际QT名:能力技治疗";
        }

        return "";
    }

    /// <summary>
    /// 颜色变亮
    /// </summary>
    private static Vector4 LightenColor(Vector4 color, float amount)
    {
        return new Vector4(
            Math.Min(1f, color.X + amount),
            Math.Min(1f, color.Y + amount),
            Math.Min(1f, color.Z + amount),
            color.W
        );
    }

    /// <summary>
    /// 检查主题是否匹配
    /// </summary>
    private static bool IsThemeMatching(ModernTheme.ThemePreset targetTheme)
    {
        EnsureThemeInitialized();
        // 创建一个临时主题来比较颜色
        var tempTheme = new ModernTheme(targetTheme);
        return theme.Colors.Primary.Equals(tempTheme.Colors.Primary);
    }
}