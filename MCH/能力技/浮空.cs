using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.PvP.PVPApi;
using Nagomi.PVPMCH;
using Nagomi.PVPMCH.依赖;


namespace Nagomi.PVPMCH.能力

{
    public class 浮空 : ISlotResolver
    {
        public int Check()
        {
            if (!PVPHelper.CanActive())
            {
                return -999;
            }
            if (!PVPMCHRotationEntry.QT.GetQt(QTKey.浮游炮))
            {
                return -7;
            }
            if (PVPHelper.通用距离检查(25))
            {
                return -3;
            }
           
            if (!PVPMCHSpells.象式浮空炮塔.IsReady())
            {
                return -1;
            }
    
            //身上buff检测

            return -1;
            
        }
        private Spell GetSpell()
        {
             var canTargetObjects = TargetHelper.GetMostCanTargetObjects(PVPMCHSpells.象式浮空炮塔, 4);

            return new Spell(PVPMCHSpells.象式浮空炮塔, canTargetObjects) { WaitServerAcq = false };
            
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