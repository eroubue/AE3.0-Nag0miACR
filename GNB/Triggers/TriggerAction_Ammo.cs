using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;

#nullable enable
namespace Nagomi.GNB.Triggers
{
    public class TriggerAction_Ammo : ITriggerCond
    {
        public enum CompareOperation
        {
            大于等于,
            大于,
            等于,
            小于,
            小于等于
        }

        [LabelName("子弹数量")]
        public int Ammo { get; set; }

        [LabelName("比较操作")]
        public CompareOperation Operation { get; set; } = CompareOperation.大于等于;

        public string DisplayName { get; } = "GNB/检测量谱-子弹比较";

        public string Remark { get; set; }

        public bool Draw() => false;

        public static int 绝枪量谱_子弹数() => (int) Core.Resolve<JobApi_GunBreaker>().Ammo;

        public bool Handle(ITriggerCondParams triggerCondParams)
        {
            int currentAmmo = 绝枪量谱_子弹数();

            switch (Operation)
            {
                case CompareOperation.大于等于:
                    return currentAmmo >= this.Ammo;
                case CompareOperation.大于:
                    return currentAmmo > this.Ammo;
                case CompareOperation.等于:
                    return currentAmmo == this.Ammo;
                case CompareOperation.小于:
                    return currentAmmo < this.Ammo;
                case CompareOperation.小于等于:
                    return currentAmmo <= this.Ammo;
                default:
                    return false;
            }
        }
    }
}