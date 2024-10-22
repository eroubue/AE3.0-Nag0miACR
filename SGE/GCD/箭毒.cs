using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
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
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.箭毒).IsReady())
        {
            return -66;
        }

      
        if (Core.Me.Level < 66) return -3;
        //红豆关了不打
        if (!QT.QTGET(QTKey.红豆))
        {
            return -2;
        }
        //开了蛇刺保留且蛇刺只有一个了不打
        if (QT.QTGET(QTKey.保留红豆) && Core.Resolve<JobApi_Sage>().Addersting < SGESettings.红豆保留数量) return -6;
        //蛇毒没了不打
        if (Core.Resolve<JobApi_Sage>().Addersting <= 0) return -1;
        //如果蛇刺还有
        if (Core.Resolve<JobApi_Sage>().Addersting>= 1&&!QT.QTGET(QTKey.停手))
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 8);
            if (aoeCount >= 2) return 2;
        }
        if (Core.Resolve<MemApiMove>().IsMoving())
            //还有层数就打
            if (Core.Resolve<JobApi_Sage>().Addersting >= 1&&!QT.QTGET(QTKey.停手))
                return 1;
        //在移动就打
        if (Core.Resolve<MemApiMove>().IsMoving()&&!QT.QTGET(QTKey.停手))
            if (Core.Resolve<JobApi_Sage>().Addersting>= 1)
            {
                return 3;
            }


        //一般不打
        return -1;
    }
    public Spell GetSpell()
    {//还是嵌套替换 箭毒
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