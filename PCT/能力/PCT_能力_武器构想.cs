using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;
using Nagomi.PCT;

namespace Nagomi.PCT.能力

{
    public class PCT_能力_武器构想 : ISlotResolver
    {
        public int Check()
        {
            if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;

            if (!Core.Resolve<JobApi_Pictomancer>().武器画 || !PCTSpells.重锤构想.IsUnlockWithCDCheck())
            {
                return -1;
            }


            if (Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.武器构想).GetSpell().Charges<1)
            {
                return -3;
            }
            if (Core.Resolve<JobApi_Pictomancer>().武器画&&PCTSpells.重锤构想.IsUnlockWithCDCheck()&&Helper.团辅是否快转好()&&PCTSettings.Instance.OpenLazy)
            {
                return -4;
            }

            if (!QT.QTGET(QTKey.武器构想))
            {
                return -7;
            }

            return 1;
            
        }
        private Spell GetSpell()
        {
            
            return Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.武器构想).GetSpell();
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