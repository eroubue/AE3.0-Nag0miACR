using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;
using Nagomi.PCT;
using Nagomi.PCT.utils;



namespace Wotou.Dancer.Utility;

public class EnAvantHotkeyResolver: IHotkeyResolver
{
    private uint SpellId = PCTSpells.速涂;
    private float Rotation;
    
    public EnAvantHotkeyResolver(float rotation = 0)
    {
        this.Rotation = rotation;
    }
    
    public void Draw(Vector2 size)
    {
        uint id = Core.Resolve<MemApiSpell>().CheckActionChange(SpellId);
        Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(id, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size1);
        // Check if skill is on cooldown and apply grey overlay if true
        
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(SpellId).GetSpell().IsReadyWithCanCast())
        {
            // Use ImGui.GetItemRectMin() and ImGui.GetItemRectMax() for exact icon bounds
            Vector2 overlayMin = ImGui.GetItemRectMin();
            Vector2 overlayMax = ImGui.GetItemRectMax();

            // Draw a grey overlay over the icon
            ImGui.GetWindowDrawList().AddRectFilled(
                overlayMin, 
                overlayMax, 
                ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.5f))); // 50% transparent grey
        }
        
        var cooldownRemaining = SpellId.GetSpell().Cooldown.TotalMilliseconds / 1000;
        if (cooldownRemaining > 0)
        {
            // Convert cooldown to seconds and format as string
            string cooldownText = Math.Ceiling(cooldownRemaining).ToString();

            // 计算文本位置，向左下角偏移
            Vector2 textPos = ImGui.GetItemRectMin();
            textPos.X -= 1; // 向左移动一点
            textPos.Y += size1.Y - ImGui.CalcTextSize(cooldownText).Y + 5; // 向下移动一点

            // 绘制冷却时间文本
            ImGui.GetWindowDrawList().AddText(textPos, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 1)), cooldownText);
        }
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(Core.Resolve<MemApiSpell>().CheckActionChange(this.SpellId).GetSpell(), size, isActive);
    }

    public int Check()
    {
        // return 1;
        return SpellId.GetSpell().IsReadyWithCanCast() ? 0 : -1;
    }

    public void Run()
    {
        float rotation = CameraHelper.GetCameraRotation() + Rotation;

        Core.Resolve<MemApiMove>().SetRot(rotation);

        Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(this.SpellId).GetSpell(SpellTargetType.Self);
        if (AI.Instance.BattleData.NextSlot == null && PCTSpells.速涂.GetSpell().IsReadyWithCanCast())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(spell);
        }
        else
        {
            Slot slot = new Slot();
            slot.Add(spell);
            if (spell.IsAbility())
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(slot);
            else
                AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(slot);
        }
    }
}

public class JoystickWindow
{
    public float Rotation { get; private set; } = 0f; // 弧度
    private Vector2 center;
    private float radius = 60f;
    private bool isDragging = false;

    public void Draw()
    {
        ImGui.Begin("摇杆悬浮窗", ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoCollapse);

        Vector2 windowPos = ImGui.GetCursorScreenPos();
        center = windowPos + new Vector2(radius, radius);

        // 绘制圆盘
        ImGui.GetWindowDrawList().AddCircle(center, radius, ImGui.ColorConvertFloat4ToU32(new Vector4(0.5f, 0.5f, 0.5f, 1)), 64, 2f);

        // 处理鼠标拖动
        Vector2 mousePos = ImGui.GetIO().MousePos;
        bool hovered = (mousePos - center).Length() <= radius;
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Left) && hovered)
            isDragging = true;
        if (!ImGui.IsMouseDown(ImGuiMouseButton.Left))
            isDragging = false;

        if (isDragging)
        {
            Vector2 dir = mousePos - center;
            if (dir.Length() > 0.01f)
                Rotation = MathF.Atan2(dir.Y, dir.X); // 弧度
        }

        // 绘制指示线
        Vector2 end = center + new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation)) * radius;
        ImGui.GetWindowDrawList().AddLine(center, end, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 0, 0, 1)), 3f);

        // 显示角度
        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + radius * 2 + 10);
        ImGui.Text($"当前角度: {Rotation * 180 / MathF.PI:F1}°");

        ImGui.End();
    }

    // 可选：静态实例供主窗口调用
    public static JoystickWindow Instance { get; } = new JoystickWindow();
    public static void DrawStatic() => Instance.Draw();

    // 联动EnAvantHotkeyResolver
    public EnAvantHotkeyResolver GetEnAvantHotkeyResolver()
    {
        return new EnAvantHotkeyResolver(this.Rotation);
    }
}
public class JoystickHotkeyWindow : HotkeyWindow
{
    public float Rotation { get; private set; } = 0f;
    private Vector2 center;
    private float radius = 60f;
    private float knobRadius => radius * 0.3f;
    private bool isDragging = false;
    private Vector2 knobPos;
    private Action<float>? onRelease;
    private float minRadius = 40f, maxRadius = 120f;

    public JoystickHotkeyWindow(JobViewSave save, string name, Action<float>? onRelease = null) : base(save, name)
    {
        this.onRelease = onRelease;
        knobPos = Vector2.Zero;
    }

    public void DrawHotkeyWindow(QtStyle style)
    {
        ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0, 0, 0, 0)); // 透明背景

        ImGuiWindowFlags flags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize;
        ImGui.Begin("摇杆悬浮窗", flags);

        
        ImGui.SameLine();
        ImGui.Text("  ");
        ImGui.SameLine();
        ImGui.Text("大小:");
        ImGui.SameLine();
        if (ImGui.ArrowButton("##减小", ImGuiDir.Left) && radius > minRadius)
            radius -= 5f;
        ImGui.SameLine();
        if (ImGui.ArrowButton("##增大", ImGuiDir.Right) && radius < maxRadius)
            radius += 5f;

        Vector2 windowPos = ImGui.GetCursorScreenPos();
        center = windowPos + new Vector2(radius, radius);

        ImGui.GetWindowDrawList().AddCircle(center, radius, ImGui.ColorConvertFloat4ToU32(new Vector4(0.5f, 0.5f, 0.5f, 1)), 64, 2f);

        Vector2 mousePos = ImGui.GetIO().MousePos;
        bool hovered = (mousePos - center).Length() <= radius;
        if (!PCTSettings.Instance.isEnAvantPanelLocked && ImGui.IsMouseClicked(ImGuiMouseButton.Left) && hovered)
            isDragging = true;

        if (!ImGui.IsMouseDown(ImGuiMouseButton.Left) && isDragging)
        {
            isDragging = false;
            knobPos = Vector2.Zero;
            onRelease?.Invoke(Rotation);
        }

        if (isDragging)
        {
            Vector2 dir = mousePos - center;
            float len = MathF.Min(dir.Length(), radius - knobRadius);
            if (len > 0.01f)
            {
                Vector2 norm = Vector2.Normalize(dir);
                knobPos = norm * len;
                Rotation = MathF.Atan2(norm.Y, norm.X);
            }
        }
        else
        {
            knobPos = Vector2.Zero;
        }

        Vector2 end = center + new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation)) * (radius - knobRadius);
        ImGui.GetWindowDrawList().AddLine(center, end, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 0, 0, 1)), 3f);
        ImGui.GetWindowDrawList().AddCircleFilled(center + knobPos, knobRadius, ImGui.ColorConvertFloat4ToU32(new Vector4(0.9f, 0.2f, 0.2f, 0.9f)), 32);

        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + radius * 2 + 10);
        ImGui.Text($"当前角度: {Rotation * 180 / MathF.PI:F1}°");

        ImGui.End();
        ImGui.PopStyleColor();
    }

    public static JoystickHotkeyWindow JoystickPanel = new JoystickHotkeyWindow(PCTSettings.Instance.JobViewSave, "摇杆HotkeyWindow");
}

/*public class JoystickHotkeyWindow : HotkeyWindow
{
    public float Rotation { get; private set; } = 0f;
    private Vector2 center;
    private float radius = 60f;
    private bool isDragging = false;

    public JoystickHotkeyWindow(JobViewSave save, string name) : base(save, name) { }

    public void DrawHotkeyWindow(QtStyle style)
    {
        ImGui.Begin("摇杆悬浮窗", ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoCollapse);

        Vector2 windowPos = ImGui.GetCursorScreenPos();
        center = windowPos + new Vector2(radius, radius);

        ImGui.GetWindowDrawList().AddCircle(center, radius, ImGui.ColorConvertFloat4ToU32(new Vector4(0.5f, 0.5f, 0.5f, 1)), 64, 2f);

        Vector2 mousePos = ImGui.GetIO().MousePos;
        bool hovered = (mousePos - center).Length() <= radius;
        if (ImGui.IsMouseClicked(ImGuiMouseButton.Left) && hovered)
            isDragging = true;
        if (!ImGui.IsMouseDown(ImGuiMouseButton.Left))
            isDragging = false;

        if (isDragging)
        {
            Vector2 dir = mousePos - center;
            if (dir.Length() > 0.01f)
                Rotation = MathF.Atan2(dir.Y, dir.X);
        }

        Vector2 end = center + new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation)) * radius;
        ImGui.GetWindowDrawList().AddLine(center, end, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 0, 0, 1)), 3f);

        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + radius * 2 + 10);
        ImGui.Text($"当前角度: {Rotation * 180 / MathF.PI:F1}°");

        ImGui.End();
    }

    public static JoystickHotkeyWindow Instance = new JoystickHotkeyWindow(PCTSettings.Instance.JobViewSave, "摇杆HotkeyWindow");
}*/

public static class JoystickHotkeyWindowManager
{
    public static JoystickHotkeyWindow Instance = new JoystickHotkeyWindow(PCTSettings.Instance.JobViewSave, "摇杆HotkeyWindow");

    public static void DrawOrUpdateHotkeyWindow(QtStyle style)
    {
        // 可根据设置变化重建实例
        var viewSave = new JobViewSave();
        viewSave.QtHotkeySize = new Vector2(60, 60); // 可用设置
        viewSave.ShowHotkey = true;
        viewSave.LockWindow = false;
        Instance = new JoystickHotkeyWindow(viewSave, "摇杆HotkeyWindow");
        Instance.DrawHotkeyWindow(style);
    }
}