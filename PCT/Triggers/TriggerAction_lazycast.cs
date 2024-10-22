using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using ImGuizmoNET;

namespace Nagomi.PCT.Triggers;

public class TriggerAction_LazyCast : ITriggerAction
{
    public string DisplayName => "画家/惰性施法";
    public string Remark { get; set; }

    public bool LazycastEnabled { get; set; } = false;
    private int _提前 = 0; // 默认惰性施法时间为 0 秒
    private string? Preview;

    public bool Draw()
    {
        Preview = LazycastEnabled switch
        {
            false => "禁用",
            true => $"启用 ({_提前} 秒)"
        };

        if (ImGui.BeginCombo("", Preview))
        {
            if (ImGui.Selectable("禁用"))
            {
                LazycastEnabled = false;
            }
            if (ImGui.Selectable("启用"))
            {
                LazycastEnabled = true;
            }

            ImGui.EndCombo();
        }

        if (LazycastEnabled)
        {
            ImGui.NewLine();
            if (ImGui.SliderInt("团辅前多少秒不释放高威力能力技", ref _提前, 0, 20))
            {
                // 更新 提前
            }
        }

        return true;
    }

    public bool Handle()
    {
        PCTSettings.Instance.OpenLazy = LazycastEnabled;
        PCTSettings.Instance.团辅提前 = _提前;
        return true;
    }
}


