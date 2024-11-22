using AEAssist;
using AEAssist.MemoryApi;
using ImGuiNET;
using Nagomi.PvP.PVPApi;


namespace Nagomi.SMN.Settings
{
    public class SMNSettingUI
    {
        public static SMNSettingUI Instance = new();
        public SMNSettings SMNSettings => SMNSettings.Instance;
    
        public void Draw()
        {
      
            if (ImGui.Button("获取触发器链接"))
            {
                Core.Resolve<MemApiChatMessage>().Toast2("感谢使用零师傅工具箱\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
                Core.Resolve<MemApiSendMessage>().SendMessage("/e https://11142.kstore.space/TriggernometryExport.xml");
            }
            ;
            ImGui.SameLine();
            ImGui.Text("导入到act高级触发器插件的远程触发器中，使用前请更新!");
            
           // ImGui.SliderFloat("音量", ref SMNSettings.Instance.Volume,0,1);
           // ImGui.Checkbox("音效", ref SMNSettings.音效);
           // ImGui.Checkbox("智能aoe目标", ref SMNSettings.智能aoe目标);
   
            if (ImGui.Button("Save"))
            {
                SMNSettings.Instance.Save();
            }
        }
    }
}