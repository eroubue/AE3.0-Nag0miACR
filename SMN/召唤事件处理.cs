using System;
using System.Threading.Tasks;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Nagomi.SMN;
using Nagomi.SMN.GCD;
using Nagomi.SMN.Settings;
using Nagomi.SMN.Triggers;
using Nagomi.SMN.能力;
using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using System.Media;
using System.Collections.Generic;
using System.Numerics;
using Dalamud;
using Microsoft.VisualBasic;
using Nagomi.utils.Helper;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Nagomi.SMN
{
    public class SMNRotationEventHandler : IRotationEventHandler
    {




        public void OnResetBattle()
        {
            SMNBattleData.Instance = new SMNBattleData();
            SMNBattleData.Instance.Reset();
            SMNRotationEntry.QT.Reset();
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
    Core.Resolve<MemApiChatMessage>().Toast2("欢迎使用零和ACR\n推荐高难场景使用零和时间轴\n设置面板内附零师傅高级触发器", 1, 3000);
    
}


        public void OnExitRotation()
        {
            Core.Resolve<MemApiChatMessage>().Toast2("感谢陪伴零和ACR\nヾ(￣▽￣)Bye~Bye~", 1, 3000);
        }

        public void OnTerritoryChanged()
        {
        
        }
        

    }
    
}