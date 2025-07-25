using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace Nagomi.GNB.Settings
{
    public class GNBSettingUI
    {
        public static GNBSettingUI Instance = new();
        public GNBSettings GNBSettings => GNBSettings.Instance;
    
        public void Draw()
        {
            ImGui.Text("额外技能距离: " + GNBSettings.Instance.额外技能距离.ToString("F2"));
            ImGui.SameLine();
            ImGui.ProgressBar(GNBSettings.Instance.额外技能距离 / 3.0f, new Vector2(200, 0), "");
            ImGui.SliderFloat("额外技能距离", ref GNBSettings.Instance.额外技能距离, 0, 3);
            if (ImGui.Button("Save"))//保存按钮，不用动
            {
                GNBSettings.Instance.Save();
            }
            if (ImGui.Button("0"))
            {
                GNBSettings.Instance.保留子弹数 = 0;
            }
            ImGui.SameLine();
            if (ImGui.Button("1"))
            {
                GNBSettings.Instance.保留子弹数 = 1;
            }
            ImGui.SameLine();
            if (ImGui.Button("2"))
            {
                GNBSettings.Instance.保留子弹数 = 2;
            }
            ImGui.SameLine();
            ImGui.Text("当前保留子弹数：");
            ImGui.SameLine();
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0, 1, 0, 1));
            ImGui.Text($"{GNBSettings.Instance.保留子弹数}");
            ImGui.PopStyleColor();
        }
    }
    
}