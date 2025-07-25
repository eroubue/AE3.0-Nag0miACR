using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;
using Nagomi.PCT;
    
namespace Nagomi.PCT.GCD

{
    public class PCTGCD_CYM : ISlotResolver
    {
        public int Check()
        {
            if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
            
            if (Core.Resolve<MemApiMove>().IsMoving()) 
            {
                return -1;
            }
            if (!Core.Me.HasAura(PCTBuffs.减色混合) ) 
            {
                return -2;
            }
            if (!QT.QTGET(QTKey.CYM)) 
            {
                return -3;
            }
            if (!Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.冰结之蓝青).IsUnlockWithCDCheck())
            {
                return -3;
            } 
            return 1;
        }
        private Spell GetSpell()
        {            
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 5);
            if (aoeCount >= 4 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.冰冻之蓝青).IsUnlockWithCDCheck() &&  QT.QTGET(QTKey.AOE))
             //   && !PCTSettings.Instance.智能aoe目标)
                return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.冰冻之蓝青).GetSpell();
            // if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.冰冻之蓝青).IsUnlockWithCDCheck() &&
            //    QT.QTGET(QTKey.AOE) && PCTSettings.Instance.智能aoe目标 && canTargetObjects != null &&
           //     canTargetObjects.IsValid())
           //     return new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.冰冻之蓝青), canTargetObjects);
            
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.冰结之蓝青).GetSpell();
        }
        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null)
                return;
            slot.Add(spell);
        }
    }
}