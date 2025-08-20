using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.GUI;
using AEAssist.Helper;
using ImGuiNET;
using Millusion.ACR.Astrologian.Setting;

namespace Millusion.ACR.Astrologian.UI.Tab;

public static class SettingUI
{
    public static void Draw(JobViewWindow jobViewWindow)
    {
        ImGui.Text("ACR模式: 暂时无效");
        if (ImGui.RadioButton("日随模式", ref AST_Settings.Instance.ACRMode, 1))
            AST_Settings.Instance.Save();
        ImGui.SameLine();
        if (ImGui.RadioButton("高难模式", ref AST_Settings.Instance.ACRMode, 2))
            AST_Settings.Instance.Save();
        ImGui.Separator();
        ImGui.Text("地星设置:");
        if (ImGui.Checkbox("优先场中", ref AST_Settings.Instance.EarthlyStarPriorityCenter)) AST_Settings.Instance.Save();

        ImGui.SameLine();
        if (ImGui.RadioButton("目标位置", ref AST_Settings.Instance.EarthlyStarTarget, (int)SpellTargetType.Target))
            AST_Settings.Instance.Save();

        ImGui.SameLine();
        if (ImGui.RadioButton("自身位置", ref AST_Settings.Instance.EarthlyStarTarget, (int)SpellTargetType.Self))
            AST_Settings.Instance.Save();

        if (ImGuiHelper.LeftInputInt("醒梦阈值:", ref AST_Settings.Instance.LucidDreamingMp, 0, 10000, 100))
            AST_Settings.Instance.Save();

        if (ImGui.Checkbox("智能AOE目标", ref AST_Settings.Instance.GravityAutoTarget))
            AST_Settings.Instance.Save();

        if (ImGui.Checkbox("强制治疗当前目标", ref AST_Settings.Instance.ForceHealTarget))
            AST_Settings.Instance.Save();
        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.Text("用于特殊情况，比如需要治疗NPC时开启，其他情况不建议开启(只会单体GCD治疗和先天)");
            ImGui.EndTooltip();
        }

        if (ImGuiHelper.LeftInputInt("移动中使用DOT延迟(ms):", ref AST_Settings.Instance.MovedDotDelay, 0, 10000, 1000))
            AST_Settings.Instance.Save();

        if (ImGui.Checkbox("移动时光速", ref AST_Settings.Instance.MovedLightspeed))
            AST_Settings.Instance.Save();
        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.Text("开启后，光速将在boss战中移动(GCD冷却为0)时卡GCD使用");
            ImGui.EndTooltip();
        }

        ImGui.Separator();
        var partyList = PartyHelper.Party.Select(p => p.Name.ToString()).ToList();
        List<string> list = ["优先级目标"];
        var items = list.Concat(partyList).ToArray();
        DrawCardCombo("太阳神指定目标:", ref AST_Settings.Instance.BalanceTargetName, items);
        DrawCardCombo("放浪神指定目标:", ref AST_Settings.Instance.SpareTargetName, items);
        if (ImGui.CollapsingHeader("卡牌目标优先级设置"))
        {
            ImGui.BeginTable("CardTargetTable", 2, ImGuiTableFlags.Resizable);
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.Text("太阳神优先级");
            ImGui.TableNextColumn();
            ImGui.Text("放浪神优先级");
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            if (ImGui.Button("重置##Balance"))
            {
                AST_Settings.Instance.BalanceCardTargetList = new AST_DefaultSetting().BalanceCardTargetList;
                AST_Settings.Instance.Save();
            }

            ImGui.TableNextColumn();
            if (ImGui.Button("重置##Spare"))
            {
                AST_Settings.Instance.SpareCardTargetList = new AST_DefaultSetting().SpareCardTargetList;
                AST_Settings.Instance.Save();
            }

            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            if (DrawJobsList(AST_Settings.Instance.BalanceCardTargetList, "太阳神优先级"))
                AST_Settings.Instance.Save();
            ImGui.TableNextColumn();
            if (DrawJobsList(AST_Settings.Instance.SpareCardTargetList, "放浪神优先级"))
                AST_Settings.Instance.Save();
            ImGui.EndTable();
        }
    }

    private static void DrawCardCombo(string label, ref string name, string[] items)
    {
        var select = 0;
        var hasName = false;
        for (var i = 0; i < items.Length; i++)
            if (items[i] == name)
            {
                select = i;
                hasName = true;
                break;
            }

        // if (select == 0) name = items[0];
        ImGui.Text(label);
        ImGui.SameLine();
        ImGui.PushID("##" + label);
        ImGui.SetNextItemWidth(200);
        if (ImGui.Combo("", ref select, items, items.Length))
        {
            name = items[select];
            AST_Settings.Instance.Save();
        }

        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.Text("选择指定目标为卡牌目标，目标不存在时会选择优先级目标");
            ImGui.EndTooltip();
        }

        ImGui.PopID();
        ImGui.SameLine();
        if (!hasName)
            ImGui.Text("指定目标: " + name + " 不存在，已默认选择优先级目标");
        else
            ImGui.Text("指定目标: " + name);
    }

    private static bool DrawJobsList(List<Jobs> items, string key)
    {
        var r = false;
        for (var i = 0; i < items.Count; i++)
        {
            var str = JobHelper.GetTranslation(items[i]);
            var padding = 4 - str.Length;
            if (padding > 0) str += new string(' ', padding * 4);

            ImGui.Text(str);

            ImGui.SameLine();
            if (ImGui.Button($"Up##{key}{i}") && i > 0)
            {
                // 交换当前项和上一项
                (items[i], items[i - 1]) = (items[i - 1], items[i]);
                r = true;
            }

            ImGui.SameLine();
            if (ImGui.Button($"Down##{key}{i}") && i < items.Count - 1)
            {
                // 交换当前项和下一项
                (items[i], items[i + 1]) = (items[i + 1], items[i]);
                r = true;
            }
        }

        return r;
    }
}