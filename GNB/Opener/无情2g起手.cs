using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.Opener;

public class 无情2g起手 : IOpener
{
    public int StartCheck()
    {
        if (Core.Me.Level != 100) return -5;

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
        Step2,
        Step3,
       
    };

    public Action CompeltedAction { get; set; }


    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.利刃斩, SpellTargetType.Target));
        
    }

    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.残暴弹, SpellTargetType.Target));
        slot.Add(new Spell(GNBSpells.血壤, SpellTargetType.Target));
        slot.Add(new Spell(GNBSpells.无情, SpellTargetType.Self));
    }
    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.烈牙, SpellTargetType.Target));
        slot.Add(new Spell(GNBSpells.爆破领域, SpellTargetType.Target));
        slot.Add(new Spell(GNBSpells.撕喉, SpellTargetType.Target));
    }
    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.倍攻, SpellTargetType.Self));
        slot.Add(new Spell(GNBSpells.弓形冲波, SpellTargetType.Self));
        slot.Add(new Spell(GNBSpells.音速破, SpellTargetType.Target));
    }
    
    

    

    public uint Level { get; } = 100;

   
    public void InitCountDown(CountDownHandler countDownHandler)//倒数行为
    {

        if (Core.Me.HasAura(GNBBuffs.王室亲卫)&&GNBSettings.Instance.ST)
            countDownHandler.AddAction(4500, GNBSpells.关盾姿);
        if (Core.Me.HasAura(GNBBuffs.王室亲卫)&&!GNBSettings.Instance.ST)
            countDownHandler.AddAction(4000, GNBSpells.盾姿);



        if (QT.QTGET(QTKey.突进起手) &&!QT.QTGET(QTKey.停手))

        {
            countDownHandler.AddAction(660, GNBSpells.弹道, SpellTargetType.Target);

        }
        
        
    }
}