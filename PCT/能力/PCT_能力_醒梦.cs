using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PCT;
using PCT.utils.Helper;

namespace Nagomi.PCT.能力

{
    public class PCT_能力_醒梦 : ISlotResolver
    {
        public int Check()
        {

            if (Core.Me.CurrentMp <= 6800 && PCTSpells.醒梦.IsReady())
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