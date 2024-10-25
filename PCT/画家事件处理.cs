using System;
using System.Threading.Tasks;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Nagomi.PCT;
using Nagomi.PCT.GCD;
using Nagomi.PCT.Opener;
using Nagomi.PCT.Settings;
using Nagomi.PCT.Triggers;
using Nagomi.PCT.能力;
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
using PCT.utils.Helper;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Nagomi.PCT
{
    public class PictomancerRotationEventHandler : IRotationEventHandler
    {




        public void OnResetBattle()
        {
            PCTBattleData.Instance = new PCTBattleData();
            PCTBattleData.Instance.Reset();
            PictomancerRotationEntry.QT.Reset();
        }

        public async Task OnNoTarget()
        {
            if (!Core.Resolve<JobApi_Pictomancer>().武器画 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.武器彩绘).IsReady() && !Core.Me.IsMoving()&& Helper.是否在副本中())
            {
                await Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.武器彩绘).GetSpell().Cast();
            }

            if (!Core.Resolve<JobApi_Pictomancer>().生物画 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.动物彩绘).IsReady() && !Core.Me.IsMoving()&& Helper.是否在副本中())
            {
                await Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.动物彩绘).GetSpell().Cast();
            }

            if (!Core.Resolve<JobApi_Pictomancer>().风景画 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘).IsReady() && !Core.Me.IsMoving()&& Helper.是否在副本中())
            {
                await Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘).GetSpell().Cast();
            }

            await Task.CompletedTask;
        }

        public void OnSpellCastSuccess(Slot slot, Spell spell)
        {
        
        }

        public async Task OnPreCombat()
        {

            // 检测有没有速行buff或者最近是否使用 (后者是考虑到服务器延迟)
            if (!Core.Resolve<JobApi_Pictomancer>().武器画 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.武器彩绘).IsReady() && Helper.是否在副本中()&& !Core.Me.IsMoving())
            {
                await Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.武器彩绘).GetSpell().Cast();
            }

            if (!Core.Resolve<JobApi_Pictomancer>().生物画 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.动物彩绘).IsReady() && Helper.是否在副本中()&& !Core.Me.IsMoving())
            {
                await Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.动物彩绘).GetSpell().Cast();
            }

            if (!Core.Resolve<JobApi_Pictomancer>().风景画 && Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘).IsReady() && Helper.是否在副本中()&& !Core.Me.IsMoving())
            {
                await Core.Resolve<MemApiSpell>().CheckActionChange(PCTSpells.风景彩绘).GetSpell().Cast();
            }

            await Task.CompletedTask;
        }
        
    

        public void AfterSpell(Slot slot, Spell spell)
        {
           // if (PCTSettings.Instance.音效)

              //  if (spell.Id == PCTSpells.彩虹点滴)
                {
                  //  Core.Resolve<MemApiChatMessage>().Toast("那什么光线!");
                   // PCT.utils.WavePlayer.PlayWavFile("../../ACR/Nagomi/beam.wav");


                }

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