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
using Nagomi.PCT;
using Nagomi.PCT.GCD;
using Nagomi.PCT.Opener;
using Nagomi.PCT.Settings;
using Nagomi.PCT.Triggers;
using Nagomi.PCT.utils;
using Nagomi.PCT.能力;
using Wotou.Dancer.Utility;
using Keys = AEAssist.Define.HotKey.Keys;
using Map = Nagomi.utils.Map;
using Vector2 = System.Numerics.Vector2;
namespace Nagomi.PCT
{

    public class PictomancerRotationEntry : IRotationEntry
    {
        public string OverlayTitle { get; } = "绝本画家";
    
        public string AuthorName { get; set; } = "Nag0mi";
        public Jobs TargetJob { get; } = Jobs.Pictomancer;

        public AcrType AcrType { get; } = AcrType.HighEnd;

        public int MinLevel { get; } = 70;
        public int MaxLevel { get; } = 100;

        public string Description { get; } = "记得搭配零和时间轴使用。";


        public List<SlotResolverData> SlotResolvers = new()
        {
            new(new PCTGCD_天星(), SlotMode.Gcd),
            new(new PCT_能力_减色混合(), SlotMode.OffGcd),
            new(new PCT_能力_风景构想(), SlotMode.OffGcd),
            new(new PCTGCD_BLACK(), SlotMode.Gcd),
            new(new PCTGCD_CYM(), SlotMode.Gcd),
            new(new PCT_能力_马蒂恩(), SlotMode.OffGcd),
            new(new PCT_能力_莫古力(), SlotMode.OffGcd),
            new(new PCT_能力_武器构想(), SlotMode.OffGcd),
            new(new PCTGCD_锤子(), SlotMode.Gcd),
            
            new(new PCTGCD_彩虹(), SlotMode.Gcd),
            new(new PCTGCD_武器彩绘(), SlotMode.Gcd),
            new(new PCTGCD_动物彩绘(), SlotMode.Gcd),
            new(new PCTGCD_风景彩绘(), SlotMode.Gcd),
            new(new PCT_能力_动物构想(), SlotMode.OffGcd),
            new(new PCTGCD_WHITE(), SlotMode.Gcd),
            new(new PCTGCD_RGB(), SlotMode.Gcd),
            
            
            new(new PCT_能力_醒梦(), SlotMode.OffGcd),
       


        };

        public Rotation Build(string settingFolder)
        {
            PCTSettings.Build(settingFolder);
            BuildQT();
            var rot = new Rotation(SlotResolvers)
            {
                TargetJob = Jobs.Pictomancer,
                AcrType = AcrType.HighEnd,
                MinLevel = 70,
                MaxLevel = 100,
                Description = "2.0重构版本\n支持五绝，需要QT和支持请联系作者",
            };
            
            rot.AddOpener(GetOpener);
            rot.SetRotationEventHandler(new PictomancerRotationEventHandler());
            rot.AddTriggerAction(new TriggerAction_NewQt());
            rot.AddTriggerAction(new TriggerAction_QT());
            rot.AddTriggerAction(new TriggerAction_HotKey());
            rot.AddTriggerAction(new TriggerAction_LazyCast());
            return rot;
        }
        IOpener GetOpener(uint level)
        {
            if (level == 100 && Helper.是否在副本中())
                if(Helper.副本人数()==8)return  new PCT_Opener100();
            if (level == 70&& Helper.是否在副本中() && Map.高难地图.Contains(Helper.当前地图id))
                return new PCT_Opener70();
            if ( level == 80&& Helper.是否在副本中() && Map.高难地图.Contains(Helper.当前地图id))
                return new PCT_Opener80();
            if (level == 90&& Helper.是否在副本中() && Map.高难地图.Contains(Helper.当前地图id))
                return new PCT_Opener90();
            return null;
        }
        // 声明当前要使用的UI的实例 示例里使用QT
        public static JobViewWindow QT { get; private set; }
        public static HotkeyWindow? JoystickWindow { get; set; }
    
        // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
        public IRotationUI GetRotationUI()
        {
            return QT;
        }

        private PCTSettingUI settingUI = new();
        public void OnDrawSetting()
        {
            settingUI.Draw();
        }

        // 构造函数里初始化QT
        public void BuildQT()
        {
            QT = new JobViewWindow( PCTSettings.Instance.JobViewSave,  PCTSettings.Instance.Save, OverlayTitle);
            //jobViewWindow.AddTab("日志", _lazyOverlay.更新日志);
            
             QT.AddTab("通用", 画家悬浮窗.通用);
             QT.AddTab("DEV", 画家悬浮窗.DrawDev);
             QT.SetUpdateAction(() =>
             {
                 JoystickHotkeyWindowManager.DrawOrUpdateHotkeyWindow(new QtStyle(PCTSettings.Instance.JobViewSave));
                 var enAvantViewSave = new JobViewSave();
                 enAvantViewSave.LockWindow = PCTSettings.Instance.isEnAvantPanelLocked;
             });

             QT.AddQt(QTKey.减色混合,true);
             QT.AddQt(QTKey.AOE,true);
             QT.AddQt(QTKey.CYM,true);
             QT.AddQt(QTKey.RGB,true);
             QT.AddQt(QTKey.sb,true);
             QT.AddQt(QTKey.锤连击,false);
             QT.AddQt(QTKey.动物彩绘,true);
             QT.AddQt(QTKey.武器彩绘,false);
             QT.AddQt(QTKey.风景彩绘,true);
             QT.AddQt(QTKey.动物构想,true);
             QT.AddQt(QTKey.武器构想,false);
             QT.AddQt(QTKey.风景构想,true);
             QT.AddQt(QTKey.莫古力激流,true);
             QT.AddQt(QTKey.马蒂恩惩罚,true);
             QT.AddQt(QTKey.保留1层锤,false);
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Clear();
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.RGB);
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.动物彩绘);
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.武器彩绘);
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.风景彩绘);
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.莫古力激流);
             PCTSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.马蒂恩惩罚);
             QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
             QT.AddHotkey("极限技", new HotKeyResolver_LB());
             QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
             QT.AddHotkey("昏乱", new HotKeyResolver_NormalSpell(7560, SpellTargetType.Target, false));
             QT.AddHotkey("盾", new HotKeyResolver_NormalSpell(34685, SpellTargetType.Self, false));
             QT.AddHotkey("群盾", (IHotkeyResolver)new 群盾());
             QT.AddHotkey("屏幕位移", (IHotkeyResolver)new 速涂());
             QT.AddHotkey("tp魔纹", (IHotkeyResolver)new mousebuff());
            

        }

        public class 速涂 : IHotkeyResolver
        {
            public void Draw(System.Numerics.Vector2 size)
            {
                System.Numerics.Vector2 size1 = size * 0.8f;
                ImGui.SetCursorPos(size * 0.1f);
                IDalamudTextureWrap idalamudTextureWrap;

                if (Core.Resolve<MemApiIcon>().GetActionTexture(34684U, out idalamudTextureWrap, true))
                {
                    if (idalamudTextureWrap != null)
                    {
                        ImGui.Image(idalamudTextureWrap.ImGuiHandle, size1);
                    }
                    else
                    {
                        Console.WriteLine("Failed to load texture for action ID: 29054");
                    }
                }
            }

            public void DrawExternal(Vector2 size, bool isActive)
            {
                SpellHelper.DrawSpellInfo(new Spell(34684U, (IBattleChara)Core.Me), size, isActive);
            }

            public int Check() => 0;

            public void Run()
            {
                float rotation = CameraHelper.GetCameraRotation();
                var pos = Core.Me.Position;
                 if (AI.Instance.BattleData.NextSlot == null && PCTSpells.速涂.GetSpell().IsReadyWithCanCast())
                {
                    
                    Core.Resolve<MemApiMoveControl>().Stop();
                    Core.Resolve<MemApiMove>().SetRot(rotation);
                    KeyHelper.Send(Keys.W);
                    AI.Instance.BattleData.NextSlot = new Slot(1500);
                    AI.Instance.BattleData.NextSlot.Add(new Spell(34684U, Core.Me));
                   // AI.Instance.BattleData.NextSlot.AddDelaySpell(500, new Spell(34684U, Core.Me));
                }
                else
                {   
                    Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
                }

               


            }
        }
        public class mousebuff : IHotkeyResolver
        {
            public void Draw(System.Numerics.Vector2 size)
            {
                System.Numerics.Vector2 size1 = size * 0.8f;
                ImGui.SetCursorPos(size * 0.1f);
                IDalamudTextureWrap idalamudTextureWrap;

                if (Core.Resolve<MemApiIcon>().GetActionTexture(34675, out idalamudTextureWrap, true))
                {
                    if (idalamudTextureWrap != null)
                    {
                        ImGui.Image(idalamudTextureWrap.ImGuiHandle, size1);
                    }
                    else
                    {
                        // 处理 idalamudTextureWrap 为 null 的情况
                        // 例如：显示默认图标或错误消息
                        Console.WriteLine("Failed to load texture for action ID: 34675");
                    }
                }
            }

            public void DrawExternal(Vector2 size, bool isActive)
            {
                SpellHelper.DrawSpellInfo(new Spell(34675, (IBattleChara)Core.Me), size, isActive);
            }

            public int Check() => 0;

            public void Run()
            {
                var nowpos = Core.Me.Position;
                if (AI.Instance.BattleData.NextSlot == null && PCTSpells.风景构想.GetSpell().IsReadyWithCanCast())
                {
                    
                    Core.Resolve<MemApiMove>().SetPosReturn(Core.Resolve<MemApiMove>().MousePos(),nowpos,900);
                    Core.Resolve<MemApiSendMessage>().SendMessage("/ac 风景构想");
              
                }
                else
                {   
                    Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
                }

                
                
                
               
                
            }
        }
        public class 群盾 : IHotkeyResolver
        {
            public void Draw(System.Numerics.Vector2 size)
            {
                System.Numerics.Vector2 size1 = size * 0.8f;
                ImGui.SetCursorPos(size * 0.1f);
                IDalamudTextureWrap idalamudTextureWrap;

                if (Core.Resolve<MemApiIcon>().GetActionTexture(34686U, out idalamudTextureWrap, true))
                {
                    if (idalamudTextureWrap != null)
                    {
                        ImGui.Image(idalamudTextureWrap.ImGuiHandle, size1);
                    }
                    else
                    {
                        // 处理 idalamudTextureWrap 为 null 的情况
                        // 例如：显示默认图标或错误消息
                        Console.WriteLine("Failed to load texture for action ID: 29054");
                    }
                }
            }

            public void DrawExternal(Vector2 size, bool isActive)
            {
                SpellHelper.DrawSpellInfo(new Spell(34686U, (IBattleChara)Core.Me), size, isActive);
            }

            public int Check() => 0;

            public void Run()
            {
                if (AI.Instance.BattleData.NextSlot == null && PCTSpells.坦培拉涂层.GetSpell().IsReadyWithCanCast())
                {
                    AI.Instance.BattleData.NextSlot = new Slot(1500);
                    AI.Instance.BattleData.NextSlot.AddDelaySpell(200, new Spell(34685U, Core.Me));
                    AI.Instance.BattleData.NextSlot.AddDelaySpell(200, new Spell(34686U, Core.Me));
                }
                else
                {   
                    Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
                }

               


            }
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