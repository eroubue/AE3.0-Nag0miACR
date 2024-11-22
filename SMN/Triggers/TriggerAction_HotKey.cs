using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using ImGuiNET;

namespace Nagomi.SMN.Triggers;

//这个类也可以完全复制 改一下上面的namespace和对HotKey的引用就行
public class TriggerAction_HotKey : ITriggerAction
{
    public string DisplayName { get; } = "召唤/HotKey";
    public string Remark { get; set; }
    
    public string Key = "";
    public bool Value;
    
    // 辅助数据 因为是private 所以不存档
    private int _selectIndex;
    private string[] _HotKeyArray;

    public TriggerAction_HotKey()
    {
        _HotKeyArray = SMNRotationEntry.QT.GetHotkeyArray();
    }

    public bool Draw()
    {
        _selectIndex = Array.IndexOf(_HotKeyArray, Key);
        if (_selectIndex == -1)
        {
            _selectIndex = 0;
        }
        ImGuiHelper.LeftCombo("选择Key",ref _selectIndex,_HotKeyArray);
        Key = _HotKeyArray[_selectIndex];
        return true;
    }

    public bool Handle()
    {
        SMNRotationEntry.QT.SetHotkey(Key);
        return true;
    }
}