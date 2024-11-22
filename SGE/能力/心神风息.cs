using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using System.Runtime;
using Nagomi.GNB.utils;
using Nagomi.SGE;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
namespace Nagomi.SGE.能力;

public class 心神风息 : ISlotResolver
{
  
    
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {   //没开也别用了

            if (QT.QTGET(QTKey.停手))
            {
                return -100;
            }
            if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
            if (!SGESpells.心神风息.IsUnlockWithCDCheck())
                return -66;
            if (Core.Me.Distance(Core.Me.GetCurrTarget()) > 25+SGESettings.Instance.额外技能距离) return -1;
            if (QT.QTGET(QTKey.爆发)) return 100;
            if (!QT.QTGET(QTKey.心神风息))
            {
                return -1;
            }
           
            
            return 0;
        }

        public void Build(Slot slot)
        {   
            slot.Add(SGESpells.心神风息.GetSpell());
        }
    
}