using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;

namespace Nagomi.GNB.能力;

public class GNB能力_无情 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    public int Check()
    {
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!GNBSpells.无情.GetSpell().IsReadyWithCanCast())
        {
            return -66;
        }
        
        if (QT.QTGET(QTKey.倾泻爆发))
        {
            return 100;
        }
        if (!QT.QTGET(QTKey.爆发))
        {
            return -10;
        }
        if (!QT.QTGET(QTKey.无情))
        {
            return -10;
        }
        if (QT.QTGET(QTKey.无情不延后))
        {
            return 1;
        }

        if (Core.Me.Level == 100)
        {
            if (Core.Resolve<JobApi_GunBreaker>().Ammo == 3 && !SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 8) ) return 1;//无血壤填充&&
            //Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹
            if (Core.Resolve<JobApi_GunBreaker>().Ammo ==2 &&QT.QTGET(QTKey.二弹)&&SpellExtension.CoolDownInGCDs(GNBSpells.血壤, 4)&&Core.Resolve<MemApiSpellCastSuccess>().LastGcd==GNBSpells.爆发击) return 1;
            if (Core.Resolve<JobApi_GunBreaker>().Ammo ==0 &&QT.QTGET(QTKey.零弹)&&Core.Resolve<MemApiSpell>().GetLastComboSpellId() == GNBSpells.残暴弹) return 1;

        }

        if (Core.Me.Level != 100)
        {
            return 1;
        }

        
        
        return -1;
            
    }
    public void Build(Slot slot)
    {
        slot.Add(GNBSpells.无情.GetSpell());
    }
}
