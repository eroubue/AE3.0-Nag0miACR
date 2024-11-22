using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.CombatRoutine;
using AEAssist.MemoryApi;
using Nagomi.SGE;
using Nagomi.SGE.Settings;

namespace Nagomi.SGE.Opener;

public class 贤者起手 : IOpener
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

    };

    public Action CompeltedAction { get; set; }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(8000, 24290u, SpellTargetType.Self);
        countDownHandler.AddAction(5000, Core.Resolve<MemApiSpell>().CheckActionChange(24291));
        countDownHandler.AddPotionAction(3000);//爆发药
        countDownHandler.AddAction(SGESettings.Instance.预读时间, 24312u, SpellTargetType.Target);
    }
}