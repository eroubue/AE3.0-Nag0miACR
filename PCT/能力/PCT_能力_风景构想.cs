using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using ECommons.Automation.NeoTaskManager.Tasks;
using Nagomi.PCT;
    
namespace Nagomi.PCT.能力

{
    public class PCT_能力_风景构想 : ISlotResolver
    {
        public int Check()
        {

            if (!Core.Resolve<JobApi_Pictomancer>().风景画||!PCTSpells.星空构想.IsReady())
            {
                return -1;
            }
            if (GCDHelper.GetGCDCooldown() > 1450)
            {
                return -2;
            }

            if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景构想).GetSpell().Charges<1)
            {
                return -3;
            }
            if (!QT.QTGET(QTKey.风景构想))
            {
                return -7;
            }

            return 1;
            
        }
        public void Build(Slot slot)
        {
            slot.AddDelaySpell(200, new Spell(PCTSpells.星空构想, Core.Me));
        }
    }
}