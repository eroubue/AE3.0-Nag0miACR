using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using System.Runtime;
using Nagomi.SGE;
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
namespace Nagomi.SGE.能力;

public class 根素 : ISlotResolver
{
  
    
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {   //没开也别用了

            if (QT.QTGET(QTKey.停手))
            {
                return -100;
            }
            if (!SGESpells.根素.IsReady())
                return -66;
            if (QT.QTGET(QTKey.根素))
            {
                return -1;
            }
            if (Core.Me.Level < 76) return -3;
         
            if (Core.Resolve<JobApi_Sage>().Addersgall >= 2) return -2;
            
            

            return 0;
        }

        public void Build(Slot slot)
        {   //心神风息
            slot.Add(SGESpells.根素.GetSpell());
        }
    
}