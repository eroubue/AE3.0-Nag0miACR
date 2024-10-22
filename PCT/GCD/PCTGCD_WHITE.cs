using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;
    
namespace Nagomi.PCT.GCD

{
    public class PCTGCD_WHITE : ISlotResolver
    {
        public int Check()
        {
            
            if (Core.Resolve<MemApiMove>().IsMoving() && Core.Resolve<JobApi_Pictomancer>().豆子 >= 1 &&　PCTSpells.神圣之白.IsReady())
            {
                return 1;
            }
            var aoeCount = TargetHelper.GetNearbyEnemyCount(Core.Me.GetCurrTarget(), 25, 5);
            if (aoeCount >= 2 && Core.Resolve<JobApi_Pictomancer>().豆子 >= 2 &&　PCTSpells.神圣之白.IsReady())
            {
                return 5;
            }
            
            return -1;
            
        }
        private Spell GetSpell()
        {
           // var canTargetObjects = TargetHelper.GetMostCanTargetObjects(PCTSpells.神圣之白, 4);
            //if(PCTSpells.神圣之白.IsReady()&& PCTSettings.Instance.智能aoe目标 && canTargetObjects != null &&
              // canTargetObjects.IsValid())
                //return new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.神圣之白), canTargetObjects);
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.神圣之白).GetSpell();
            
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