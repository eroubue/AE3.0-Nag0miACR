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
using Nagomi.utils;
using Nagomi.utils.Helper;

namespace Nagomi.SGE.能力;

public class 心关  : ISlotResolver
{
     public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {   //自动心关关了跳过

        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!QT.QTGET(QTKey.心关))
        {
            return -2;
        }


        //心关在冷却 跳过
        if (!SGESpells.Kardia.IsUnlockWithCDCheck())
            return -3;

        // 获取所有队内的坦克
        var allTanks = PartyHelper.CastableTanks;

        // 如果队内坦克数目小于2 且在8人本
        if (allTanks.Count < 2 && Helper.队伍成员数量 == 8)
        {
            //如果坦克列表的坦克是目标的目标，则给他
            if (PartyHelper.CastableTanks != null)
            {
                if (Core.Me.GetCurrTargetsTarget() != PartyHelper.CastableTanks[0]) return -6;
                if (PartyHelper.CastableTanks[0].HasAura(2605u)) return -5;


               else return 1;
            }
            if (Helper.获取血量最低成员().HasAura(2605u)) return -3;
            else return 2;

        }

        

        // 如果队内坦克数目等于2，且在8人本
        if (allTanks.Count == 2 && Helper.队伍成员数量 == 8)
        {
            if (Core.Me.GetCurrTargetsTarget().HasAura(2605)) return -23;
            return 5;

        }
        
        //除此之外 都不管
        return -1;
    }

    public void Build(Slot slot)
    {

        // 获取所有队内的坦克
        var allTanks = PartyHelper.CastableTanks;

        // 如果队内坦克数目小于2 且在8人本
        if (allTanks.Count < 2 && Helper.队伍成员数量 == 8)
        {
            //如果坦克列表的坦克是目标的目标，则给他
            if (PartyHelper.CastableTanks != null)
            {
                if (Core.Me.GetCurrTargetsTarget() != PartyHelper.CastableTanks[0]) return;
                if (Core.Me.GetCurrTargetsTarget().HasAura(2605)) return;
                slot.Add(new Spell(SGESpells.Kardia, Core.Me.GetCurrTargetsTarget()));
            }
            if (Helper.获取血量最低成员().HasAura(2605)) return;
            slot.Add(new Spell(SGESpells.Kardia, Helper.获取血量最低成员));

        }

        // 如果队内坦克数目等于2，且在8人本
        if (allTanks.Count == 2 && Helper.队伍成员数量 == 8)
        {
            if (Core.Me.GetCurrTargetsTarget().HasAura(2605)) return;

            slot.Add(new Spell(SGESpells.Kardia, Core.Me.GetCurrTargetsTarget()));

        }
        
        else return;
    }
}