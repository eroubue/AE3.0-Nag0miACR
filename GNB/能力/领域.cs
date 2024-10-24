using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.能力;

public class 领域 : ISlotResolver
{
    public int Check()
    {
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (Core.Me.Level < 18) return -3;
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.危险领域).IsReady())
        {
            return -66;
        }
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
        if (QT.QTGET(QTKey.倾泻爆发))
        {
            return 100;
        }
        if (!QT.QTGET(QTKey.爆发)&&!QT.QTGET(QTKey.领域))
        {
            return -2;
        }
        if (!QT.QTGET(QTKey.领域))
        {
            return -10;
        }
        if (SpellExtension.CoolDownInGCDs(GNBSpells.无情, 2)) return -1;
        return 1;
            
    }
    private Spell GetSpell()
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.危险领域).GetSpell();
    }
    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}