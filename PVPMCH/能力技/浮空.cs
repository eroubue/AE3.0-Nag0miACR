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
using PCT.utils.Helper;

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
            if (Helper.GCD剩余时间()<500)
            {
                return -999;
            }
            if (!PVPMCHSpells.象式浮空炮塔.IsReady())
            {
                return -1;
            }
            var canTargetObjects = TargetHelper.GetMostCanTargetObjects(PVPMCHSpells.象式浮空炮塔, 4);

            //检测量谱使用Core.Resolve<JobApi_PVPMCHNAME>().量谱名称
            if (Helper.GCD剩余时间()>=500 && PVPMCHSpells.象式浮空炮塔.IsReady() && canTargetObjects != null &&
                canTargetObjects.IsValid() && PVPMCHRotationEntry.QT.GetQt(QTKey.浮游炮))
            {
                return 12;
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