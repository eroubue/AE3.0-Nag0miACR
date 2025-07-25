using System;
using System.Threading.Tasks;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Nagomi.SGE;
using Nagomi.SGE.GCD;
using Nagomi.SGE.Opener;
using Nagomi.SGE.Settings;
using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;


namespace Nagomi.SGE
{
    public class SGERotationEventHandler : IRotationEventHandler
    {




        public void OnResetBattle()
        {
            SGEBattleData.Instance = new SGEBattleData();
            SGEBattleData.Instance.Reset();
           
        }

        public async Task OnNoTarget()
        {
            await Task.CompletedTask;
        }

        public void OnSpellCastSuccess(Slot slot, Spell spell)
        {
        
        }

        public async Task OnPreCombat()
        {
            await Task.CompletedTask;
        }
        
    

        public void AfterSpell(Slot slot, Spell spell)
        {


        }

        public void OnBattleUpdate(int currTime)
        {
      
        }

        public void OnEnterRotation()
        {
            Core.Resolve<MemApiChatMessage>().Toast2("零和贤者高难专用ACR，必须配轴！", 2, 5000);

        }

        public void OnExitRotation()
        {
      
        }

        public void OnTerritoryChanged()
        {
        
        }
        

    }
    
}