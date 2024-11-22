using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Nagomi.SGE.utils;
using Nagomi.utils.Helper;

namespace Nagomi.SGE.能力;

public class SGE醒梦 : ISlotResolver
{
   
    
        public int Check()
        {

            if (Core.Me.CurrentMp <= 6700 && SGESpells.醒梦.IsUnlockWithCDCheck()&&!(QT.QTGET(QTKey.停手))&&!Helper.技能是否刚使用过(37033U,1500)&&Core.Resolve<MemApiSpell>().GetCharges(37033)>=0.02)
            {
                return 1;
            }
            return -1;
            
        }
        private Spell GetSpell()
        {
            
            return Core.Resolve<MemApiSpell>().CheckActionChange(SGESpells.醒梦).GetSpell();
        }
        public void Build(Slot slot)
        {
            var spell = GetSpell();
            if (spell == null)
                return;
            slot.Add(spell);
        }
    
}