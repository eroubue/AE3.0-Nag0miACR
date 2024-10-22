using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using System;
using System.Numerics;
using System.Threading.Tasks;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;
using Nagomi.PVPMCH;
using Nagomi.PVPMCH.GCD;
using Nagomi.PvP.PVPApi;
using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using ImGuiNET;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using Nagomi.PVPMCH.依赖;
using Nagomi.PVPMCH.Settings;
using Nagomi.PVPMCH.能力;

namespace Nagomi.PVPMCH;

public class PVPMCHRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "Nag0mi";//作者名字
    public string OverlayTitle { get; } = "Title";
    public Rotation Build(string settingFolder)
    {
        PVPMCHSettings.Build(settingFolder);
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Machinist,//acr职业
            AcrType = AcrType.PVP,//acr类型，both=通用,Normal=日常,HighEnd=高难
            MinLevel = 30,//支持最小等级
            MaxLevel = 100,//支持最大等级
            Description = "pvp机工测试",
        };
  
      
        rot.SetRotationEventHandler(new PVPMCHRotationEventHandler());
        return rot;
    }

    public List<SlotResolverData> SlotResolvers = new()
    {
        new(new 净化(), SlotMode.Always),
        new(new 药(), SlotMode.Always),
        new(new 分析(), SlotMode.Always),
        new(new GCD回转飞锯(), SlotMode.Always),
        new(new GCD毒(), SlotMode.Always),
        new(new GCD钻头(), SlotMode.Always),
        new(new GCD热冲击(), SlotMode.Always),
        new(new GCD空气锚(), SlotMode.Always),
        new(new GCD霰弹枪(), SlotMode.Always),
        new(new 浮空(), SlotMode.Always),
        new(new GCD1(), SlotMode.Always),

    };
    public static JobViewWindow QT { get; private set; }  // 声明当前要使用的UI的实例 示例里使用QT
    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }
    private PVPMCHSettingUI settingUI = new();//把PVPMCH记得替换掉
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    // 构造函数里初始化QT

    
    public void BuildQT()
    {
        QT = new JobViewWindow( PVPMCHSettings.Instance.JobViewSave,  PVPMCHSettings.Instance.Save, OverlayTitle);
        PVPMCHRotationEntry.QT.AddTab("通用", new Action<JobViewWindow>(this.DrawGeneral));
        PVPMCHRotationEntry.QT.AddTab("debug", new Action<JobViewWindow>(this.DrawDev));
        PVPMCHRotationEntry.QT.AddQt("回转飞锯", true);
        PVPMCHRotationEntry.QT.AddQt("毒菌冲击", true);
        PVPMCHRotationEntry.QT.AddQt("蓄力冲击", true);
        PVPMCHRotationEntry.QT.AddQt("热冲击", true);
        PVPMCHRotationEntry.QT.AddQt("空气锚", true);
        PVPMCHRotationEntry.QT.AddQt("钻头", true);
        PVPMCHRotationEntry.QT.AddQt("霰弹枪", true);
        PVPMCHRotationEntry.QT.AddQt("野火", true);
        PVPMCHRotationEntry.QT.AddQt("浮游炮", true);
      PVPMCHRotationEntry.QT.AddQt("分析", true);
      PVPMCHRotationEntry.QT.AddQt("喝热水", true);
      PVPMCHRotationEntry.QT.AddQt("自动净化", true);
      PVPMCHRotationEntry.QT.AddHotkey("霰弹枪", (IHotkeyResolver) new HotKeyResolver_NormalSpell(29404U, (SpellTargetType) 2, false));
      PVPMCHRotationEntry.QT.AddHotkey("疾跑", (IHotkeyResolver) new HotKeyResolver_NormalSpell(29057U, (SpellTargetType) 1, false));
      PVPMCHRotationEntry.QT.AddHotkey("龟壳", (IHotkeyResolver) new PVPHelper.龟壳());
      
    }
#nullable enable



       public  void DrawGeneral(JobViewWindow jobViewWindow)
    { 
      Share.Pull = true;
      PVPHelper.通用设置配置("说明:减色混合按移动和停止切换");
      ImGui.Text("不选敌对目标不会进行任何技能判定");
      ImGui.Separator();
      PVPHelper.技能配置界面(29711U, "喝热水");
      ImGui.InputInt("热水阈值", ref PVPMCHSettings.Instance.药血量, 5, 4);
      ImGui.Separator();
      PVPHelper.技能图标(29405U);
      ImGui.SameLine();
      PVPHelper.技能图标(29406U);
      ImGui.SameLine();
      PVPHelper.技能图标(29407U);
      ImGui.SameLine();
      PVPHelper.技能图标(29408U);
      ImGui.Text("钻头套装");
      ImGui.Checkbox("钻头:有分析时才使用", ref PVPMCHSettings.Instance.钻头分析);
      ImGui.Checkbox("毒菌冲击:有分析时才使用", ref PVPMCHSettings.Instance.毒菌分析);
      ImGui.Checkbox("空气锚:有分析时才使用", ref PVPMCHSettings.Instance.空气锚分析);
      ImGui.Checkbox("回转飞锯:有分析时才使用", ref PVPMCHSettings.Instance.回转飞锯分析);
      ImGui.Checkbox("不以龟壳为目标", ref PVPMCHSettings.Instance.TargetDefend);
      ImGui.Separator();
      PVPHelper.技能图标(29414U);
      ImGui.Text("分析");
      ImGui.Checkbox("钻头套装可用才使用分析", ref PVPMCHSettings.Instance.分析可用);
      ImGui.Separator();
      PVPHelper.技能配置界面(29409U, "过热野火");
      ImGui.Checkbox("过热野火", ref PVPMCHSettings.Instance.过热野火);
      ImGui.Separator();
      PVPMCHSettings.Instance.Save();
    }

    public void DrawDev(JobViewWindow jobViewWindow)
    {
      
        Share.Pull = true;
         DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
      interpolatedStringHandler.AppendLiteral("gcd:");
      interpolatedStringHandler.AppendFormatted<int>(GCDHelper.GetGCDCooldown());
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
      interpolatedStringHandler.AppendLiteral("自己：");
      interpolatedStringHandler.AppendFormatted<SeString>(((IGameObject) Core.Me).Name);
      interpolatedStringHandler.AppendLiteral(",");
      interpolatedStringHandler.AppendFormatted<uint>(((IGameObject) Core.Me).DataId);
      interpolatedStringHandler.AppendLiteral(",");
      interpolatedStringHandler.AppendFormatted<Vector3>(((IGameObject) Core.Me).Position);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
      interpolatedStringHandler.AppendLiteral("血量百分比：");
      interpolatedStringHandler.AppendFormatted<float>(GameObjectExtension.CurrentHpPercent((ICharacter) Core.Me));
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
      interpolatedStringHandler.AppendLiteral("盾值百分比：");
      interpolatedStringHandler.AppendFormatted<float>((float) ((ICharacter) Core.Me).ShieldPercentage / 100f);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
      interpolatedStringHandler.AppendLiteral("血量百分比：");
      interpolatedStringHandler.AppendFormatted<bool>((double) GameObjectExtension.CurrentHpPercent((ICharacter) Core.Me) + (double) ((ICharacter) Core.Me).ShieldPercentage / 100.0 <= 1.0);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
      interpolatedStringHandler.AppendLiteral("是否移动：");
      interpolatedStringHandler.AppendFormatted<bool>(MoveHelper.IsMoving());
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
      interpolatedStringHandler.AppendLiteral("小队人数：");
      interpolatedStringHandler.AppendFormatted<int>(PartyHelper.CastableParty.Count);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
      interpolatedStringHandler.AppendLiteral("25米内敌方人数：");
      interpolatedStringHandler.AppendFormatted<int>(TargetHelper.GetNearbyEnemyCount((IBattleChara) Core.Me, 25, 25));
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
      interpolatedStringHandler.AppendLiteral("20米内小队人数：");
      interpolatedStringHandler.AppendFormatted<int>(PartyHelper.CastableAlliesWithin20.Count);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
      interpolatedStringHandler.AppendLiteral("目标5米内人数：");
      interpolatedStringHandler.AppendFormatted<int>(TargetHelper.GetNearbyEnemyCount(GameObjectExtension.GetCurrTarget((IBattleChara) Core.Me), 25, 5));
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
      interpolatedStringHandler.AppendLiteral("LB槽当前数值：");
      interpolatedStringHandler.AppendFormatted<ushort>(LocalPlayerExtension.LimitBreakCurrentValue(Core.Me));
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
      interpolatedStringHandler.AppendLiteral("上个技能：");
      interpolatedStringHandler.AppendFormatted<uint>(Core.Resolve<MemApiSpellCastSuccess>().LastSpell);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
      interpolatedStringHandler.AppendLiteral("上个GCD：");
      interpolatedStringHandler.AppendFormatted<uint>(Core.Resolve<MemApiSpellCastSuccess>().LastGcd);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
      interpolatedStringHandler.AppendLiteral("上个能力技：");
      interpolatedStringHandler.AppendFormatted<uint>(Core.Resolve<MemApiSpellCastSuccess>().LastAbility);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
      interpolatedStringHandler.AppendLiteral("上个连击技能：");
      interpolatedStringHandler.AppendFormatted<uint>(Core.Resolve<MemApiSpell>().GetLastComboSpellId());
      interpolatedStringHandler.AppendLiteral(")");
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
    }





    public void Dispose()
    {
        // TODO release managed resources here
    }

}