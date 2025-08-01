using ImGuiNET;
using System.Numerics;

namespace Nagomi.GNB.Settings
{
    public interface ITriggerSettingUI
    {
        string Name();
        
        void Draw();
    
        void Handle();
    
    }
    public class GNB通用设置UI : ITriggerSettingUI
    {
        public string Name() => "绝枪通用设置";

        public void Draw()
        {
            // MT/ST 切换
            ImGui.Text("MT or ST");
            ImGui.SameLine();
            if (GNBSettings.Instance.ST)
            {
                ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), "ST");
            }
            else
            {
                ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), "MT");
            }
            ImGui.SameLine();
            if (ImGui.Button(" ST "))
            {
                GNBSettings.Instance.ST = true;
                GNBSettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button(" MT "))
            {
                GNBSettings.Instance.ST = false;
                GNBSettings.Instance.Save();
            }

            // 起手方案
            ImGui.Text("当前起手：");
            ImGui.SameLine();
            string openerName = GNBSettings.Instance.opener switch
            {
                1 => "零弹120起手",
                2 => "二弹120起手",
                3 => "神兵起手",
                4 => "2g无情起手",
                _ => "未知"
            };
            ImGui.TextColored(new Vector4(0.0f, 0.3f, 0.8f, 1.0f), openerName);

            if (ImGui.Button("零弹无情起手"))
            {
                GNBSettings.Instance.opener = 1;
                GNBSettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("二弹无情起手"))
            {
                GNBSettings.Instance.opener = 2;
                GNBSettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("2g无情起手"))
            {
                GNBSettings.Instance.opener = 4;
                GNBSettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("神兵起手"))
            {
                GNBSettings.Instance.opener = 3;
                GNBSettings.Instance.Save();
            }

            // 额外技能距离
            ImGui.Text("额外技能距离: " + GNBSettings.Instance.额外技能距离.ToString("F2"));
            ImGui.SameLine();
            ImGui.ProgressBar(GNBSettings.Instance.额外技能距离 / 3.0f, new Vector2(200, 0), "");
            ImGui.SliderFloat("", ref GNBSettings.Instance.额外技能距离, 0, 3);

            // 保留子弹数
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

        public void Handle()
        {
            // 这里可以根据需要添加保存或应用设置的逻辑
            GNBSettings.Instance.Save();
        }
    }
}