using AEAssist.CombatRoutine;
using Millusion.ACR.Astrologian.Events;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.SlotResolvers;
using Millusion.ACR.Astrologian.Triggers.Action;
using Millusion.ACR.Astrologian.UI;

namespace Millusion.ACR.Astrologian;

// ReSharper disable once ClassNeverInstantiated.Global
public class AST_RotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "汐ベMoon";

    public Rotation Build(string settingFolder)
    {
        AST_Settings.Build(settingFolder);
        AST_View.BuildUI();
        var rotation = new Rotation(AST_SlotResolvers.SlotResolverDatas)
        {
            TargetJob = Jobs.Astrologian,
            AcrType = AcrType.Both,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "通用占星，目前偏日随，还在优化阶段, 有问题请DC反馈\n" +
                          "更新了一个技能视线检查(防止看不到目标疯狂抽搐尝试释放技能)，未经过充分测试，有问题请及时反馈\n"

        };

        rotation.AddTriggerAction(new AST_Action_QT());

        // 添加各种事件回调
        rotation.SetRotationEventHandler(new AST_RotationEventHandler());

        return rotation;
    }

    public IRotationUI GetRotationUI()
    {
        return AST_View.UI;
    }


    public void OnDrawSetting()
    {
    }

    public void Dispose()
    {
    }
}