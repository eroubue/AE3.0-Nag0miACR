using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.PCT;
using Nagomi.PCT.GCD;
using Nagomi.PCT.Settings;
using Nagomi.PCT.能力;

namespace Nagomi.PCT.Opener;

public class PCT_Opener100 : IOpener
{
    public int StartCheck()
    {

        if (PCTSpells.风景构想.GetSpell().Charges<1)
        {
            return -2;
        }
        if (AI.Instance.BattleData.CurrBattleTimeInMs > 5)
        {
            return -5;
        }
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
        Step4,
        Step5,
        Step6,
        Step7,
        Step8,
        Step9,
        Step10,
        Step11,
    };
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(4500, PCTSpells.彩虹点滴, SpellTargetType.Target);
    }
    private static void Step0(Slot slot)
    {
        LogHelper.Print("100起手开始");
        slot.Add(new Spell(PCTSpells.神圣之白, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.重锤构想, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.绒球构想, SpellTargetType.Target));
    }
    
    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.翅膀彩绘, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.星空构想, SpellTargetType.Self));
    }
    
    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.重锤敲章, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.减色混合, SpellTargetType.Target));
    }
    
    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.冰结之蓝青, SpellTargetType.Target));
        
    }
    
    private static void Step4(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.飞石之纯黄, SpellTargetType.Target));
    }
    
    private static void Step5(Slot slot)
    { 
        slot.Add(new Spell(PCTSpells.闪雷之品红, SpellTargetType.Target));
    }
    
    private static void Step6(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.彗星之黑, SpellTargetType.Target));
    }
    
    private static void Step7(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.翅膀构想, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.莫古力激流, SpellTargetType.Target));
    }
    
    private static void Step8(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.天星棱光, SpellTargetType.Target));
    }
    
    private static void Step9(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.重锤掠刷, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.重锤抛光, SpellTargetType.Target));
    }
    
    private static void Step10(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.彩虹点滴, SpellTargetType.Target));
    }
    private static void Step11(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.即刻咏唱, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.兽爪彩绘, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.兽爪构想, SpellTargetType.Target));
    }
        
    public Action CompeltedAction { get; set; }
 
}