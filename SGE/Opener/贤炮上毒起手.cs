using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.CombatRoutine;
using AEAssist.MemoryApi;
using Nagomi.PCT;
using Nagomi.SGE;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;

namespace Nagomi.SGE.Opener;

public class 贤炮上毒起手 : IOpener
{
    public int StartCheck()
    {
        if (PartyHelper.CastableParty.Count <= 4 && !Core.Me.GetCurrTarget().IsBoss())
            return -100;
        return 0;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step0,

    };

    public Action CompeltedAction { get; set; }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(15000, 24290u, SpellTargetType.Self);
        countDownHandler.AddAction(9000, Core.Resolve<MemApiSpell>().CheckActionChange(24292), SpellTargetType.Self);
        countDownHandler.AddAction(3000, 24290u, SpellTargetType.Self);
        countDownHandler.AddAction(SGESettings.Instance.预读时间, 24318u, SpellTargetType.Target);
    }
    private static void Step0(Slot slot)
    
    {
        LogHelper.Print("贤炮上毒起手开始");
        slot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(24293), SpellTargetType.Target));

    }
    
}