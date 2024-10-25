using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
using Nagomi.utils;
using PCT.utils.Helper;

namespace Nagomi.SGE.GCD;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;

public class 复活 : ISlotResolver
{




//复活的调用


    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    public string targetName = "";
    private static IBattleChara? target;

    public int Check()
    {   //即刻没转好不拉

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!SpellsDefine.Swiftcast.IsReady()) return -3;
        if (Helper.自身存在其中Buff(SGEBuffs.无法发动技能类))
        {
            return -3;
        }
        if (Map.不拉人地图.Contains(Helper.当前地图id))
        {
            return -3;
        }
        target = Helper.没有复活状态的死亡队友();
        if (target == null || !target.IsValid())
        {
            return -1;
        }
        if (target.Distance(Helper.自身) > 30)
        {
            return -1;
        }
        if (Helper.目标是否在剧情状态(target))
        {
            return -1;
        }
        if (Helper.目标是否拥有BUFF(SGEBuffs.限制复活))
        {
            return -1;
        }
        if (!target.IsTargetable)
        {
            return -1;
        }
        if (!Helper.目标是否可见或在技能范围内(24287u))
        {
            return -1;
        }

        if (Helper.自身.Position.Y - target.Position.Y > 1)
        {
            if (Helper.副本人数() > 4)
            {
                return -1;
            }
        }
        //拉人QT没开不拉
        if (!QT.QTGET(QTKey.复活)) return -2;
        //蓝量小于2400不拉
        if (Core.Me.CurrentMp < 2400) return -2;
        //其他情况 常开，随时准备拉
        return 1;
    }

    public void Build(Slot slot)
    {   //把死了的人加进目标
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(148u));
        //设定targetname
        // 检查skillTarget是否为null
        if (skillTarget != null && skillTarget.Name != null)
        {
            // 确保skillTarget.Name不为null后再访问TextValue属性
            SGESettings.Instance.targetName = skillTarget.Name.TextValue;
        }
        //即刻加入slot

        slot.Add(SpellsDefine.Swiftcast.GetSpell());
        
        //复活目标加入slot
        slot.Add(new Spell(24287u, skillTarget));
    }
}
