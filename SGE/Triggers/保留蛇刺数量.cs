using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using Nagomi.PCT;
using Nagomi.SGE.Settings;

namespace Nagomi.SGE.Triggers;

public class TriggerAction_保留蛇刺数量 : ITriggerAction
{
    public string DisplayName => "贤者/一直保留红豆";
    public string Remark { get; set; }

    public bool save红豆 { get; set; } = false;
    private string? Preview;
    private int _红豆保留数量 = 1; // 默认红豆保留数量为 1

    public bool Draw()
    {
        Preview = save红豆 switch
        {
            false => "禁用",
            true => $"启用"
        };

        if (ImGui.BeginCombo("是否开启", Preview))
        {
            if (ImGui.Selectable("禁用"))
            {
                save红豆 = false;
            }
            if (ImGui.Selectable("启用"))
            {
                save红豆 = true;
            }

            ImGui.EndCombo();
        }

        if (save红豆)
        {
            ImGui.NewLine();
            if (ImGui.SliderInt("红豆保留数量", ref _红豆保留数量, 1, 3))
            {
                // 更新 红豆保留数量
            }
        }

        return true;
    }

    public bool Handle()
    {
        SGESettings.Instance.红豆保留数量 = _红豆保留数量;
        return true;
    }
}
