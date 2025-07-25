using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using Nagomi.PCT;
using Nagomi.SGE.Settings;

namespace Nagomi.SGE.Triggers;

public class 混合输血热键目标 : ITriggerAction
{
    public string DisplayName => "贤者/混合输血目标";
    public string Remark { get; set; }

    public bool save红豆 { get; set; } = false;
    private string? Preview;
    private int _混合输血目标 = 1; // 默认红豆保留数量为 1

    public bool Draw()
    {
      
        ImGui.SliderInt("PM", ref _混合输血目标 ,1, 8);
        
        return true;
    }

    public bool Handle()
    {
        SGESettings.Instance.混合输血目标 = _混合输血目标 ;
        return true;
    }
}
