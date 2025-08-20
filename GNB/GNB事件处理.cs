using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;
using AEAssist.Helper;

namespace Nagomi.GNB
{
    public class GNBRotationEventHandler : IRotationEventHandler
    {
        public void OnResetBattle()//重置战斗
        {
            // 重置战斗中缓存的数据
            // QT的设置重置为默认值
            GNBBattleData.Instance = new GNBBattleData();
            GNBBattleData.Instance.Reset();
        }

        public async Task OnNoTarget()//进战且无目标时
        {
            // 检查周围是否有可选敌对目标
            bool hasTargetableEnemy = false;
            
            // 遍历25米范围内的所有敌人
            if (TargetMgr.Instance.EnemysIn25 != null && TargetMgr.Instance.EnemysIn25.Count > 0)
            {
                foreach (var enemy in TargetMgr.Instance.EnemysIn25.Values)
                {
                    if (enemy != null && enemy.IsValid() && enemy.IsTargetable)
                    {
                        hasTargetableEnemy = true;
                        break;
                    }
                }
            }
            
            // 只有在没有可选敌对目标时才开启落地无情QT
            if (!hasTargetableEnemy)
            {
                QT.QTSET(QTKey.落地无情, true);
            }
            
            await Task.CompletedTask;
        }

        public void OnSpellCastSuccess(Slot slot, Spell spell)//施法成功判定可以滑步时
        {
        
        }

        public async Task OnPreCombat()//脱战时
        {
            await Task.CompletedTask;
        }
        
    

        public void AfterSpell(Slot slot, Spell spell)
        //某个技能使用之后的处理,比如诗人在刷Dot之后记录这次是否是强化buff的Dot 如果是读条技能，则是服务器判定它释放成功的时间点，比上面的要慢一点
        {
            // 如果使用的是无情技能，关闭落地无情QT
            if (spell.Id == GNBSpells.无情&&QT.QTGET(QTKey.落地无情))
            {
                GNB小帮手.延迟关闭落地无情();
            }
        }
        

        public void BeforeSpell(Slot slot, Spell spell)
        {
            if (spell.Id == GNBSpells.烈牙 && QT.QTGET(QTKey.无情) && GNBSpells.无情.GetSpell().IsReadyWithCanCast())
            {
                slot.Insert(new SlotAction(GNBSpells.无情.GetSpell()));
            }
            
        }



        public void OnBattleUpdate(int currTime)//战斗中逐帧检测
        {
      
        }

        public void OnEnterRotation()//切换到当前ACR
        {
            Core.Resolve<MemApiChatMessage>().Toast2("本acr仅输出功能，减伤请手动或配轴使用。", 1, 2000);
        }

        public void OnExitRotation()//退出ACR
        {
      
        }

        public void OnTerritoryChanged()
        {
            // 获取当前地图ID
            var currentMapId = Helper.当前地图id;
            
            // 可以根据地图ID进行不同的处理
            // 例如：某些地图的特殊逻辑、重置特定状态等
            if (currentMapId == 777)
            {
                 GNBSettings.Instance.opener = 3;
            }

            if (currentMapId == 887)
            {
                GNBSettings.Instance.opener = 5;
            }
        }
        

    }
}