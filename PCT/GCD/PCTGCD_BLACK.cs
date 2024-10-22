using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;
using PCT.utils.Helper;

namespace Nagomi.PCT.GCD

{
    public class PCTGCD_BLACK : ISlotResolver
    {
        public int Check()
        {
            
            if (!Core.Resolve<MemApiMove>().IsMoving() && QT.QTGET(QTKey.sb)  && !PCTSpells.减色混合.IsReady())
            {
                return -1;
            }
            if (Core.Resolve<JobApi_Pictomancer>().豆子 == 0 || !Core.Me.HasAura(PCTBuffs.色调反转) || !PCTSpells.彗星之黑.IsReady())
            {
                return -2;
            }
            

            return 1;
            
        }
        private Spell GetSpell()
        {
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.彗星之黑).GetSpell();

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