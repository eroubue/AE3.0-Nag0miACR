using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;
    
namespace Nagomi.PCT.能力

{
    public class PCT_能力_减色混合 : ISlotResolver
    {
        public int Check()
        {
            if (!PictomancerRotationEntry.QT.GetQt("减色混合"))
            {
                return -7;
            }
            if ( PCTSpells.彗星之黑.IsReady()&&Core.Resolve<JobApi_Pictomancer>().豆子 == 0 || !PCTSpells.减色混合.IsReady())
            {
                return -2;
            }
            
            if (Core.Me.HasAura(PCTBuffs.减色混合))
            {
                return -1;
            }
            return 1;
            
        }
        private Spell GetSpell()
        {
            
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.减色混合).GetSpell();
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