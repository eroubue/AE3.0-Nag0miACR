using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist;
using AEAssist.MemoryApi;

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
        
        }
        

    }
}