using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons;
using ImGuiNET;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.GNB.Settings;
using Nagomi.GNB.utils;
using Nagomi.utils.Helper;


namespace Nagomi.GNB;


//群盾消化的新方法


public class 退避对位t: IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(7537, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
        {
        SpellHelper.DrawSpellInfo(GNBSpells.退避.GetSpell(), size, isActive);
    }

    public int Check()
    {
       return 0;
      }

    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.退避).IsUnlockWithCDCheck())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.退避), SpellTargetType.Pm2));
            
        }
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }
    }
}


public class 支援减对位T : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心), out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心), PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心).IsUnlockWithCDCheck())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心), SpellTargetType.Pm2));
            
        }
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }
        
    }
}
public class hot对位T : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(GNBSpells.极光, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(GNBSpells.极光, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.极光).IsUnlockWithCDCheck())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.极光), SpellTargetType.Pm2));
            
        }
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }
        
    }
}
public class 支援减最低血量 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心), out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心), PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心).IsUnlockWithCDCheck())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.石之心), Helper.获取血量最低成员));
            
        }
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }
    }
}

public class hot最低血量 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(16151, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(16151, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && GNBSpells.极光.IsUnlockWithCDCheck())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(GNBSpells.极光, Helper.获取血量最低成员));
            
        }
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }
    }
}


