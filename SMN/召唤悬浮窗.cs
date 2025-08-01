using AEAssist;
using AEAssist.API.MemoryApi;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using AEAssist.Verify;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Party;
using System.Runtime.CompilerServices;
using System.Text;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons.GameFunctions;
using ECommons.ImGuiMethods;
using Nagomi.Shared;

namespace Nagomi.SMN;

public class 召唤悬浮窗
{
   public static void DrawDev(JobViewWindow jobViewWindow)
    {
        ImGui.TextUnformatted($"召唤兽在场的时间: {Core.Resolve<JobApi_Summoner>().SummonTimerRemaining}");
        ImGui.TextUnformatted($"以太超流层数: {Core.Resolve<JobApi_Summoner>().AetherflowStacks}");
        ImGui.TextUnformatted($"召唤兽: {Core.Resolve<JobApi_Summoner>().ActivePetType}");
        ImGui.TextUnformatted($"召唤技能层数(修正): {Core.Resolve<JobApi_Summoner>().AttunementAdjust}");
        ImGui.TextUnformatted($"三神宝石的持续时间: {Core.Resolve<JobApi_Summoner>().AttunmentTimerRemaining}");
        ImGui.TextUnformatted($"有以太超流: {Core.Resolve<JobApi_Summoner>().HasAetherflowStacks}");
        ImGui.TextUnformatted($"有宝石兽: {Core.Resolve<JobApi_Summoner>().HasPet}");
        
        // 使用共享模块
        共享悬浮窗模块.DrawCountdownInfo();
        共享悬浮窗模块.DrawCombatInfo();
        共享悬浮窗模块.DrawLBInfo();
    }

    public static void 通用(JobViewWindow jobViewWindow)
    {
        // 使用共享模块
        共享悬浮窗模块.DrawGetLinksButtons();
        共享悬浮窗模块.DrawHiddenFeatures("SMN");
    }
}