using AEAssist.CombatRoutine.Trigger;
using ImGuiNET;
using Nagomi.PCT;
using Nagomi.GNB.Settings;
using System.Numerics;
using System.Reflection;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.IO;

namespace Nagomi.GNB.Triggers;

public class 绝枪时间轴配置设置: ITriggerAction
{
    public string DisplayName { get; } = "绝枪/修改配置";
    public string Remark { get; set; }
    
    public string Key = "";
    public ITriggerSettingUI? nowSetting = null;
    
    public int 保留子弹数 = 0;

    private readonly Dictionary<string, ITriggerSettingUI> TriggerSettingUI = new();
    public 绝枪时间轴配置设置()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Namespace == "Nagomi.GNB.Settings" && typeof(ITriggerSettingUI).IsAssignableFrom(t) && !t.IsInterface);
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
            
            if (ImGui.BeginCombo("###绝枪设置类型", $"{nowSetting.Name()}"))
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
            // 新增：保留子弹数选择
            int[] options = { 0, 1, 2 };
            string[] optionLabels = { "0", "1", "2" };
            int selected = 保留子弹数;
            if (ImGui.Combo("保留子弹数", ref selected, optionLabels, optionLabels.Length))
            {
                保留子弹数 = selected;
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
