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

public class 绝枪70级绝神兵起手 : IOpener
{
    public int StartCheck()
    {
        //return -1;
      /*  if (PartyHelper.NumMembers <= 4 && !Core.Me.GetCurrTarget().IsDummy()))
            return -100;*/
        if (!GNBSpells.无情.IsReady())
            return -6;
        if (Core.Me.Level < 70) return -5;

       if (Core.Resolve<MemApiZoneInfo>().GetCurrTerrId() != 777u) return -8;

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
        
       
    };

    public Action CompeltedAction { get; set; }


   // public int StepCount => 9;


    private static void Step0(Slot slot)
    {
        LogHelper.Print("零和GNB", "开始神兵特化炒股5g起手");
        slot.Add(new Spell(GNBSpells.利刃斩, SpellTargetType.Target));//单体1
    }
    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.残暴弹, SpellTargetType.Target));//单体2
        LogHelper.Print("零和GNB", "2接爆发药");
        if (GNBRotationEntry.QT.GetQt("爆发药")) slot.Add(Spell.CreatePotion()); //爆发药
        slot.Add(new Spell(GNBSpells.石之心, SpellTargetType.Pm2));//支援
        
    }
    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.迅连斩, SpellTargetType.Target));//单体3
    }
    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.利刃斩, SpellTargetType.Target));//单体1
    }
    private static void Step4(Slot slot)
    {
        slot.Add(new Spell(GNBSpells.残暴弹, SpellTargetType.Target));//单体2
        slot.Add(new Spell(GNBSpells.无情, SpellTargetType.Self));
    }
    private static void Step5(Slot slot) //5GCD
    {
        slot.Add(new Spell(GNBSpells.音速破, SpellTargetType.Target)); 

    }
    public uint Level { get; } = 70;

   
    public void InitCountDown(CountDownHandler countDownHandler)//倒数行为
    {

       if (Core.Me.HasAura(GNBBuffs.王室亲卫))
            countDownHandler.AddAction(4500, GNBSpells.关盾姿, SpellTargetType.Self);



       if (QT.QTGET(QTKey.突进起手) &&!QT.QTGET(QTKey.停手) &&(Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
                                                         SettingMgr.GetSetting<GeneralSettings>().AttackRange))//在攻击范围外

       {
           countDownHandler.AddAction(600, GNBSpells.弹道, SpellTargetType.Target);

           // countDownHandler.AddAction(1000, SkillData.LightningShot, SpellTargetType.Target);
       }
        
        
    }
}