using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using Millusion.ACR.Astrologian.UI;

namespace Millusion.ACR.Astrologian.Triggers.Action;

public class AST_Action_QT : ITriggerAction
{
    private readonly List<TriggerQTSetting> _qtList = [];

    public string DisplayName => "占星/QT设置";

    public string Remark { get; set; } = "";

    public bool Draw()
    {
        ImGui.BeginChild("##AST_Trigger_QT_List", new Vector2(0f, 0f));
        ImGuiHelper.DrawSplitList("QT开关", _qtList, DrawHeader, AddCallBack, DrawCallback);
        ImGui.EndChild();
        return true;
    }

    public bool Handle()
    {
        foreach (var qtSetting in _qtList)
        {
            qtSetting.Handle();
        }

        return true;
    }

    private TriggerQTSetting DrawCallback(TriggerQTSetting arg)
    {
        arg.Draw();
        return arg;
    }

    private string DrawHeader(TriggerQTSetting arg)
    {
        var v = (arg.Value ? "开" : "关");
        return v + "-" + arg.Key;
    }

    private TriggerQTSetting AddCallBack()
    {
        return new TriggerQTSetting();
    }

    private class TriggerQTSetting
    {
        public string Key = "";

        public bool Value;

        private int _radioCheck;

        private int _selectIndex;

        private readonly string[] _qtArray = AST_View.UI.GetQtArray();

        public bool Draw()
        {
            _selectIndex = Array.IndexOf(_qtArray, Key);
            if (_selectIndex == -1)
            {
                _selectIndex = 0;
            }

            _radioCheck = (Value ? 1 : 0);

            ImGui.NewLine();
            ImGui.SetCursorPos(new Vector2(0f, 40f));
            ImGui.Combo("Qt开关", ref _selectIndex, _qtArray, _qtArray.Length);
            ImGui.RadioButton("开", ref _radioCheck, 1);
            ImGui.SameLine();
            ImGui.RadioButton("关", ref _radioCheck, 0);

            Key = _qtArray[_selectIndex];
            Value = _radioCheck == 1;
            return true;
        }

        public bool Handle()
        {
            return AST_View.UI.SetQt(Key, Value);
        }
    }
}