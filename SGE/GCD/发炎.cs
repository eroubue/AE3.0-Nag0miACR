using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using System.Runtime;
using Nagomi.SGE;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
namespace Nagomi.SGE.GCD;

public class 发炎 : ISlotResolver

{
    
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级没到不打

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.发炎).IsReady())
        {
            return -66;
        }
        if (Core.Me.Level < 26) return -3;
        //发炎QT关了不打
        if (!QT.QTGET(QTKey.发炎)) return -2;
        //距离目标大于6了不打
        if (QT.QTGET(QTKey.保留发炎) && GetSpell().Charges < SGESettings.Instance.发炎保留数量) return -6;
        if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 6) return -1;
        //获取到的技能的冷却状态大于1.9了打 就是快转好了
        if (Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Phlegma).IsReady() && GetSpell().Charges > 1.9&&!QT.QTGET(QTKey.爆发))
        {
            return 2;
        }
        if (QT.QTGET(QTKey.爆发)&&Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Phlegma).IsReady() && GetSpell().Charges >= 1.0)
        {
            return 1;
        }


        if (Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Phlegma).IsReady() && GetSpell().Charges >= 1)
        {
            //6米内的目标周围5米内有2个以上的怪物就打
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 6, 5);
            if (aoeCount >= 2) return 4;
        }

        //在移动的时候
        if (Core.Resolve<MemApiMove>().IsMoving())
        { //还有层数就打
            if (Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Phlegma).IsReady() && GetSpell().Charges >= 1)
            { return 1; }
        }
        //我感觉这里应该是常关的
        return -1;
    }
    public Spell GetSpell()
    {//发炎的嵌套替换获取技能
        return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Phlegma).GetSpell();
    }


    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}