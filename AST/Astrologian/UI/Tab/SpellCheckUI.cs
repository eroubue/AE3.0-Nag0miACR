using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.MemoryApi;
using ECommons.GameFunctions;
using FFXIVClientStructs.FFXIV.Client.Game;
using ImGuiNET;
using Millusion.ACR.Astrologian.SlotResolvers;
using Millusion.Helper;

namespace Millusion.ACR.Astrologian.UI.Tab;

public static class SpellCheckUI
{
    public static void Draw(JobViewWindow jobViewWindow)
    {
        ImGui.Text("下一个技能: " + AST_SlotResolvers.SlotResolvers.FirstOrDefault(r => r.CheckCode >= 0)?.Name);
        ImGui.Text("下一个GCD: " + AST_SlotResolvers.SlotResolvers
            .FirstOrDefault(r => r.CheckCode >= 0 && r.Mode == SlotMode.Gcd)?.Name);
        ImGui.Text("下一个能力技: " + AST_SlotResolvers.SlotResolvers
            .FirstOrDefault(r => r.CheckCode >= 0 && r.Mode == SlotMode.OffGcd)?.Name);
        ImGui.Separator();
        if (ImGui.CollapsingHeader("技能列表"))
        {
            ImGui.BeginTable("SpellCheckTable", 8,
                ImGuiTableFlags.Borders | ImGuiTableFlags.Resizable | ImGuiTableFlags.RowBg);
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.Text("ID");
            ImGui.TableNextColumn();
            ImGui.Text("Name");
            ImGui.TableNextColumn();
            ImGui.Text("Mode");
            ImGui.TableNextColumn();
            ImGui.Text("SpellReady");
            ImGui.TableNextColumn();
            ImGui.Text("LOS");
            ImGui.TableNextColumn();
            ImGui.Text("CheckCode");
            ImGui.TableNextColumn();
            ImGui.Text("Target");
            ImGui.TableNextColumn();
            ImGui.Text("Test");
            // ImGui.TableNextColumn();
            // ImGui.Text("ActionState");
            foreach (var action in AST_SlotResolvers.SlotResolvers)
            {
                unsafe
                {
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    ImGui.Text(action.SpellId.ToString());
                    ImGui.TableNextColumn();
                    ImGui.Text(action.Name);
                    ImGui.TableNextColumn();
                    ImGui.Text(action.Mode.ToString());
                    ImGui.TableNextColumn();
                    ImGui.Text(action.SpellReady.ToString());
                    ImGui.TableNextColumn();
                    if (action.SpellTarget != null)
                    {
                        ImGui.Text(ActionManager.GetActionInRangeOrLoS(action.SpellId, ObjectFunctions.Struct(Core.Me),
                                       action.SpellTarget.GameObject()) + " - " +
                                   ActionManager.GetActionInRangeOrLoS(action.SpellId, action.SpellTarget.GameObject(),
                                       ObjectFunctions.Struct(Core.Me)
                                   ));
                    }

                    ImGui.TableNextColumn();
                    ImGui.Text(action.CheckCode.ToString());
                    ImGui.TableNextColumn();
                    ImGui.Text(action.SpellTarget?.Name.ToString() ?? "null");
                    ImGui.TableNextColumn();
                    if (action.SpellTarget != null)
                    {
                        ImGui.Text(ActionManager.CanUseActionOnTarget(action.SpellId, action.SpellTarget.GameObject())
                            .ToString());
                    }
                    // ImGui.TableNextColumn();
                    // ImGui.Text(MsSpellHelper.GetActionState(action.SpellId, action.SpellTarget) + " - " +
                    //            MsSpellHelper.GetActionState(action.SpellId, action.SpellTarget, false) + " - " +
                    //            MsSpellHelper.GetActionState(action.SpellId, action.SpellTarget, true, false) + " - " +
                    //            MsSpellHelper.GetActionState(action.SpellId, action.SpellTarget, false, false)
                    // );
                }
            }

            ImGui.EndTable();
        }
    }
}