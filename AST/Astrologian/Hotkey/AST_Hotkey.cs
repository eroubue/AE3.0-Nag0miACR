using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.Hotkey;

public static class AST_Hotkey
{
    public static void AddAllHotkey(JobViewWindow ui)
    {
        ui.AddHotkey("光速", new HotKeyResolver_NormalSpell(SpellsDefine.Lightspeed, SpellTargetType.Self));
        ui.AddHotkey("中间学派", new HotKeyResolver_NormalSpell(SpellsDefine.NeutralSect, SpellTargetType.Self));
        ui.AddHotkey("大宇宙", new HotKeyResolver_NormalSpell(SpellsDefine.Macrocosmos, SpellTargetType.Self));
        ui.AddHotkey("防击退", new HotKeyResolver_NormalSpell(SpellsDefine.Surecast, SpellTargetType.Self));
        // UI.AddHotkey("营救", new HotKey(SpellsDefine.Rescue, SpellTargetType.SpecifyTarget));
        ui.AddHotkey("LB", new HotKeyResolver_LB());
    }
}