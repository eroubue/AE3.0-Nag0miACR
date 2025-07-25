using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using Nagomi.PCT;
using Nagomi.SGE.Settings;
using System.Numerics;
using System.Reflection;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.IO;

namespace Nagomi.SGE.Triggers;

public class 贤者时间轴配置设置: ITriggerAction
{
    public string DisplayName { get; } = "贤者/修改配置";
    public string Remark { get; set; }
    
    public string Key = "";
    public ITriggerSettingUI? nowSetting = null;
    

    private readonly Dictionary<string, ITriggerSettingUI> TriggerSettingUI = new();
    public 贤者时间轴配置设置()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Namespace == "Nagomi.SGE.Settings" && typeof(ITriggerSettingUI).IsAssignableFrom(t) && !t.IsInterface);
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type) as ITriggerSettingUI;
            TriggerSettingUI.Add(type.Name, instance);
        }
    }
    
    public bool Draw()
    {
       
        if (TriggerSettingUI.Count > 0)
        {
            if (nowSetting == null)
            {
                nowSetting = TriggerSettingUI.First().Value;
                Key = nowSetting.Name();
            }   
            ImGui.BeginGroup();
            
            if (ImGui.BeginCombo("###贤者设置类型", $"{nowSetting.Name()}"))
            {
                foreach (var settingUi in TriggerSettingUI)
                {
                    if (ImGui.Selectable($"{settingUi.Value.Name()}"))
                    {
                        Key = settingUi.Key;
                        nowSetting = settingUi.Value;
                    }
                }
                ImGui.EndCombo();
            }
            nowSetting.Draw();
            ImGui.EndGroup();
        }
        return true;
    }

    public bool Handle()
    {
        nowSetting?.Handle();
        return true;
    }
}
