using Nagomi.GNB.utils;
using Nagomi.SGE.utils;

namespace Nagomi.SGE.GCD;

using AEAssist.CombatRoutine;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.MemoryApi;
using AEAssist.Helper;
using Nagomi.SGE.Settings;
using AEAssist.JobApi;
using Nagomi;

//这部分是均衡注药的调用
public class GCD_Dot : ISlotResolver
{
   

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {   //等级低于30直接别用了，均衡没学会呢

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 25+SGESettings.Instance.额外技能距离) return -1;
        
        if (!QT.QTGET(QTKey.DOT))
        {
            return -2;
        }
        
        //加入dot黑名单了也别用
        if (DotBlacklistHelper.IsBlackList(Core.Me.GetCurrTarget()))
            return -10;
        //刚用过就别用了
        if (SGESpells.EukrasianDyskrasia.RecentlyUsed(5000))
        {
            return -23;
        }

        //如果在移动，判断一下周围目标数目 决定是不是要小上一下

        var 周围目标 = TargetHelper.GetNearbyEnemyCount(Core.Me, 0, 5);
        if (Core.Resolve<MemApiMove>().IsMoving() && 周围目标 >= 3 && GCDHelper.GetGCDDuration() == 0 && !SGESpells.EukrasianDyskrasia.RecentlyUsed(5000))
        {
            if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(SGEBuffs.均衡注药2614, 4000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(SGEBuffs.均衡注药II2615, 4000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(2616, 4000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(3897, 4000))
            {
                return -6;
            }

        }


        //如果身上有爆发药，可以提前续一下dot
        if (Core.Me.HasAura(49))
        {
            if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(SGEBuffs.均衡注药2614, 6000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(SGEBuffs.均衡注药II2615, 6000) ||
                Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(2616, 6000) ||
                    Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(3897, 6000))
            {
                return -1;
            }
        }
        //如果没有爆发药，那就判断一下是不是大于4s，大于不上dot
        if (Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(SGEBuffs.均衡注药2614, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(SGEBuffs.均衡注药II2615, 4000) ||
            Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(2616, 4000) ||
                    Core.Me.GetCurrTarget().HasMyAuraWithTimeleft(3897, 4000))
            return -1;

        //如果快死了，不上dot，阈值由玩家自行设定
        if (Core.Me.GetCurrTarget().CurrentHpPercent() < SGESettings.Instance.不上dot阈值)
            return -1;

        return 0;
    }
    public Spell GetSpell()
    {//注药的嵌套获取，都能用



        if (QT.QTGET(QTKey.AOE))
        {   //判断一下周围五米范围内是不是有2个以上的，有的话把失衡dot添加进slot
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me, 5, 5);
            if (aoeCount >= 2 && !(Core.Me.Level < 82))
            {
                return Core.Resolve<MemApiSpell>().CheckActionChange(37032).GetSpell();
                //这里是嵌套的获取失衡Dot在各个等级的ID的替换
            }
        }


        return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.注药).GetSpell();
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        //如果身上没有均衡，就塞个均衡进去
        if (!Core.Resolve<JobApi_Sage>().Eukrasia)
        {
            slot.Add(SGESpells.Eukrasia.GetSpell());
        }
        slot.Add(spell);
        //slot.AppendSequence();
    }
}