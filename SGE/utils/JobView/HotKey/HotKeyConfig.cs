using System.Numerics;
using AEAssist.CombatRoutine;
using AEAssist.Helper;
using ImGuiNET;
using Nagomi.utils;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Nagomi.SGE.utils.JobView.HotKey;

public static class HotKeyConfig
{
    //public static Dictionary<string, uint> SpellList = HotKeySpellConfig.List;
    public static Dictionary<int, HotKeyTarget> targetList = HotKeyTargetConfig.List;

    private static string selectSpellName = "极光";
    private static uint selectSpell = 16151u;
    private static string? selectTargetName = "自己";
    private static int targetKey = 1;
    private static HotKeyTarget selectTarget = new("自己", SpellTargetType.Self);
    public static void DrawHotKeyConfigView(HotkeyWindow HotkeyWindow, ref Dictionary<string, HotKetSpell> HotkeyConfig,Dictionary<string, uint> Spell,Action save)
    {
        if (!GlobalSetting.Instance.HotKey配置窗口)
        {
            return;
        }
        ImGui.SetNextWindowSize(new Vector2(375f, 350f), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(new Vector2(50f, 50f), new Vector2(float.MaxValue, float.MaxValue));
        ImGui.Begin("HotkeyConfig", ref GlobalSetting.Instance.HotKey配置窗口);
        ImGui.Indent();

        if (HotkeyConfig.Count > 0)
        {
            foreach (var (key, value) in HotkeyConfig)
            {
                ImGui.Text($"使用技能:[{value.spell.GetSpell().Name}]");
                ImGui.SameLine();
                ImGui.Text($"对[{targetList[value.target].Name}]释放。");
                    
                if (ImGui.Button($"删除-{key}"))
                {
                    HotkeyWindow.RemoveHotKey(key);
                    HotkeyConfig.Remove(key);
                    save();
                }

            }
        }
        ImGui.Unindent();
        ImGui.Separator();

            

        ImGui.Text("使用:");
        ImGui.SetNextItemWidth(200f);
        ImGui.SameLine();
        if (ImGui.BeginCombo("###选择技能", selectSpellName))
        {
            foreach (var kvs in Spell)
            {
                if (ImGui.Selectable(kvs.Key))
                {
                    selectSpellName = kvs.Key;
                    selectSpell = kvs.Value;
                }

            }
            ImGui.EndCombo();
        }
            
        ImGui.Text("对");
        ImGui.SameLine();
        ImGui.SetNextItemWidth(200f);
        if (ImGui.BeginCombo("###选择目标", selectTargetName))
        {
            foreach (var kvs in targetList)
            {
                if (ImGui.Selectable(kvs.Value.Name))
                {
                    targetKey = kvs.Key;
                    selectTargetName = kvs.Value.Name;
                    selectTarget = kvs.Value;
                }

            }
            ImGui.EndCombo();
        }
        ImGui.SameLine();
        ImGui.Text("释放。");

        string NewHotkeyName = $"{selectSpellName}{selectTargetName}";
        if (HotkeyConfig.ContainsKey(NewHotkeyName))
        {
            ImGui.Text("该技能组合已存在!");
        }
        else
        {
            if (ImGui.Button("新增HOTKEY"))
            {
                HotkeyWindow.AddHotkey(NewHotkeyName, new HotKeyResolver(selectSpell.GetSpell(), selectTarget));
                HotkeyConfig.Add(NewHotkeyName, new HotKetSpell(NewHotkeyName, selectSpell, targetKey));
                save();
            }
        }

        ImGui.End();
    }

}