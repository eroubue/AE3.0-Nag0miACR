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
        
        rot.AddOpener(GetOpener);
        
        rot.SetRotationEventHandler(new GNBRotationEventHandler());
       
        rot.AddTriggerAction(new TriggerAction_QT());
        rot.AddTriggerAction(new TriggerAction_HotKey());
        return rot;
    }

    public List<SlotResolverData> SlotResolvers = new()
    {
        new(new GNB能力_续剑(), SlotMode.Always),
        new SlotResolverData(new GNBGCD_音速破(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_子弹连(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_命运之环(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_爆发击(),SlotMode.Gcd),
        new(new GNBGCD_AOEbase(), SlotMode.Gcd),
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
  
        QT = new JobViewWindow( GNBSettings.Instance.JobViewSave,  GNBSettings.Instance.Save, OverlayTitle);
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置
        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("Dev", DrawQtDev);
        //QT.AddTab("ae", 召唤悬浮窗.ae人数查询);
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
        QT.AddQt(QTKey.血壤,true);
        QT.AddQt(QTKey.爆发击,true);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Clear();
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.突进);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.爆发击);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.领域);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.弓形);
       
      
        QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        QT.AddHotkey("极限技", new HotKeyResolver_LB());
        QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
      
        
    }

    public void Dispose()
    {
      
    }
    public void OnUIUpdate()
    {

    }
    
    public void DrawQtGeneral(JobViewWindow JobViewWindow)
    {
        ImGui.Text("测试中,目前仅支持绝神兵和绝亚");
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