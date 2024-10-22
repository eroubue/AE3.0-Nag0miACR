using AEAssist;
using AEAssist.MemoryApi;
using ImGuiNET;
using Nagomi.PvP.PVPApi;


namespace Nagomi.PCT.Settings
{
    public class PCTSettingUI
    {
        public static PCTSettingUI Instance = new();
        public PCTSettings PCTSettings => PCTSettings.Instance;
    
        public void Draw()
        {
            ImGui.InputInt("目标剩余多少百分比血量时禁用读条画画", ref PCTSettings.画画百分比);
            ImGui.Checkbox("团辅前不释放高威力技能", ref PCTSettings.OpenLazy);
            ImGui.InputInt("团辅前多少秒不释放高威力技能", ref PCTSettings.团辅提前);
            if (ImGui.Button("获取触发器链接"))
            {
                Core.Resolve<MemApiChatMessage>().Toast2("感谢使用零师傅工具箱\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
                Core.Resolve<MemApiSendMessage>().SendMessage("/e https://11142.kstore.space/TriggernometryExport.xml");
            }
            ;
            ImGui.SameLine();
            ImGui.Text("导入到act高级触发器插件的远程触发器中，使用前请更新!");
            
           // ImGui.SliderFloat("音量", ref PCTSettings.Instance.Volume,0,1);
           // ImGui.Checkbox("音效", ref PCTSettings.音效);
           // ImGui.Checkbox("智能aoe目标", ref PCTSettings.智能aoe目标);
   
            if (ImGui.Button("Save"))
            {
                PCTSettings.Instance.Save();
            }
        }
    }
}