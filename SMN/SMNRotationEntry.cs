using System.Collections.Generic;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons.Automation.NeoTaskManager.Tasks;
using ECommons.DalamudServices;
using ECommons.Reflection;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using Nagomi.SMN;
using Nagomi.SMN.GCD;
using Nagomi.SMN.Settings;
using Nagomi.SMN.Triggers;
using Nagomi.PCT.utils;
using Nagomi.utils;
using Nagomi.SMN.能力;
using Nagomi.SMN.GCD;
using Nagomi.utils.Helper;
using Keys = AEAssist.Define.HotKey.Keys;
using Map = Nagomi.utils.Map;
using Vector2 = System.Numerics.Vector2;

namespace Nagomi
{

    public class SMNRotationEntry : IRotationEntry
    {
        public string OverlayTitle { get; } = "70召唤";
    
        public string AuthorName { get; set; } = "Nag0mi";


        public List<SlotResolverData> SlotResolvers = new()
        {
            new(new SMNGCD_BASE(), SlotMode.Gcd),
            
            new(new SMN_能力_醒梦(), SlotMode.OffGcd),
       


        };

        public Rotation Build(string settingFolder)
        {
            SMNSettings.Build(settingFolder);
            BuildQT();
            var rot = new Rotation(SlotResolvers)
            {
                TargetJob = Jobs.Summoner,
                AcrType = AcrType.HighEnd,
                MinLevel = 70,
                MaxLevel = 70,
                Description = "70神兵自动专用",
            };
            
            rot.AddOpener(GetOpener);
            rot.SetRotationEventHandler(new SMNRotationEventHandler());
            rot.AddTriggerAction(new TriggerAction_QT());
            rot.AddTriggerAction(new TriggerAction_HotKey());
            return rot;
        }
        IOpener GetOpener(uint level)
        {
            return null;
        }
        // 声明当前要使用的UI的实例 示例里使用QT
        public static JobViewWindow QT { get; private set; }
    
        // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
        public IRotationUI GetRotationUI()
        {
            return QT;
        }

        private SMNSettingUI settingUI = new();
        public void OnDrawSetting()
        {
            settingUI.Draw();
        }

        // 构造函数里初始化QT
        public void BuildQT()
        {
            QT = new JobViewWindow( SMNSettings.Instance.JobViewSave,  SMNSettings.Instance.Save, OverlayTitle);
            //jobViewWindow.AddTab("日志", _lazyOverlay.更新日志);
            QT.AddTab("通用", 召唤悬浮窗.通用);
            QT.AddTab("DEV", 召唤悬浮窗.DrawDev);
            //QT.AddTab("ae", 召唤悬浮窗.ae人数查询);
            //QT.AddTab("log", LogModifier.DrawLogModifierTab);


            QT.AddQt(QTKey.AOE,true);

            SMNSettings.Instance.JobViewSave.QtUnVisibleList.Clear();
            SMNSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.RGB);
            QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
            QT.AddHotkey("昏乱", new HotKeyResolver_NormalSpell(7560, SpellTargetType.Target, false));
            QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
            


            

        }
        

        public void Dispose()
        {
            // TODO 轮盘赌
            //TODO 占卜
            //TODO logs
            //TODO 底裤飞雷神
        }
    }
}