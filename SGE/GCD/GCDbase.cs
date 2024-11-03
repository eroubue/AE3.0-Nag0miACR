using AEAssist.CombatRoutine;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.MemoryApi;
using AEAssist.Helper;
using Nagomi.SGE.Settings;
using AEAssist.JobApi;
using Nagomi;
using Nagomi.SGE.utils;
namespace Nagomi.SGE.GCD;

public class GCDbase : ISlotResolver
{
   

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        //如果没有蓝 并且 醒梦cd 没有醒梦buff 禁止输出
        if (Core.Me.CurrentMp < 1500) return -2;

        //如果获取到的是失衡，允许执行
        if (GetSpell() == Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia.GetSpell().Id).GetSpell()&&!QT.QTGET(QTKey.停手)) return 0;
            //如果在移动，且不具有即刻
        if (Core.Resolve<MemApiMove>().IsMoving()&&!Core.Me.HasMyAuraWithTimeleft(SGEBuffs.即刻咏唱))
        {   //假设开了失衡走位QT，就允许通过
            if (SGESettings.Instance.失衡走位==1 && Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia).IsUnlockWithCDCheck()&&!QT.QTGET(QTKey.停手))
            {
                return 0;
            }
                //否则在移动且无即刻就不让过
                return -1;
        }
        //如果在移动，且获取到的技能不是失衡，那就不许打
        if (Core.Resolve<MemApiMove>().IsMoving()&& GetSpell() != Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia.GetSpell().Id).GetSpell())
            return -1;

        return 0;
    }
    public Spell GetSpell()
    {   //如果开了AOE选项


        if (QT.QTGET(QTKey.AOE))
        {   //判断一下周围五米范围内是不是有2个以上的，有的话把失衡/失衡dot添加进slot
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
            if (aoeCount >= 2&& !(Core.Me.Level < 46))
                //return SGESpells.Holy.GetSpell();//这里有个Holy是干啥的，不知道，抄了
            { 
                return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia.GetSpell().Id).GetSpell();
                //这里是嵌套的获取失衡在各个等级的ID的替换
            }
        }
        ////如果在移动，且开了失衡走位，且GCD转到0了
        if (Core.Resolve<MemApiMove>().IsMoving()&& SGESettings.Instance.失衡走位 == 1 && GCDHelper.GetGCDDuration()== 0&& Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia).IsReady())
        {   //那就返回失衡
            return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dyskrasia.GetSpell().Id).GetSpell();
        }
        //除此之外，只返回注药
        return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.Dosis.GetSpell().Id).GetSpell();
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