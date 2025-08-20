using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using AEAssist.Extension;
using System.Runtime.CompilerServices;
using AEAssist.CombatRoutine;
using AEAssist.Define;
using Nagomi.Shared;

namespace Nagomi.GNB;

public class 绝枪悬浮窗
{
   public static void DrawDev(JobViewWindow jobViewWindow)
    {
        ImGui.TextUnformatted($"子弹: {Core.Resolve<JobApi_GunBreaker>().Ammo}");
        ImGui.TextUnformatted($"子弹连击状态: {Core.Resolve<JobApi_GunBreaker>().AmmoComboStep}");
        ImGui.TextUnformatted($"倾泄qt保留子弹数: {GNBSettings.Instance.保留子弹数}");
        ImGui.TextUnformatted($"当前额外技能距离: {GNBSettings.Instance.额外技能距离}");
      
        // 使用共享模块
        共享悬浮窗模块.DrawCountdownInfo();
        共享悬浮窗模块.DrawCombatInfo();
        共享悬浮窗模块.DrawLBInfo();
        
        if (ImGui.CollapsingHeader("ACR逻辑"))
        {
            ImGui.Text("本acr建议使用2.5gcd " );
            ImGui.Text("未开启无情不延后qt时，100级填充期无情仅在可双爆发击时使用" );
            ImGui.Text("二弹120在爆发击卸子弹后进无情，零弹120在残暴弹使用后进无情" );
            ImGui.Text("gcd优先级：低等级音速破，子弹连，倍攻，音速破，狮心连，命运之环，爆发击，基础连击" );
            
            if (ImGui.CollapsingHeader("子弹连逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("停手检查：如果开启停手开关，直接返回 拒绝");
                ImGui.BulletText("敌人无敌检查：如果目标有无敌buff，拒绝使用");
                ImGui.BulletText("距离检查：根据额外技能距离设置检查攻击距离");
                ImGui.BulletText("技能可用性检查：如果烈牙技能不可用，拒绝使用");
                ImGui.BulletText("爆发开关检查：如果爆发QT关闭，拒绝使用");
                ImGui.BulletText("倾泻爆发检查：如果开启倾泻爆发且子弹数量足够，给予 使用");
                ImGui.BulletText("仅爆发击限制：如果设置仅使用爆发击卸除子弹，拒绝使用");
                ImGui.BulletText("子弹连开关检查：如果子弹连QT关闭，拒绝使用");
                ImGui.BulletText("倍攻优先级：如果倍攻可用且开启倍攻，优先使用倍攻");
                ImGui.BulletText("狮心连检查：如果正在狮心连连击中，拒绝使用");
                ImGui.BulletText("连击继续：如果正在烈牙连击中，继续使用");
                ImGui.BulletText("AOE检查：72级后敌人数量大于3且命运之环可用时，拒绝使用");
                ImGui.BulletText("无情冷却检查：如果无情即将冷却完成，拒绝使用");
                ImGui.BulletText("零弹dot优先级：如果开启零弹且音速破可用，优先使用音速破");
                ImGui.BulletText("药物状态检查：如果吃药还没放无情，拒绝使用");
            }
            if (ImGui.CollapsingHeader("倍攻逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("倍攻开关检查：如果倍攻QT关闭，拒绝使用");
                ImGui.BulletText("爆发开关检查：如果爆发QT关闭，拒绝使用");
                ImGui.BulletText("技能可用性检查：如果倍攻技能不可用，拒绝使用");
                ImGui.BulletText("子弹检查：如果子弹数量少于1，拒绝使用");
                ImGui.BulletText("距离检查：如果距离大于5码，拒绝使用");
                ImGui.BulletText("无情冷却检查：如果无情在2GCD内冷却且未开启倾泻爆发，拒绝使用");
                ImGui.BulletText("零弹dot优先级：如果开启零弹且音速破可用，优先使用音速破");
                ImGui.BulletText("倾泻爆发检查：如果开启倾泻爆发且子弹数量足够，给予 使用");
                ImGui.BulletText("仅爆发击限制：如果设置仅使用爆发击卸除子弹，拒绝使用");
                ImGui.BulletText("无情状态检查：如果没有无情buff，拒绝使用");
                
            }
            if (ImGui.CollapsingHeader("音速破逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("敌人无敌检查：如果目标有无敌buff，拒绝使用");
                ImGui.BulletText("停手检查：如果开启停手开关，直接返回 拒绝");
                ImGui.BulletText("倾泻爆发检查：如果开启倾泻爆发，给予 使用");
                ImGui.BulletText("爆发开关检查：如果爆发QT关闭，拒绝使用");
                ImGui.BulletText("dot开关检查：如果dotQT关闭，拒绝使用");
                ImGui.BulletText("音速破开关检查：如果音速破QT关闭，拒绝使用");
                ImGui.BulletText("距离检查：根据额外技能距离设置检查攻击距离");
                ImGui.BulletText("技能可用性检查：如果音速破技能不可用，拒绝使用");
                ImGui.BulletText("二弹连击优先级：如果开启二弹且烈牙可用，优先使用烈牙");
                ImGui.BulletText("70级80级为最高优先级gcd");
                 
            }
            if (ImGui.CollapsingHeader("狮心连逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("爆发开关检查：如果爆发QT关闭，拒绝使用");
                ImGui.BulletText("狮心连开关检查：如果狮心连QT关闭，拒绝使用");
                ImGui.BulletText("敌人无敌检查：如果目标有无敌buff，拒绝使用");
                ImGui.BulletText("无情状态检查：如果没有无情buff且崛起之心未变化，拒绝使用");
                ImGui.BulletText("技能可用性检查：如果崛起之心技能不可用，拒绝使用");
                ImGui.BulletText("连击继续：如果正在支配之心或终结之心连击中，继续使用");
                ImGui.BulletText("子弹连优先级：如果开启子弹连且烈牙可用且子弹不为0，优先使用子弹连");
                ImGui.BulletText("距离检查：根据额外技能距离设置检查攻击距离");
                ImGui.BulletText("子弹连检查：如果正在烈牙连击中，拒绝使用");
                ImGui.BulletText("buff限制检查：吃药还没放无情不打");
            }
            if (ImGui.CollapsingHeader("命运之环逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("停手检查：如果开启停手开关，直接返回 拒绝");
                ImGui.BulletText("技能可用性检查：如果命运之环技能不可用或无法施放，拒绝使用");
                ImGui.BulletText("QT开关检查：如果AOEQT关闭，拒绝使用");
                ImGui.BulletText("QT开关检查：如果命运之环QT关闭，拒绝使用");
                ImGui.BulletText("目标数量检查：获取5码范围内的敌人数量，如果敌人数量少于2个，拒绝使用");
                ImGui.BulletText("爆发条件检查：如果开启倾泻爆发且子弹数量足够（超过保留数量+1），给予 使用");
                ImGui.BulletText("无情/药物状态下的子弹管理：");
                ImGui.Indent();
                ImGui.Text("当没有无情buff且没有药物buff时：");
                ImGui.BulletText("88级以下：子弹少于2个且不是残暴弹或恶魔切连击时拒绝使用");
                ImGui.BulletText("88级及以上：子弹少于3个且不是残暴弹或恶魔切连击时拒绝使用");
                ImGui.Unindent();
                ImGui.BulletText("QT限制检查：如果设置仅使用爆发击卸除子弹，拒绝使用命运之环");
                ImGui.BulletText("buff限制检查：吃药还没放无情不打");
            }
            
            if (ImGui.CollapsingHeader("爆发击逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("子弹检查：如果子弹数量为0，拒绝使用");
                ImGui.BulletText("敌人无敌检查：如果目标有无敌buff，拒绝使用");
                ImGui.BulletText("停手检查：如果开启停手开关，直接返回 拒绝");
                ImGui.BulletText("爆发击开关检查：如果爆发击QT关闭，拒绝使用");
                ImGui.BulletText("距离检查：根据额外技能距离设置检查攻击距离");
                ImGui.BulletText("技能可用性检查：如果爆发击技能不可用，拒绝使用");
                ImGui.BulletText("AOE检查：敌人数量大于2且命运之环可用时，拒绝使用");
                ImGui.BulletText("倾泻爆发检查：如果开启倾泻爆发且子弹数量足够，给予 使用");
                ImGui.BulletText("仅爆发击限制：如果设置仅使用爆发击卸除子弹且子弹数量足够，给予 使用");
                ImGui.BulletText("连击优先级：如果子弹连或狮心连可用，优先使用连击技能");
                ImGui.BulletText("药物状态检查：如果吃药还没放无情，拒绝使用");
                ImGui.BulletText("无情+药物状态：如果同时有无情和药物buff，给予 使用");
                ImGui.BulletText("药物状态：如果无情消失但药物还在，给予 使用");
                ImGui.Text("88级以下逻辑：");
                ImGui.Indent();
                ImGui.BulletText("子弹溢出：子弹为2且是残暴弹或恶魔切连击时，给予 使用");
                ImGui.BulletText("无情状态：如果有无情buff，给予 使用");
                ImGui.Unindent();
                ImGui.Text("88级及以上逻辑：");
                ImGui.Indent();
                ImGui.BulletText("子弹溢出：子弹为3且是残暴弹或恶魔切连击时，给予 使用");
                ImGui.BulletText("无情填充：如果22秒内用过无情，在无情期间填充使用");
                ImGui.BulletText("120后填充：子弹为3且是残暴弹且无情不在2GCD内冷却，120后填充期使用");
                ImGui.BulletText("二弹120卸子弹：开启二弹且子弹不为2且无情在2GCD内冷却且血壤在8GCD内冷却时使用");
                ImGui.BulletText("二弹120提前卸子弹：开启二弹且子弹为2且无情在1GCD内冷却且血壤在8GCD内冷却且上一个连击是残暴弹或恶魔切时使用");
                ImGui.BulletText("零弹120卸子弹：开启零弹且子弹不为0且无情在4GCD内冷却且血壤在8GCD内冷却时使用");
                ImGui.BulletText("零弹120填充：开启零弹且血壤在7GCD内冷却时使用");
                ImGui.Unindent();
            }
            
            
            
            
             
           
            
            if (ImGui.CollapsingHeader("无情逻辑"))
            {
                ImGui.Text("检查流程（按优先级从高到低）：");
                ImGui.BulletText("停手检查：如果开启停手开关，直接返回 拒绝");
                ImGui.BulletText("技能可用性检查：如果无情技能不可用，拒绝使用");
                ImGui.BulletText("倾泻爆发检查：如果开启倾泻爆发，给予 使用");
                ImGui.BulletText("爆发开关检查：如果爆发QT关闭，拒绝使用");
                ImGui.BulletText("无情开关检查：如果无情QT关闭，拒绝使用");
                ImGui.BulletText("无情不延后检查：如果开启无情不延后，给予 使用");
                ImGui.Text("100级特殊逻辑：");
                ImGui.Indent();
                ImGui.BulletText("无血壤填充：子弹为3且血壤不在8GCD内冷却时使用");
                ImGui.BulletText("二弹120：子弹为2且开启二弹且血壤在4GCD内冷却且上一个GCD是爆发击时使用");
                ImGui.BulletText("零弹120：子弹为0且开启零弹且上一个连击是残暴弹时使用");
                ImGui.Unindent();
                ImGui.Text("非100级逻辑：直接给予 使用");
            }
            
           
        }
    }

    public static void 通用(JobViewWindow jobViewWindow)
    {
        ImGui.Text("测试中");
        ImGui.Text("MT or ST");
        ImGui.SameLine();
        switch (GNBSettings.Instance.ST)
        {
            case true:
                ImGui.TextColored(new System.Numerics.Vector4(1.0f, 0.0f, 0.0f, 1.0f), "ST");
                break;
            case false:
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 1.0f, 0.0f, 1.0f), "MT");
                break;
        }
        ImGui.SameLine();
        if (ImGui.Button(" ST "))
        {
            GNBSettings.Instance.ST = true;
            GNBSettings.Instance.Save();
        }
        ImGui.SameLine();
        if (ImGui.Button(" MT "))
        {
            GNBSettings.Instance.ST = false;
            GNBSettings.Instance.Save();
        }
        ImGui.Text("当前起手：");
        ImGui.SameLine();
        switch (GNBSettings.Instance.opener)
        {
            case 0:
                ImGui.TextColored(new System.Numerics.Vector4(0.5f, 0.5f, 0.5f, 1.0f), "无起手"); // 蓝色
                break;
            // 蓝色
            case 1:
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 0.8f, 0.1f, 1.0f), "零弹120起手"); // 蓝色
                break;
            case 2:
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 0.8f, 0.1f, 1.0f), "二弹120起手"); // 蓝色
                break;
            case 3:
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 0.3f, 0.8f, 1.0f), "神兵起手"); // 蓝色
                break;
            case 4:
                ImGui.TextColored(new System.Numerics.Vector4(0.2f, 0.5f, 0.8f, 1.0f), "2g无情起手"); // 蓝色
                break;
            case 5:
                ImGui.TextColored(new System.Numerics.Vector4(0.0f, 0.3f, 0.8f, 1.0f), "绝亚起手"); // 蓝色
                break;
        }

        if (ImGui.Button("无起手"))
        {
            GNBSettings.Instance.opener = 0;
            GNBSettings.Instance.Save();
        }
        
         
        if (ImGui.Button("零弹起手"))
        {
            GNBSettings.Instance.opener = 1;
            GNBSettings.Instance.Save();
        }
        ImGui.SameLine();
        if (ImGui.Button("二弹起手"))
        {
            GNBSettings.Instance.opener = 2;
            GNBSettings.Instance.Save();
        }
        ImGui.SameLine();
        if (ImGui.Button("2g起手"))
        {
            GNBSettings.Instance.opener = 4;
            GNBSettings.Instance.Save();
        }
        
        if (ImGui.Button("神兵起手"))
        {
            GNBSettings.Instance.opener = 3;
            GNBSettings.Instance.Save();
        }
        ImGui.SameLine();
        if (ImGui.Button("绝亚起手"))
        {
            GNBSettings.Instance.opener = 5;
            GNBSettings.Instance.Save();
        }
        
       
        ImGui.Text("额外技能距离:");
         ImGui.SameLine();
         ImGui.SliderFloat("", ref GNBSettings.Instance.额外技能距离, 0, 3, "%.2f");
        if (ImGui.Button("0"))
        {
            GNBSettings.Instance.保留子弹数 = 0;
        }
        ImGui.SameLine();
        if (ImGui.Button("1"))
        {
            GNBSettings.Instance.保留子弹数 = 1;
        }
        ImGui.SameLine();
        if (ImGui.Button("2"))
        {
            GNBSettings.Instance.保留子弹数 = 2;
        }
        ImGui.SameLine();
        ImGui.Text("当前保留子弹数：");
        ImGui.SameLine();
        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0, 1, 0, 1));
        ImGui.Text($"{GNBSettings.Instance.保留子弹数}");
        ImGui.PopStyleColor();
            
        // 使用共享模块
        共享悬浮窗模块.DrawGetLinksButtons();
        共享悬浮窗模块.DrawHiddenFeatures("GNB");
    }
}