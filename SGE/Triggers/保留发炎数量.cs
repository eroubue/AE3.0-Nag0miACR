using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using Nagomi.PCT;
using Nagomi.SGE.Settings;

namespace Nagomi.SGE.Triggers;

public class TriggerAction_保留发炎数量 : ITriggerAction
{
    public string DisplayName => "贤者/保留发炎";
    public string Remark { get; set; }

    public bool save发炎 { get; set; } = false;
    private string? Preview;
    private int _发炎保留数量 = 1; // 默认发炎保留数量为 1

    public bool Draw()
    {
        Preview = save发炎 switch
        {
            false => "禁用",
            true => $"启用"
        };

        if (ImGui.BeginCombo("是否开启", Preview))
        {
            if (ImGui.Selectable("禁用"))
            {
                save发炎 = false;
            }
            if (ImGui.Selectable("启用"))
            {
                save发炎 = true;
            }

            ImGui.EndCombo();
        }

        if (save发炎)
        {
            ImGui.NewLine();
            if (ImGui.SliderInt("发炎保留数量", ref _发炎保留数量, 1, 2))
            {
                // 更新 发炎保留数量
            }
        }

        return true;
    }

    public bool Handle()
    {
        SGESettings.Instance.发炎保留数量 = _发炎保留数量;
        return true;
    }
}
