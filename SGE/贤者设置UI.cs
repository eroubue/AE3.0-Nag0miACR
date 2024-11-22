using AEAssist.GUI;
using ImGuiNET;
using Nagomi.SGE;
namespace Nagomi.SGE.Settings;

public class SGESettingUI
{
    
        public static SGESettingUI Instance = new();
        public SGESettings SGESettings => SGESettings.Instance;
    
        public void Draw()
        {
            ImGui.SliderFloat("不上dot阈值", ref SGESettings.Instance.不上dot阈值, 0.0f, 1.0f);
            ImGui.Text("按住Ctrl左键单击滑块可以直接输入数字，默认3%不上dot");
            ImGui.SliderFloat("额外技能距离", ref SGESettings.Instance.额外技能距离, 0.0f, 3.0f);
            ImGui.Text("按住Ctrl左键单击滑块可以直接输入数字，适配长臂猿用的！没开别动！");
            ImGui.SliderInt("红豆保留数量", ref SGESettings.Instance.红豆保留数量, 1, 3);
            ImGui.SliderInt("发炎保留数量", ref SGESettings.Instance.发炎保留数量, 1, 2);
            ImGui.Text("保留QT是开了QT就绝对不打，动了也不打");
            ImGui.Text("H1 or H2");
            ImGui.SameLine();
            if (SGESettings.Instance.H1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 0.0f, 0.0f, 1.0f), "H1"); 
            }
            else if (!SGESettings.Instance.H1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "H2"); 
            }
            ImGui.SameLine();
            if (ImGui.Button(" H1 "))
            {
                SGESettings.Instance.H1 = true;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button(" H2 "))
            {
                SGESettings.Instance.H1 = false;
                SGESettings.Instance.Save();
            }
            
            if (ImGui.Button("失衡走位关"))
            {
                SGESettings.Instance.失衡走位 = 0;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("失衡走位开"))
            {
                SGESettings.Instance.失衡走位 = 1;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            ImGui.Text("失衡走位：");
            ImGui.SameLine();
            if (SGESettings.Instance.失衡走位 == 0)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f), "关"); // 绿色
            }
            else if (SGESettings.Instance.失衡走位 == 1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 0.0f, 1.0f), "开"); // 蓝色
            }
            if (ImGui.Button("即刻贤炮关"))
            {
                SGESettings.Instance.即刻贤炮 = 0;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("即刻贤炮开"))
            {
                SGESettings.Instance.即刻贤炮 = 1;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            ImGui.Text("即刻贤炮热键：");
            ImGui.SameLine();
            if (SGESettings.Instance.即刻贤炮 == 0)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f), "关"); // 绿色
            }
            else if (SGESettings.Instance.即刻贤炮 == 1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 0.0f, 1.0f), "开"); // 蓝色
            }
            ImGui.Text("当前起手：");
            ImGui.SameLine();
            if (SGESettings.Instance.opener == 0)
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "默认起手"); // 绿色
            }
            else if (SGESettings.Instance.opener == 1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 0.3f, 0.8f, 1.0f), "贤炮起手"); // 蓝色
            }

            if (ImGui.Button("默认起手"))
            {
                SGESettings.Instance.opener = 0;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("贤炮起手"))
            {
                SGESettings.Instance.opener = 1;
                SGESettings.Instance.Save();
            }
   

            ImGuiHelper.LeftInputInt("起手预读时间：", ref SGESettings.Instance.预读时间);
            {
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            ImGui.Text("ms");

            // ImGui.SliderFloat("音量", ref SGESettings.Instance.Volume,0,1);
            // ImGui.Checkbox("音效", ref SGESettings.音效);
            // ImGui.Checkbox("智能aoe目标", ref SGESettings.智能aoe目标);
   
            if (ImGui.Button("Save"))
            {
                SGESettings.Instance.Save();
            }
        }
    
}

