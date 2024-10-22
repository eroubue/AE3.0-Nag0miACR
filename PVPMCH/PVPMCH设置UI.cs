using ImGuiNET;
using Nagomi.PVPMCH;

namespace Nagomi.PVPMCH.Settings
{
    public class PVPMCHSettingUI
    {
        public static PVPMCHSettingUI Instance = new();
        public PVPMCHSettings PVPMCHSettings => PVPMCHSettings.Instance;
    
        public void Draw()
        {
            //这里设置ui
            //ui类型请查询ImGui
            //ImGui.InputInt("目标剩余多少百分比血量时禁用读条画画", ref PVPMCHSettings.画画百分比);
            // ImGui.Checkbox("音效", ref PCTSettings.音效);
            // ImGui.Checkbox("智能aoe目标", ref PCTSettings.智能aoe目标);
   
            if (ImGui.Button("Save"))//保存按钮，不用动
            {
                PVPMCHSettings.Instance.Save();
            }
        }
    }
}