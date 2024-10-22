using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using ImGuizmoNET;

namespace Nagomi.PCT.Triggers;

public class TriggerAction_SmartAoE : ITriggerAction
{
    public string DisplayName => "画家/智能aoe";
    public string Remark { get; set; }

    public bool 智能aoe { get; set; } = new();
    private string? Preview;

    public void Check()
    {

    }

    public bool Draw()
    {
        Preview = 智能aoe switch
        {
            false => "禁用",
            true => "启用",
        };

        if (ImGui.BeginCombo("", Preview))
        {
            if (ImGui.Selectable("禁用"))
            {
                智能aoe = false;
            }
            if (ImGui.Selectable("启用"))
            {
                智能aoe = true;
            }
            

            ImGui.EndCombo();
        }
        return true;
    }

    public bool Handle()
    {
       // PCTSettings.Instance.智能aoe目标 = 智能aoe;
        return true;
    }
}