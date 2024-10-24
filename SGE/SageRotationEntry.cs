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
using Nagomi.SGE.GCD;
using Nagomi.SGE.Opener;
using Nagomi.SGE.Settings;
using Nagomi.SGE.Triggers;
using Nagomi.SGE.utils;
using Nagomi.SGE.能力;
using Nagomi.依赖.Helper;
using Map = Nagomi.utils.Map;

namespace Nagomi.SGE;

public class SGERotationEntry : IRotationEntry
{
    public string OverlayTitle { get; } = "贤者高难";

    public string AuthorName { get; set; } = "Nag0mi";
    public Jobs TargetJob { get; } = Jobs.Sage;

    public AcrType AcrType { get; } = AcrType.HighEnd;

    public int MinLevel { get; } = 70;
    public int MaxLevel { get; } = 100;

    public string Description { get; } = "记得搭配零和时间轴使用。本acr只有输出功能!";


    public List<SlotResolverData> SlotResolvers = new()
    {
        new(new 心关(), SlotMode.OffGcd),
        new(new 根素(), SlotMode.OffGcd),
        new(new 心神风息(), SlotMode.OffGcd),
        new(new SGE醒梦(), SlotMode.OffGcd),
        new(new GCD绝亚康复(), SlotMode.Gcd),
        new(new 复活(), SlotMode.Gcd),
        new(new GCD康复(), SlotMode.Gcd),
        new(new GCD_Dot(), SlotMode.Gcd),
        new(new 发炎(), SlotMode.Gcd),
        new(new 箭毒(), SlotMode.Gcd),
        new(new GCDbase(), SlotMode.Gcd),
        





    };

    public Rotation Build(string settingFolder)
    {
        SGESettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Sage,
            AcrType = AcrType.HighEnd,
            MinLevel = 70,
            MaxLevel = 100,
            Description = "记得搭配时间轴使用。本acr只有输出功能!",
        };

        rot.AddOpener(GetOpener);
        rot.SetRotationEventHandler(new SGERotationEventHandler());
        rot.AddTriggerAction(new TriggerAction_QT());
        rot.AddTriggerCondition(new 贤者时间轴蓝豆状态(), new 贤者时间轴红豆状态());
     

        return rot;
    }

    IOpener GetOpener(uint level)
    {
        if (SGESettings.Instance.opener == 1)
            return new 贤炮起手();

        return new 贤者起手();
    }

    // 声明当前要使用的UI的实例 示例里使用QT
    public static JobViewWindow QT { get; private set; }

    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }

    private SGESettingUI settingUI = new();

    public void OnDrawSetting()
    {
        settingUI.Draw();
    }

    // 构造函数里初始化QT
    public void BuildQT()
    {
        QT = new JobViewWindow(SGESettings.Instance.JobViewSave, SGESettings.Instance.Save, OverlayTitle);
        //jobViewWindow.AddTab("日志", _lazyOverlay.更新日志);
        //jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        QT.AddTab("通用",xuanfuchuang);
        QT.AddTab("DEV",DrawDev);

        QT.AddQt(QTKey.停手, false);
        QT.AddQt(QTKey.DOT, true);
        QT.AddQt(QTKey.AOE, true);
        QT.AddQt(QTKey.红豆, true);
        QT.AddQt(QTKey.保留红豆, true);
        QT.AddQt(QTKey.心神风息, true);
        QT.AddQt(QTKey.发炎, true);
        QT.AddQt(QTKey.爆发, true);
        QT.AddQt(QTKey.复活, true);
        QT.AddQt(QTKey.康复, true);
        QT.AddQt(QTKey.根素, true);
        QT.AddQt(QTKey.心关, true);






        SGERotationEntry.QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        SGERotationEntry.QT.AddHotkey("极限技", new HotKeyResolver_LB());
        SGERotationEntry.QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
        SGERotationEntry.QT.AddHotkey("疾跑", new HotKeyResolver_疾跑());
        
        QT.AddHotkey("群盾", (IHotkeyResolver)new 群盾());
        QT.AddHotkey("群盾消化", (IHotkeyResolver)new 群盾消化());
        QT.AddHotkey("即刻贤炮", (IHotkeyResolver)new 即刻贤炮());
        QT.AddHotkey("混合最低血量", (IHotkeyResolver)new 混合最低血量());
        QT.AddHotkey("单盾T", (IHotkeyResolver)new 单盾T());
        QT.AddHotkey("单盾最低血量", (IHotkeyResolver)new 单盾最低血量());
        QT.AddHotkey("神翼T", (IHotkeyResolver)new 神翼T());
        QT.AddHotkey("营救最远", (IHotkeyResolver)new 营救最远());


    }

    public void xuanfuchuang(JobViewWindow jobViewWindow)
    {
            ImGui.SameLine();
            if (SGESettings.Instance.H1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 0.0f, 0.0f, 1.0f), "H1"); 
            }
            else if (!SGESettings.Instance.H1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "H2"); 
            }
            ImGui.SameLine();
            if (ImGui.Button(" H1 "))
            {
                SGESettings.Instance.H1 = true;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button(" H2 "))
            {
                SGESettings.Instance.H1 = false;
                SGESettings.Instance.Save();
            }

            
            if (ImGui.Button("失衡走位关"))
            {
                SGESettings.Instance.失衡走位 = 0;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("失衡走位开"))
            {
                SGESettings.Instance.失衡走位 = 1;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            ImGui.Text("失衡走位：");
            ImGui.SameLine();
            if (SGESettings.Instance.失衡走位 == 0)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f), "关"); // 绿色
            }
            else if (SGESettings.Instance.失衡走位 == 1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 1.0f, 0.0f, 1.0f), "开"); // 蓝色
            }
            ImGui.Text("当前起手：");
            ImGui.SameLine();
            if (SGESettings.Instance.opener == 0)
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "默认起手"); // 绿色
            }
            else if (SGESettings.Instance.opener == 1)
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 0.3f, 0.8f, 1.0f), "贤炮起手"); // 蓝色
            }

            if (ImGui.Button("默认起手"))
            {
                SGESettings.Instance.opener = 0;
                SGESettings.Instance.Save();
            }
            ImGui.SameLine();
            if (ImGui.Button("贤炮起手"))
            {
                SGESettings.Instance.opener = 1;
                SGESettings.Instance.Save();
            }
            if (ImGui.Button("获取触发器链接"))
            {
                Core.Resolve<MemApiChatMessage>().Toast2("感谢使用零师傅工具箱\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
                Core.Resolve<MemApiSendMessage>().SendMessage("/e https://11142.kstore.space/TriggernometryExport.xml");
            }
            ;
            ImGui.Text("导入到act高级触发器插件的远程触发器中，使用前请更新!");
    }

    public void DrawDev(JobViewWindow jobViewWindow)
    {
        
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
    
}