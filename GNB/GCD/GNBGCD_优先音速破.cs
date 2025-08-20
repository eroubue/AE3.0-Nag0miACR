using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.GCD;

public class GNBGCD_优先音速破 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public int Check()
    {
        if (!Core.Me.HasAura(GNBBuffs.无情)) return -23;
        if (!SpellExtension.IsReadyWithCanCast(GNBSpells.音速破.GetSpell()))
            return -50;
        // 如果当前角色等级不等于70，则返回-10
        if (!QT.QTGET(QTKey.优先音速破))
        {
            return -10;
        }
        // 当额外技能距离不为0且与目标的距离大于3加上额外技能距离时，返回-12
        if (GNBSettings.Instance.额外技能距离 != 0 && Core.Me.Distance(Core.Me.GetCurrTarget()) > 3 + GNBSettings.Instance.额外技能距离)
            return -12;
        
        // 当额外技能距离为0且与目标的距离（忽略碰撞箱）大于设定的攻击范围时，返回-5
        if (GNBSettings.Instance.额外技能距离 == 0 && Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;


        
        // 如果当前目标有任何无敌buff，则返回-152
        if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
        
        // 如果启用了“停手”设置，则返回-100
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        
        // 如果启用了“倾泻爆发”设置，则返回100
        if (QT.QTGET(QTKey.倾泻爆发))
        {
            return 100;
        }
        
        // 如果未启用“爆发”设置，则返回-10
        if (!QT.QTGET(QTKey.爆发))
        {
            return -10;
        }
        
        // 如果未启用“dot”设置，则返回-10
        if (!QT.QTGET(QTKey.dot))
        {
            return -10;
        }
        if (Helper.技能0dot6s内是否用过(GNBSpells.无情)&&QT.QTGET(QTKey.落地无情)&&!Core.Me.HasAura(GNBBuffs.无情)) return -21;
        // 如果未启用“音速破”设置，则返回-10
        
       
        // 如果“音速破”技能不可用或不能施放，则返回-50
        
        
        // 如果以上条件都不满足，则返回0
        return 0;
    }
    
    private Spell GetSpell()
    {
        return GNBSpells.音速破.GetSpell();

    }
     public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}