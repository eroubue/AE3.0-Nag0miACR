using System.Numerics;
using AEAssist;
using AEAssist.Define.HotKey;
using AEAssist.GUI;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using ImGuiNET;
using Nagomi.utils;
using Keys = AEAssist.Define.HotKey.Keys;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Nagomi.SGE.utils.JobView.HotKey;

/// 快捷键窗口类
public class HotkeyWindow(
    JobViewSave save,
    string name,
    ref Dictionary<string, HotKetSpell> config,
    Dictionary<string, uint> spell,
    Action saveAction)
{
    public JobViewSave save = save;
    /// 用于储存所有hotkey控件的字典
    private Dictionary<string, HotkeyControl> HotkeyDict = new();

    /// 处于激活状态的hotkey列表
    public List<string> ActiveList = [];

    /// 动态按顺序储存hotkey名称的list，用于排序显示hotkey
    public List<string> HotkeyNameList = [];
    // 记录控件名称和对应快捷键的字典
    public Dictionary<string, HotkeyConfig> HotkeyConfig => save.HotkeyConfig;
    
    ///窗口拖动
    public bool LockWindow;
    
    private Dictionary<string, long> lastActiveTime = new();

    public Dictionary<string, HotKetSpell> HotKeyConfigs = config;


    /// 隐藏的hotkey列表
    public List<string> HotkeyUnVisibleList => save.HotkeyUnVisibleList;

    ///hotkey按钮一行有几个
    public int HotkeyLineCount
    {
        get => save.HotkeyLineCount;
        set => save.HotkeyLineCount = value;
    }

    public void CreateHotkey()
    {
        if (HotKeyConfigs.Count <= 0) return;
        foreach (var kvs in HotKeyConfigs)
        {
            AddHotkey(kvs.Value.Name,
                new HotKeyResolver(kvs.Value.spell.GetSpell(), new HotKeyTarget(
                    HotKeyTargetConfig.List[kvs.Value.target].Name,
                    HotKeyTargetConfig.List[kvs.Value.target].SpellTargetType,
                    HotKeyTargetConfig.List[kvs.Value.target].Func,
                    HotKeyTargetConfig.List[kvs.Value.target].CharacterAgent)
                ));
        }
    }

    /// <summary>
    /// 添加新的Hotkey控件
    /// </summary>
    public void AddHotkey(string hotkeyName, IHotkeyResolver slot)
    {
        if (HotkeyDict.ContainsKey(hotkeyName))
            return;
        var hotkey = new HotkeyControl(hotkeyName);
        HotkeyDict.Add(hotkeyName, hotkey);
        HotkeyDict[hotkeyName].Slot = slot;
        if (!HotkeyNameList.Contains(hotkeyName))
        {
            HotkeyNameList.Add(hotkeyName);
        }
    }
    
    public void RemoveHotKey(string hotkeyName)
    {
        HotkeyDict.Remove(hotkeyName);
        HotkeyNameList.Remove(hotkeyName);
        HotkeyConfig.Remove(hotkeyName);
    }


    /// 设置上一次add添加的hotkey的toolTip
    public void SetHotkeyToolTip(string toolTip)
    {
        HotkeyDict[HotkeyDict.Keys.ToArray()[^1]].ToolTip = toolTip;
    }

    /// 返回包含当前所有hotkey名字的数组
    public string[] GetHotkeyArray()
    {
        return HotkeyDict.Keys.ToArray();
    }
    
    /// 获取Hotkey控件
    public HotkeyControl? GetHotkey(string name)
    {
        return HotkeyDict.TryGetValue(name, out var control) ? control : null;
    }

    /// 激活单个快捷键,mo无效
    public void SetHotkey(string hotkeyName)
    {
        if (!ActiveList.Contains(hotkeyName) && HotkeyDict.TryGetValue(hotkeyName, out var value))
        {
            //激活按钮
            RunSlot(value);
        }
    }


    public void DrawHotkeyWindow(QtStyle style)
    {
        
        HotKeyConfig.DrawHotKeyConfigView(this, ref HotKeyConfigs, spell,saveAction);
        
        if (!save.ShowHotkey)
            return;
        var num = HotkeyNameList.Count - HotkeyUnVisibleList.Count;
        if (num <= 0)
            return;

        var now = TimeHelper.Now();

        //更新按钮是否激活的状态
        for (int i = ActiveList.Count - 1; i >= 0; i--)
        {
            var hotkeyName = ActiveList[i];
            if (!HotkeyDict.ContainsKey(hotkeyName))
                continue;
            if (!lastActiveTime.TryGetValue(hotkeyName, out var lastActive))
            {
                continue;
            }

            // cd暂定0.5秒,也就是0.5秒内的重复点击无效
            if (now - lastActive >= 500)
            {
                ActiveList.RemoveAt(i);
            }
        }

        style.SetStyle();

        var line = num / HotkeyLineCount;
        if (num % HotkeyLineCount != 0)
            line++;
        var row = num < HotkeyLineCount ? num : HotkeyLineCount;

        var hotKeySize = style.OverlayScale * save.QtHotkeySize;


        var width = (row - 1) * 6 + 16 + hotKeySize.X * row;
        var height = (line - 1) * 6 + 16 + hotKeySize.Y * line;
        
        // 设置窗口大小和位置
        ImGui.SetNextWindowSize(new Vector2(width, (int)height));

        var flag = LockWindow ? QtStyle.QtWindowFlag | ImGuiWindowFlags.NoMove : QtStyle.QtWindowFlag;
        ImGui.Begin($"###Hotkey_Window{name}", flag);

        var index = 0;
        //画出控件
        for (int i = 0; i < HotkeyNameList.Count; i++)
        {

            var hotkeyName = HotkeyNameList[i];

            if (!HotkeyDict.ContainsKey(hotkeyName))
            {

                continue;
            }

            if (HotkeyUnVisibleList.Contains(hotkeyName))
            {
                continue;
            }


            var hotkey = HotkeyDict[hotkeyName];



            ImGui.BeginChild($"###Hotkey_HotkeyControl{name + i}", hotKeySize, false, QtStyle.QtWindowFlag);
            hotkey.Slot.Draw(hotKeySize);
            //鼠标点击
            if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
            {
                ImGui.BeginTooltip();
                ImGui.Text($"{hotkeyName}");
                if (hotkey.ToolTip != "")
                    ImGui.Text($"{hotkey.ToolTip}");
                ImGui.EndTooltip();

                if (ImGui.IsMouseClicked(ImGuiMouseButton.Left))
                {
                    RunSlot(hotkey);
                }
            }

            hotkey.Slot.DrawExternal(hotKeySize, ActiveList.Contains(hotkeyName));

            if (HotkeyConfig.TryGetValue(hotkeyName, out var hotkeyConfig) && hotkeyConfig.Keys != Keys.None)
            {
                //显示快捷键
                var text = HotkeySplice(hotkeyConfig.ModifierKey, hotkeyConfig.Keys);
                ImGui.SetCursorPos(new Vector2(0, 0));
                ImGui.Text(text);
            }

            ImGui.EndChild();

            if (index % HotkeyLineCount != HotkeyLineCount - 1)
            {
                ImGui.SameLine();
            }
            index++;
        }
        
        style.EndStyle();
        
        ImGui.End();
    }

    /// 用于draw一个更改qt排序显示等设置的视图
    public void HotkeySettingView()
    {
        ImGui.Checkbox("显示热键栏", ref save.ShowHotkey);
        ImGui.Checkbox("打开自定义Hotkey窗口", ref GlobalSetting.Instance.HotKey配置窗口);
        ImGui.TextDisabled("   *左键拖动改变hotkey顺序，右键点击显示更多");
        for (var i = 0; i < HotkeyNameList.Count; i++)
        {
            var item = HotkeyNameList[i];
            var visible = !HotkeyUnVisibleList.Contains(item) ? "显示" : "隐藏";

            if (visible == "隐藏")
                ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.6f, 0, 0, 1));

            ImGui.Selectable($"   {visible}        {item}");

            if (visible == "隐藏")
                ImGui.PopStyleColor(1);


            //排序        
            if (ImGui.IsItemActive() && !ImGui.IsItemHovered())
            {
                var n_next = i + (ImGui.GetMouseDragDelta(0).Y < 0f ? -1 : 1);
                if (n_next < 0 || n_next >= HotkeyNameList.Count)
                    continue;
                HotkeyNameList[i] = HotkeyNameList[n_next];
                HotkeyNameList[n_next] = item;
                ImGui.ResetMouseDragDelta();
            }

            //右键
            ImGui.PushStyleVar(ImGuiStyleVar.PopupBorderSize, 1);
            ImGui.PushStyleColor(ImGuiCol.Border, new Vector4(0.2f, 0.2f, 0.2f, 1));
            if (ImGuiHelper.IsRightMouseClicked())
                ImGui.OpenPopup($"###hotkeyPopup{name + i}");
            if (ImGui.BeginPopup($"###hotkeyPopup{name + i}"))
            {
                //显示隐藏
                var vis = !HotkeyUnVisibleList.Contains(item);
                if (ImGui.Checkbox("显示", ref vis))
                {
                    if (!vis)
                        HotkeyUnVisibleList.Add(item);
                    else
                        HotkeyUnVisibleList.Remove(item);
                }

                if (!HotkeyConfig.TryGetValue(item, out var hotkeyConfig))
                {
                    hotkeyConfig = new();
                    HotkeyConfig[item] = hotkeyConfig;
                }

                //快捷键设置
                ImGuiHelper.KeyInput("快捷键设置", ref hotkeyConfig.Keys);
                ImGuiHelper.DrawEnum("组合键", ref hotkeyConfig.ModifierKey);


                if (ImGui.Button("重置"))
                {
                    HotkeyConfig.Remove(item);
                }


                ImGui.EndPopup();
            }

            ImGui.PopStyleColor(1);
            ImGui.PopStyleVar(1);
        }
    }

    ///快捷键显示格式化
    private static string HotkeyFormat(Keys key)
    {
        return key switch
        {
            Keys.Space => "SPC",
            Keys.Oemtilde => "`",
            Keys.D1 => "1",
            Keys.D2 => "2",
            Keys.D3 => "3",
            Keys.D4 => "4",
            Keys.D5 => "5",
            Keys.D6 => "6",
            Keys.D7 => "7",
            Keys.D8 => "8",
            Keys.D9 => "9",
            Keys.D0 => "0",
            Keys.OemMinus => "-",
            Keys.Oemplus => "+",
            Keys.OemOpenBrackets => "[",
            Keys.Oem6 => "]",
            Keys.Oem5 => "\\",
            Keys.Oem1 => ";",
            Keys.Oem7 => "'",
            Keys.LShiftKey => "s",
            Keys.Oemcomma => ",",
            Keys.OemPeriod => ".",
            Keys.OemQuestion => "/",
            Keys.RShiftKey => "s",
            Keys.LControlKey => "c",
            Keys.LMenu => "a",
            Keys.RMenu => "a",
            Keys.RControlKey => "c",
            _ => $"{key}"
        };
    }

    public static string HotkeySplice(ModifierKey modifierKey, Keys key)
    {
        var md = modifierKey switch
        {
            ModifierKey.Alt => "Alt+",
            ModifierKey.Control => "Ctrl+",
            ModifierKey.Shift => "Shift+",
            _ => ""
        };
        return $"{md}{HotkeyFormat(key)}";
    }

    private void RunSlot(HotkeyControl hotkey)
    {
        if (hotkey.Slot.Check() < 0)
            return;

        if (!ActiveList.Contains(hotkey.Name))
        {
            ActiveList.Add(hotkey.Name);
            lastActiveTime[hotkey.Name] = TimeHelper.Now();
            hotkey.Slot.Run();
        }
    }

    /// <summary>
    /// 运行键盘快捷键模块,一般放在update中
    /// </summary>
    public void RunHotkey()
    {
        foreach (var hotkey in HotkeyConfig)
        {
            if (hotkey.Value.Keys == Keys.None) continue;
            if (Core.Resolve<MemApiHotkey>().CheckState(hotkey.Value.ModifierKey, hotkey.Value.Keys))
            {
                //激活按钮
                RunSlot(HotkeyDict[hotkey.Key]);
            }
        }
    }
}