using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.GNB.utils;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;


namespace Nagomi.SGE.GCD;

public class 箭毒 : ISlotResolver
{
   
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级没到不打
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
      
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.箭毒).IsUnlockWithCDCheck())
        {
            return -66;
        }
        if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 25+SGESettings.Instance.额外技能距离) return -1;
        
        if (QT.QTGET(QTKey.保留红豆) && Core.Resolve<JobApi_Sage>().Addersting <= SGESettings.Instance.红豆保留数量+1) return -6;
        
        if (Core.Resolve<JobApi_Sage>().Addersting <= 0) return -1;
        if (QT.QTGET(QTKey.爆发)) return 100;
        if (!QT.QTGET(QTKey.红豆))
        {
            return -2;
        }
        //如果蛇刺还有
        if (Core.Resolve<JobApi_Sage>().Addersting>= 1&&QT.QTGET(QTKey.AOE))
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 8);
            if (aoeCount >= 2) return 2;
        }
        //在移动就打
        if (Core.Resolve<MemApiMove>().IsMoving()&&Core.Resolve<JobApi_Sage>().Addersting>= 1)
        { return 3; }
        //一般不打
        return -1;
    }
    public Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Toxikon.GetSpell().Id).GetSpell();
    }
    
    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}