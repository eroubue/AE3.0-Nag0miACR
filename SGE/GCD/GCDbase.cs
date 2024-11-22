using AEAssist.CombatRoutine;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.MemoryApi;
using AEAssist.Helper;
using Nagomi.SGE.Settings;
using AEAssist.JobApi;
using Nagomi;
using Nagomi.GNB.utils;
using Nagomi.SGE.utils;
namespace Nagomi.SGE.GCD;

public class GCDbase : ISlotResolver
{
   

    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
    public Spell GetSpell()
    {   //如果开了AOE选项
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);


        if (QT.QTGET(QTKey.AOE))
        { 
            if (aoeCount >= 2)
            { 
                return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia.GetSpell().Id).GetSpell();
            }
        }
        if (Core.Resolve<MemApiMove>().IsMoving()&& SGESettings.Instance.失衡走位 == 1 && GCDHelper.GetGCDDuration()== 0&& Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia).IsUnlockWithCDCheck()&& aoeCount >= 1)
        {   //那就返回失衡
            return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia.GetSpell().Id).GetSpell();
        }
        //除此之外，只返回注药
        return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dosis.GetSpell().Id).GetSpell();
    }

    public int Check()
    {
        var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 25+SGESettings.Instance.额外技能距离) return -1;
        //如果没有蓝 并且 醒梦cd 没有醒梦buff 禁止输出
        if (Core.Me.CurrentMp < 1500) return -2;

        if (Core.Resolve<MemApiMove>().IsMoving()&&!Core.Me.HasMyAuraWithTimeleft(SGEBuffs.即刻咏唱))
        {   //假设开了失衡走位QT，就允许通过
            if (SGESettings.Instance.失衡走位==1  && GCDHelper.GetGCDDuration()== 0&& Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia).IsUnlockWithCDCheck()&& aoeCount >= 1)
            {
                return 0;
            }
            //否则在移动且无即刻就不让过
                return -1;
        }
        return 0;
    }
 
    public void Build(Slot slot)
    {   //获取一下spell，如果是null，就直接过了
        var spell = GetSpell();
        if (spell == null)
            return;
        if (spell == Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.EukrasianDyskrasia).GetSpell())
        {
            if (!Core.Resolve<JobApi_Sage>().Eukrasia)
                slot.Add(SGESpells.Eukrasia.GetSpell());
        }

        slot.Add(spell);
    }
}