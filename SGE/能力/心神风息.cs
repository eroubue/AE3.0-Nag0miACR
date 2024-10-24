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

public class 心神风息 : ISlotResolver
{
  
    
        public SlotMode SlotMode { get; } = SlotMode.OffGcd;

        public int Check()
        {   //没开也别用了

            if (QT.QTGET(QTKey.停手))
            {
                return -100;
            }
            if (!QT.QTGET(QTKey.心神风息))
            {
                return -1;
            }
            //不让打AOE也别用了
            //if (!Qt.GetQt("AOE")) return -8;
            //等级低于92直接别用了，心神风息没学会呢
            if (Core.Me.Level < 92) return -3;
            //冷却没好不用
            if (!SGESpells.心神风息.IsReady())
                return -66;

            return 0;
        }

        public void Build(Slot slot)
        {   //心神风息
            slot.Add(SGESpells.心神风息.GetSpell());
        }
    
}