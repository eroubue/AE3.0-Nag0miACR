using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger.TriggerAction;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.PCT;
using PCT.utils;

namespace Nagomi.PCT.GCD

{
    public class PCTGCD_RGB : ISlotResolver
    {
        public int Check()
        {
            if (Core.Resolve<MemApiMove>().IsMoving()) 
            {
                return -1;
            }
            if ( Core.Me.HasAura(PCTBuffs.减色混合) ) 
            {
                return -2;
            }
            if (!Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.火炎之红).IsReady())
            {
                return -3;
            } 
            if (!QT.QTGET(QTKey.RGB)) 
            {
                return -4;
            }
            return 1;
        }
        private Spell GetSpell()
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 5);
           // var canTargetObjects = TargetHelper.GetMostCanTargetObjects(PCTList.aoe调色,4);
            if (aoeCount >= 4 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.烈炎之红).IsReady() &&   QT.QTGET(QTKey.AOE))
           //   && !PCTSettings.Instance.智能aoe目标)
                return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.烈炎之红).GetSpell();
           // if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.烈炎之红).IsReady() &&
            //    QT.QTGET(QTKey.AOE) && PCTSettings.Instance.智能aoe目标 && canTargetObjects != null &&
            //    canTargetObjects.IsValid())
            //    return new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.烈炎之红), canTargetObjects);
            
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.火炎之红).GetSpell();
            
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