using AEAssist.CombatRoutine.View.JobView;
using Millusion.ACR.Astrologian.Hotkey;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.UI.Tab;
using Millusion.Helper;

namespace Millusion.ACR.Astrologian.UI;

public static class AST_View
{
    public static JobViewWindow UI { get; private set; }

    public static void BuildUI()
    {
        UI = new JobViewWindow(AST_Settings.Instance.JobViewSave, AST_Settings.Instance.Save, "占星术士");

        // UI.SetUpdateAction(OnUIUpdate);
        UI.AddTab("Debug", DebugUI.Draw);
        UI.AddTab("Dev", MsData.Instance.Draw);
        UI.AddTab("技能列表", SpellCheckUI.Draw);
        UI.AddTab("设置", SettingUI.Draw);

        AST_QT.AddAllQT(UI);

        AST_Hotkey.AddAllHotkey(UI);
    }
}