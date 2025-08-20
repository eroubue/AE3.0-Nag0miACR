using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.GUI;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.Helper;
using Millusion.ACR.Astrologian.Setting;
using Millusion.ACR.Astrologian.SlotResolvers.Ability;
using Millusion.CharacterRefined;
using Millusion.Helper;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.UI.Tab;

public static class DebugUI
{
    private static int _spellId = 7439;
    private static System.Numerics.Vector3 _pos = new();

    public static void Draw(JobViewWindow jobViewWindow)
    {
        if (ImGui.CollapsingHeader("小队列表"))
            foreach (var ally in PartyHelper.CastableAlliesWithin30)
                ImGui.Text("姓名:" + ally.Name +
                           "  需要治疗威力(阈值):" + ally.NeedHealWithASTPower() +
                           "  需要治疗威力:" + ally.NeedHealWithPower() +
                           "  需要最大治疗威力:" + ally.NeedMaxHealWithPower() +
                           "  当前生命值:" + ally.CurrentHp +
                           "  最大生命值:" + ally.MaxHp +
                           "  生命百分比:" + ally.CurrentHpPercent() +
                           "  坦克:" + ally.IsTank() +
                           "  IsHealer:" + ally.IsHealer() +
                           "  IsDps:" + ally.IsDps() +
                           "  Index:" + AST_Settings.Instance.BalanceCardTargetList.IndexOf(ally.CurrentJob())
                );

        if (ImGui.CollapsingHeader("卡牌目标"))
        {
            ImGui.Text("太阳神:" + AST_Ability_Play1.GetBalanceTarget()?.Name);
            ImGui.Text("战争神:" + AST_Ability_Play1.GetSpearTarget()?.Name);
            ImGui.Text("放浪神:" + AST_Ability_Play2.GetArrowTarget()?.Name);
            ImGui.Text("世界树:" + AST_Ability_Play2.GetBoleTarget()?.Name);
            ImGui.Text("建筑神:" + AST_Ability_Play3.GetSpireTarget()?.Name);
            ImGui.Text("河流神:" + AST_Ability_Play3.GetEwerTarget()?.Name);
        }

       

        if (ImGui.CollapsingHeader("AE-BattleData"))
        {
            if (AI.Instance.BattleData.NextSlot != null)
                ImGui.Text("NextSlot:" + AI.Instance.BattleData.NextSlot.ToStringSameline());
            ImGui.Text("HighPrioritySlots_GCD:" + AI.Instance.BattleData.HighPrioritySlots_GCD.Count);
            ImGui.Text("HighPrioritySlots_OffGCD:" + AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count);
        }

        if (ImGui.CollapsingHeader("AST-BattleData"))
        {
            var t = Core.Me.GetCurrTarget();
            ImGui.Text("当前目标:" + (t?.Name ?? "无") + "  TTK:" + (t != null && TTKHelper.IsTargetTTK(t, 15000, false)));
            ImGui.Text("是否在Boss战:" + AST_BattleData.Instance.IsBossBattle);
            ImGui.Text("战斗剩余时间预测:" + AST_BattleData.Instance.BattleRemainingTime);
            ImGui.Text("每100威力平均治疗量:" + CharacterStatus.AvgHeal);
            ImGui.Text(
                $"最低Hp队友:{AST_BattleData.Instance.PartyMemberWithLowestHp?.Name}  HP:{AST_BattleData.Instance.PartyMemberWithLowestHp?.CurrentHpPercent()}");
            ImGui.Text(
                $"最低血量坦克:{AST_BattleData.Instance.PartyMemberWithLowestHpTank?.Name}  HP:{AST_BattleData.Instance.PartyMemberWithLowestHpTank?.CurrentHpPercent()}");
            ImGui.Text(
                $"最低血量非坦克:{AST_BattleData.Instance.PartyMemberWithLowestHpNotTank?.Name}  HP:{AST_BattleData.Instance.PartyMemberWithLowestHpNotTank?.CurrentHpPercent()}");
            ImGui.Text(
                $"队内另一个治疗{AST_BattleData.Instance.AnotherHealer?.Name}  HP:{AST_BattleData.Instance.AnotherHealer?.CurrentHpPercent()}  Job:{AST_BattleData.Instance.AnotherHealer?.ClassJob}");
        }

        if (ImGui.CollapsingHeader("技能状态"))
        {
            ImGui.Text(SpellsDefine.Horoscope.GetSpell().Name + "  IsAllReady:" +
                       MsSpellHelper.IsUnlockWithCD(SpellsDefine.Horoscope));
            ImGui.Text(SpellsDefine.HoroscopeHelios.GetSpell().Name + "  IsAllReady:" +
                       MsSpellHelper.IsUnlockWithCD(SpellsDefine.HoroscopeHelios));
            ImGui.Text(SpellsDefine.MinorArcana.GetSpell().Name + "ID:" + SpellsDefine.MinorArcana + "  IsAllReady:" +
                       MsSpellHelper.IsUnlockWithCD(SpellsDefine.MinorArcana));
            ImGui.Text(SpellsDefine.MinorArcana.GetSpell().Name + "ID:" + SpellsDefine.MinorArcana.AdjustActionID() +
                       "  IsAllReady:" + MsSpellHelper.IsUnlockWithCD(SpellsDefine.MinorArcana.AdjustActionID()));
            ImGui.Text(SpellsDefine.LadyofCrowns.GetSpell().Name + "ID:" + SpellsDefine.LadyofCrowns.AdjustActionID() +
                       "  IsAllReady:" + MsSpellHelper.IsUnlockWithCD(SpellsDefine.LadyofCrowns.AdjustActionID()));
            ImGui.Text(SpellsDefine.LordofCrowns.GetSpell().Name + "ID:" + SpellsDefine.LordofCrowns.AdjustActionID() +
                       "  IsAllReady:" + MsSpellHelper.IsUnlockWithCD(SpellsDefine.LordofCrowns.AdjustActionID()));
        }

        if (ImGui.CollapsingHeader("技能释放时间集合"))
            foreach (var (key, value) in Core.Resolve<MemApiSpellCastSuccess>().SpellCastTime)
                ImGui.Text("技能:" + key.GetSpell().Name + "  ID:" + key + "  Ready:" + key.GetSpell().IsAllReady() +
                           "  ActionState:" + Core.Resolve<MemApiSpell>().GetActionState(key) + "  InRangeOrLoS:" +
                           Core.Resolve<MemApiSpell>().GetActionInRangeOrLoS(key) + "  已经过时间:" +
                           (TimeHelper.Now() - value));

        if (ImGui.CollapsingHeader("周围对象"))
        {
            ImGui.BeginChild("周围对象Child", new Vector2(0, 200), true);

            foreach (var unit in TargetMgr.Instance.Units)
            {
                ImGui.Text(
                    $"Unit:{unit.Value} Name:{unit.Value.Name} EntityId:{unit.Value.EntityId} GameObjectId:{unit.Value.GameObjectId} OwnerId:{unit.Value.OwnerId} " +
                    $"Distance:{unit.Value.Distance(Core.Me, DistanceMode.IgnoreHeight)}");
            }

            ImGui.EndChild();
        }

        ImGui.Separator();
        var target = Core.Me.GetCurrTarget();
        if (target != null)
        {
            ImGui.Text("目标:" + target.Name + "  HitboxRadius:" + target.HitboxRadius + "  EntityId:" + target.EntityId +
                       "  OwnerId:" + target.OwnerId + "  Distance:" +
                       target.Distance(Core.Me, DistanceMode.IgnoreHeight));
        }

        ImGui.Separator();

        ImGui.Text("副本人数:" + MsAcrHelper.DutyMembersNumber());
        ImGui.Text("鼠标位置:" + Core.Resolve<MemApiMove>().MousePos());
        ImGui.Text("自身位置:" + Core.Me?.Position);
        ImGui.Text("目标位置:" + Core.Me.GetCurrTarget()?.Position);
        if (ImGui.Button("打印自身坐标##12345"))
        {
            LogHelper.Print(Core.Me?.Position.ToString());
        }

        ImGui.SameLine();
        if (ImGui.Button("打印目标坐标##12345"))
        {
            LogHelper.Print(Core.Me.GetCurrTarget()?.Position.ToString());
        }

        ImGui.Text("Boss场地中心位置:" + MsAcrHelper.MapCenter(Core.Me.Position, true));
        var zoneInfo = MemApiZoneInfo.GetZoneInfoFromTerritoryTypeId(Svc.ClientState.TerritoryType);
        ImGui.Text("MapBaseName:" + zoneInfo.MapBaseName + "  TerritoryTypeId:" + zoneInfo.TerritoryTypeId +
                   "  DutyId:" + zoneInfo.DutyId + "  MapId:" + Svc.ClientState.MapId);
        var maps = MemApiZoneInfo.GetMapInfoFromTerritoryTypeId(Svc.ClientState.TerritoryType);
        foreach (var map in maps)
        {
            var mapCoordinates = map.GetMapCoordinates(new Vector2(0.5f, 0.5f) * 2048f);
            var mapCenter = new Vector3(mapCoordinates.X, Svc.ClientState.LocalPlayer.Position.Y, mapCoordinates.Y);
            ImGui.Text("MapId:" + map.MapId +
                       "  PlaceNameSub:" + map.PlaceNameSub +
                       "  MapCoordinates:" + mapCenter);
        }


        if (ImGui.CollapsingHeader("地面技能测试"))
        {
            ImGui.InputInt("技能id", ref _spellId);
            ImGui.InputFloat3("坐标", ref _pos);
            if (ImGui.Button("使用")) Core.Resolve<MemApiSpell>().Cast((uint)_spellId, _pos);
        }
 

        ImGui.Separator();
        ImGui.Text("CurrentContentFinderConditionId:" + Data.CurrentContentFinderConditionId);
        ImGui.Text("IsInHighEndDuty:" + Data.IsInHighEndDuty);
        ImGui.Text("IsPvP:" + Data.IsPvP);
        ImGui.Text("移动中: " + Core.Resolve<MemApiMove>().IsMoving());
        ImGui.Text("LastComboAction: " + Core.Resolve<MemApiSpell>().GetLastComboSpellId());
        ImGui.Text("ComboTime: " + Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds);

        ImGui.Separator();

        ImGui.Text("GCDDuration: " + GCDHelper.GetGCDDuration());
        ImGui.Text("ElapsedGCD: " + GCDHelper.GetElapsedGCD());
        ImGui.Text("GCDCooldown: " + GCDHelper.GetGCDCooldown());
        ImGui.Text("CanUseGCD:" + GCDHelper.CanUseGCD());
        ImGui.Text("CanUseOffGcd:" + GCDHelper.CanUseOffGcd());
        ImGui.Text("AnimationLock:" + Core.Resolve<MemApiSpell>().AnimationLock * 1000f);
        ImGui.Text("IsCasting:" + Core.Me.IsCasting);
        ImGui.Text("TotalCastTime:" + Core.Me.TotalCastTime);
        ImGui.Text("CurrentCastTime:" + Core.Me.CurrentCastTime);

        ImGui.Separator();
        // if (ImGui.Button("面向目标"))
        // {
        //     if (Core.Me.GetCurrTarget() != null)
        //     {
        //         unsafe
        //         {
        //             LogHelper.Print("面向目标");
        //             var targetPos = Core.Me.GetCurrTarget().Position;
        //             ActionManager.Instance()->AutoFaceTargetPosition(&targetPos, Core.Me.GetCurrTarget().EntityId);
        //         }
        //     }
        //
        // }
        // if (ImGui.Button("打印小队CID"))
        // {
        //     foreach (var v in Svc.Party) LogHelper.Print(v.ContentId.ToString());
        //
        //     LogHelper.Print(Svc.ClientState.LocalContentId.ToString());
        // }
        //
        // if (!ImGui.Button("打印小队Buff")) return;
        //
        // foreach (var party in PartyHelper.CastableParty)
        // {
        //     LogHelper.Print(party.Name.ToString());
        //     foreach (var stat in party.StatusList) LogHelper.Print(stat.ToString() ?? string.Empty);
        // }
    }
}