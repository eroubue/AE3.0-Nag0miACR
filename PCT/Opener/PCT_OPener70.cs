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

public class PCT_Opener70 : IOpener
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
    };
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(2000, PCTSpells.火炎之红, SpellTargetType.Target);
    }
    private static void Step0(Slot slot)
    
    {
        LogHelper.Print("70起手开始");
        slot.Add(new Spell(PCTSpells.疾风之绿, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.重锤构想, SpellTargetType.Target));

    }
    
    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.流水之蓝, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.星空构想, SpellTargetType.Self));
    }
    
    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.重锤敲章, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.绒球构想, SpellTargetType.Target));
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
        slot.Add(new Spell(PCTSpells.重锤敲章, SpellTargetType.Target));
    }
    
    private static void Step7(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.重锤敲章, SpellTargetType.Target));
    }
    
    private static void Step8(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.即刻咏唱, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.翅膀彩绘, SpellTargetType.Target));
    }
    
    private static void Step9(Slot slot)
    {
        slot.Add(new Spell(PCTSpells.翅膀构想, SpellTargetType.Target));
        slot.Add(new Spell(PCTSpells.莫古力激流, SpellTargetType.Target));
    }
    
        
    public Action CompeltedAction { get; set; }
 
}