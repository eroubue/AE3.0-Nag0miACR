using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;
using Nagomi.utils.Helper;

namespace Nagomi.SMN.能力

{
    public class SMN_能力_醒梦 : ISlotResolver
    {
        public int Check()
        {

            if (Core.Me.CurrentMp <= 6700 && PCTSpells.醒梦.IsUnlockWithCDCheck())
            {
                return 1;
            }
            return -1;
            
        }
        private Spell GetSpell()
        {
            
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.醒梦).GetSpell();
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