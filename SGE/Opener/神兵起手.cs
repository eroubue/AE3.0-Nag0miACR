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

public class 神兵起手 : IOpener
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
        Step1,
        

    };

    public Action CompeltedAction { get; set; }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(15000, 24302u, SpellTargetType.Self);//自生
        countDownHandler.AddAction(14500, 24290u, SpellTargetType.Self);//均衡
        countDownHandler.AddAction(14000, 24300u, SpellTargetType.Self);//活化
        countDownHandler.AddAction(13500, Core.Resolve<MemApiSpell>().CheckActionChange(24292), SpellTargetType.Self);//群盾
        countDownHandler.AddAction(11500, 24290u, SpellTargetType.Self);//均衡
        countDownHandler.AddAction(10000, Core.Resolve<MemApiSpell>().CheckActionChange(24291), SpellTargetType.Pm2);//单盾
        countDownHandler.AddAction(3000, 24298u, SpellTargetType.Self);//罩子
        countDownHandler.AddAction(SGESettings.Instance.预读时间, 24283u, SpellTargetType.Self);//注药
    }
    private static void Step0(Slot slot)
    
    {
        
        slot.Add(new Spell(24283u, SpellTargetType.Target));
       

    }
    
    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(24290u, SpellTargetType.Self));
        slot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(24293), SpellTargetType.Target));
    }
    
    
}