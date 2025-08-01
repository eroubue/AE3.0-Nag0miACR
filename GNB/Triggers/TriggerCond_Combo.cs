using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace Nagomi.GNB.Triggers
{
    public class TriggerCond_Combo : ITriggerCond
    {
        [LabelName("上一个连击gcd进度(1-3)")]
        public int Combo { get; set; }

        public string DisplayName { get; } = "GNB/检测连击进度";

        public string Remark { get; set; }

        public bool Draw() => false;

        public bool Handle(ITriggerCondParams triggerCondParams)
        {
            var lastComboSpellId = Core.Resolve<MemApiSpell>().GetLastComboSpellId();
            DetermineComboStage(lastComboSpellId);
            return true; // 或者返回适当的布尔值以符合你的业务逻辑
        }

        private void DetermineComboStage(uint spellId)
        {
            switch (spellId)
            {
                case 16137: // 利刃斩 - 单体1
                case 16146: // 烈牙 - 子弹1
                case 36937: // 崛起之心 - 狮心1
                case 16141: // 恶魔切 - AOE1    
                    Combo = 1;
                    break;
                case 16139: // 残暴弹 - 单体2
                case 16147: // 猛兽爪 - 子弹2
                case 36938: // 支配之心 - 狮心2
                case 16149://AOE2
 
                    Combo = 2;
                    break;
                
                case 16145: // 迅连斩 - 单体3
                case 16150: // 凶禽爪 - 子弹3
                case 36939: // 终结之心 - 狮心3
                    Combo = 3;
                    break;
                default:
                    Combo = 0; // 如果没有匹配到任何已知的连击技能，则设为0
                    break;
            }
        }
    }
}