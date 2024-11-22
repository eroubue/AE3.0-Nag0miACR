using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;
using Nagomi.PCT;
using Nagomi.utils.Helper;

namespace Nagomi.PCT.能力

{
    public class PCT_能力_马蒂恩 : ISlotResolver
    {
        public int Check()
        {
            if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;

            if (!Core.Resolve<JobApi_Pictomancer>().蔬菜准备)
            {
                return -1;
            }
            if (!Helper.自身存在Buff(PCTBuffs.星空构想))
            {
                return -20;
            }
            if (GCDHelper.GetGCDCooldown() < 600)
            {
                return -2;
            }
            if (Core.Resolve<JobApi_Pictomancer>().蔬菜准备&&PCTSpells.马蒂恩惩罚.IsUnlockWithCDCheck()&&Helper.团辅是否快转好()&&PCTSettings.Instance.OpenLazy)
            {
                return -4;
            }

            if ((PCTSpells.马蒂恩惩罚).GetSpell().Charges<1)
            {
                return -3;
            }
            if (!QT.QTGET(QTKey.马蒂恩惩罚))
            {
                return -7;
            }

            return 1;
            
        }
        private Spell GetSpell()
        {
            
            return (PCTSpells.马蒂恩惩罚).GetSpell();
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