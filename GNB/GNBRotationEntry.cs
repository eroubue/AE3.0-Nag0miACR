
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;

using Nagomi.GNB.GCD;
using Nagomi.GNB.Opener;
using Nagomi.GNB.Triggers;

using Nagomi.GNB.能力;
using Nagomi.GNB.Settings;
using Nagomi.GNB.utils;
using Nagomi.SGE.Opener;


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
            Description = "零师傅高难绝枪,2.5gcd",
        };

        IOpener GetOpener(uint level)
        {
            if (GNBSettings.Instance.opener == 1)
                return new 零弹120起手();
            if (GNBSettings.Instance.opener == 2)
                return new 二弹120起手();
            if (GNBSettings.Instance.opener == 3)
                return new 绝枪70级绝神兵起手();
            if (GNBSettings.Instance.opener == 4)
                return new 无情2g起手();
            if (GNBSettings.Instance.opener == 5)
                return new 绝亚起手();
            

            return new 二弹120起手();
        }
        
        rot.AddOpener(GetOpener);
        rot.SetRotationEventHandler(new GNBRotationEventHandler());
        rot.AddTriggerAction(new TriggerAction_NewQt());
        rot.AddTriggerAction(new TriggerAction_QT());
        rot.AddTriggerAction(new TriggerAction_HotKey());
        rot.AddTriggerAction(new 绝枪时间轴配置设置());
        rot.AddTriggerCondition(new TriggerAction_Ammo());
        return rot;
    }

    public List<SlotResolverData> SlotResolvers = new()
    {
        new(new GNB能力_续剑(), SlotMode.Always),
        new SlotResolverData(new GNBGCD_70音速破(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_子弹连(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_倍攻(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_音速破(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_狮心连(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_命运之环(),SlotMode.Gcd),
        new SlotResolverData(new GNBGCD_爆发击(),SlotMode.Gcd),
        new(new GNBGCD_AOEbase(), SlotMode.Gcd),
        new(new GNBGCD_base(), SlotMode.Gcd),
        new(new GNBGCD_闪雷弹(), SlotMode.Gcd),
        new(new GNB能力_无情(), SlotMode.OffGcd),
        new(new GNB能力_领域(), SlotMode.OffGcd),
        new(new GNB能力_弓形冲波(), SlotMode.OffGcd),
        new(new GNB能力_血壤(), SlotMode.OffGcd),
        
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
        QT.AddTab("通用", 绝枪悬浮窗.通用);
        QT.AddTab("Dev", 绝枪悬浮窗.DrawDev);
        //QT.AddTab("ae", 贤者悬浮窗.ae人数查询);
        QT.AddQt(QTKey.停手,false);
        QT.AddQt(QTKey.爆发,true);
        QT.AddQt(QTKey.倾泻爆发,false);
        QT.AddQt(QTKey.AOE,true);
        QT.AddQt(QTKey.无情,true);
        QT.AddQt(QTKey.无情不延后,true,"好了就用");
        QT.AddQt(QTKey.子弹连,true);
        QT.AddQt(QTKey.领域,true);
        QT.AddQt(QTKey.音速破,true);
        QT.AddQt(QTKey.弓形,true);
        QT.AddQt(QTKey.突进起手,true);
        QT.AddQt(QTKey.血壤,true);
        QT.AddQt(QTKey.爆发击,true);
        QT.AddQt(QTKey.dot,true);
        QT.AddQt(QTKey.狮心连,true);
        QT.AddQt(QTKey.倍攻,true);
        QT.AddQt(QTKey.二弹,true,new Action<bool>(GNB小帮手.互斥二弹120));
        QT.AddQt(QTKey.零弹,false,new Action<bool>(GNB小帮手.互斥零弹120));
        QT.AddQt(QTKey.闪雷弹,true);
        QT.AddQt(QTKey.命运之环,true);
        QT.AddQt(QTKey.仅使用爆发击卸除子弹,false);
        QT.AddQt(QTKey.小于3目标时不用弓形,false);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Clear();
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.二弹);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.零弹);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.爆发击);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.领域);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.弓形);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.音速破);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.无情不延后);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.命运之环);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.仅使用爆发击卸除子弹);
        GNBSettings.Instance.JobViewSave.QtUnVisibleList.Add(QTKey.小于3目标时不用弓形);
        
       
      
        QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7548, SpellTargetType.Self, false));
        QT.AddHotkey("极限技", new HotKeyResolver_LB());
        QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
        QT.AddHotkey("退避对位t", (IHotkeyResolver)new 退避对位t());
        QT.AddHotkey("支援减对位T", (IHotkeyResolver)new 支援减对位T());
        QT.AddHotkey("hot对位T", (IHotkeyResolver)new hot对位T());
        QT.AddHotkey("支援减最低血量", (IHotkeyResolver)new 支援减最低血量());
        QT.AddHotkey("hot最低血量", (IHotkeyResolver)new hot最低血量());
        QT.AddHotkey("停止移动", (IHotkeyResolver)new StopMoveHotkeyResolver());
      
        
    }

    public void Dispose()
    {
      
    }
    public void OnUIUpdate()
    {

    }
    


}