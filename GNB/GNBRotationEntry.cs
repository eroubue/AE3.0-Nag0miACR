using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using System;
using System.Threading.Tasks;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using ImGuiNET;
using Nagomi.GNB;
using Nagomi.GNB.GCD;
using Nagomi.GNB.Opener;
using Nagomi.GNB.Triggers;
using Nagomi.GNB.utils;
using Nagomi.GNB.能力;
using Nagomi.GNB.Settings;
using Nagomi.PCT;

namespace Nagomi.GNB;

public class GNBRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "Nag0mi";//作者名字
    public string OverlayTitle { get; } = "Title";
    public Rotation Build(string settingFolder)
    {
        // 初始化设置
        GNBSettings.Build(settingFolder);
        // 初始化QT （依赖了设置的数据）
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Gunbreaker,//acr职业
            AcrType = AcrType.HighEnd,//acr类型，both=通用,Normal=日常,HighEnd=高难
            MinLevel = 70,//支持最小等级
            MaxLevel = 100,//支持最大等级
            Description = "零师傅高难绝枪,目前支持绝神兵和绝亚超完美st，后续绝本轴待定更新（看心情）",
        };
        IOpener GetOpener(uint level)
        {
            
                return null;
          
        }
        // 添加opener
        rot.AddOpener(GetOpener);
        // 添加各种事件回调
        rot.SetRotationEventHandler(new GNBRotationEventHandler());
        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new TriggerAction_QT());
        return rot;
    }
    // 逻辑从上到下判断，通用队列是无论如何都会判断的 
    // gcd则在可以使用gcd时判断
    // offGcd则在不可以使用gcd 且没达到gcd内插入能力技上限时判断
    // pvp环境下 全都强制认为是通用队列
    // 重要 类一定要Public声明才会被查找到

    public List<SlotResolverData> SlotResolvers = new()
    {
        new(new GNB能力_续剑(), SlotMode.Always),
        new(new GNBGCD_base(), SlotMode.Gcd),
        new(new GNB能力_血壤(), SlotMode.OffGcd),
        new(new GNB能力_无情(), SlotMode.OffGcd),
        new(new GNB能力_弓形冲波(), SlotMode.OffGcd),
        new(new GNB能力_领域(), SlotMode.OffGcd),
    };
    public static JobViewWindow QT { get; private set; }  // 声明当前要使用的UI的实例 示例里使用QT
    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }
    private GNBSettingUI settingUI = new();//把GNB记得替换掉
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    // 构造函数里初始化QT
    public void BuildQT()
    {
  
        QT = new JobViewWindow( GNBSettings.Instance.GNBViewSave,  GNBSettings.Instance.Save, OverlayTitle);
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置
        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("Dev", DrawQtDev);
        QT.AddTab("ae", 画家悬浮窗.ae人数查询);
        QT.AddQt(QTKey.停手,false);
        QT.AddQt(QTKey.爆发,true);
        QT.AddQt(QTKey.倾泻爆发,false);
        QT.AddQt(QTKey.AOE,true);
        QT.AddQt(QTKey.无情,true);
        QT.AddQt(QTKey.无情后半,true);
        QT.AddQt(QTKey.子弹连,true);
        QT.AddQt(QTKey.领域,true);
        QT.AddQt(QTKey.dot,true);
        QT.AddQt(QTKey.弓形,true);
        QT.AddQt(QTKey.突进,false);
        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
     


  

        // 添加快捷按钮 (带技能图标)
      
        GNBRotationEntry.QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        GNBRotationEntry.QT.AddHotkey("极限技", new HotKeyResolver_LB());
        GNBRotationEntry.QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
        /*
       // 这是一个自定义的快捷按钮 一般用不到
       // 图片路径是相对路径 基于AEAssist(C|E)NVersion/AEAssist
       // 如果想用AE自带的图片资源 路径示例: Resources/AE2Logo.png
       QT.AddHotkey("极限技", new HotkeyResolver_General("#自定义图片路径", () =>
       {
           // 点击这个图片会触发什么行为
           LogHelper.Print("你好");
       }));
       */

        
    }

    public void Dispose()
    {
      
    }
    public void OnUIUpdate()
    {

    }
    
    public void DrawQtGeneral(JobViewWindow JobViewWindow)
    {
        ImGui.Text("测试中未完善！！！");
        ImGui.Text("爆发QT只控制无情和血壤");
    }

    public void DrawQtDev(JobViewWindow JobViewWindow)
    {
        ImGui.Text("画Dev信息");
        foreach (var v in JobViewWindow.GetQtArray())
        {
            ImGui.Text($"Qt按钮: {v}");
        }

        foreach (var v in JobViewWindow.GetHotkeyArray())
        {
            ImGui.Text($"Hotkey按钮: {v}");
        }
    }

}