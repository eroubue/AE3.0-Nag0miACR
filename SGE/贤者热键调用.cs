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
using Nagomi.SGE.Settings;
using Nagomi.SGE.utils;
using Nagomi.utils.Helper;


namespace Nagomi.SGE;


//群盾消化的新方法

public class 群盾消化 : IHotkeyResolver
{
   

    public void Draw(System.Numerics.Vector2 size)
    {
        System.Numerics.Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap idalamudTextureWrap;

        if (Core.Resolve<MemApiIcon>().GetActionTexture(24301U, out idalamudTextureWrap, true))
        {
            if (idalamudTextureWrap != null)
            {
                ImGui.Image(idalamudTextureWrap.ImGuiHandle, size1);
            }
            else
            {
                // 处理 idalamudTextureWrap 为 null 的情况
                // 例如：显示默认图标或错误消息
                Console.WriteLine("Failed to load texture for action ID: 29054");
            }
        }
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        
            SpellHelper.DrawSpellInfo(new Spell(24301U, (IBattleChara)Core.Me), size, isActive);
        
    }

    public int Check()
    {
        // 这里可以实现检查逻辑，目前返回0表示默认状态
        return 0;
    }

    public void Run()
    {
        // 这里实现触发时的逻辑
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        if (!Core.Resolve<JobApi_Sage>().Eukrasia)
            AI.Instance.BattleData.NextSlot.Add(SGESpells.Eukrasia.GetSpell());
        AI.Instance.BattleData.NextSlot.Add(SGESpells.EukrasianPrognosis.GetSpell());
        if (SGESpells.Pepsis.IsReady())
            AI.Instance.BattleData.NextSlot.Add(new Spell(SGESpells.Pepsis, Core.Me));
    }

}


public class 群盾 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(SGESpells.EukrasianPrognosis, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(SGESpells.EukrasianPrognosis, SpellTargetType.Self), size, isActive);

    public int Check() => 0;

    public void Run()
    {
        // 这里实现触发时的逻辑
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        if (!Core.Resolve<JobApi_Sage>().Eukrasia)
            AI.Instance.BattleData.NextSlot.Add(SGESpells.Eukrasia.GetSpell());
        AI.Instance.BattleData.NextSlot.Add(SGESpells.EukrasianPrognosis.GetSpell());
    }

}

public class 单盾T : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(24291u, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(24291u, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        if (!Core.Resolve<JobApi_Sage>().Eukrasia)
            AI.Instance.BattleData.NextSlot.Add(SGESpells.Eukrasia.GetSpell());
        AI.Instance.BattleData.NextSlot.Add(new Spell(24291u, PartyHelper.CastableTanks.FirstOrDefault()));
    }
}
public class 混合最低血量 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(24317u, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(24317u, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && SGESpells.混合.IsReady())
        {
            AI.Instance.BattleData.NextSlot = new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(SGESpells.混合, Helper.获取血量最低成员));
            
        }
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }
    }
}

public class 单盾最低血量 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(24291u, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(24291u, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(2607))), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        if (!Core.Resolve<JobApi_Sage>().Eukrasia)
            AI.Instance.BattleData.NextSlot.Add(SGESpells.Eukrasia.GetSpell());
        AI.Instance.BattleData.NextSlot.Add(new Spell(24291u, Helper.获取血量最低成员));
    }
}

public class 神翼T : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(24295u, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    public void DrawExternal(Vector2 size, bool isActive)
        => SpellHelper.DrawSpellInfo(new Spell(24295u, PartyHelper.CastableTanks.FirstOrDefault(agent => !agent.HasAura(SGEBuffs.复活)&&!agent.IsValid()&&agent!=null)), size, isActive);
    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        AI.Instance.BattleData.NextSlot.Add(new Spell(24295u, PartyHelper.CastableTanks.FirstOrDefault()));
    }
}
public class 即刻贤炮 : IHotkeyResolver
{
    public void Draw(System.Numerics.Vector2 size)
    {
        System.Numerics.Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap idalamudTextureWrap;

        if (Core.Resolve<MemApiIcon>().GetActionTexture(24318U, out idalamudTextureWrap, true))
        {
            if (idalamudTextureWrap != null)
            {
                ImGui.Image(idalamudTextureWrap.ImGuiHandle, size1);
            }
            else
            {
                // 处理 idalamudTextureWrap 为 null 的情况
                // 例如：显示默认图标或错误消息
                Console.WriteLine("Failed to load texture for action ID: 29054");
            }
        }
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(new Spell(24318U, (IBattleChara)Core.Me), size, isActive);
    }

    public int Check() => 0;

    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null && SGESpells.魂灵风息.IsReady()&& SGESpells.即刻咏唱.IsReady()&&SGESettings.Instance.即刻贤炮==1)
        {
            AI.Instance.BattleData.NextSlot = new Slot(1500);
            AI.Instance.BattleData.NextSlot.Add(new Spell(SGESpells.即刻咏唱, Core.Me));
            AI.Instance.BattleData.NextSlot.Add(new Spell(SGESpells.魂灵风息,  SpellTargetType.Target));
            
        }
        if (AI.Instance.BattleData.NextSlot == null && SGESpells.魂灵风息.IsReady()&&SGESettings.Instance.即刻贤炮==0)
        {
            AI.Instance.BattleData.NextSlot = new Slot(1500);
            AI.Instance.BattleData.NextSlot.Add(new Spell(SGESpells.魂灵风息,  SpellTargetType.Target));
        }
        
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }

               


    }
}


public class 营救最远 : IHotkeyResolver
{

    public int Check() => 0;
    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        var RescueTarget = PartyHelper.CastableAlliesWithin30
.Where(r => r.CurrentHp > 0 && !r.HasAura(2663) && !r.HasAura(1209) && !r.HasAura(1984) && !r.HasAura(160) && !r.HasAura(2345) && !r.HasAura(75) && !r.HasAura(712) && !r.HasAura(1096) && !r.HasAura(1303))
.OrderBy(r => r.Distance(PartyHelper.CastableAlliesWithin30.FirstOrDefault()))
.LastOrDefault();
        AI.Instance.BattleData.NextSlot.Add(new Spell(7571u, RescueTarget));
    }

    void IHotkeyResolver.Draw(Vector2 size)
    {
        Vector2 size3 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap textureWrap;
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(7571u, out textureWrap))
            return;
        ImGui.Image(textureWrap.ImGuiHandle, size3);
    }

    void IHotkeyResolver.DrawExternal(Vector2 size, bool isActive)
    {
        var RescueTarget = PartyHelper.CastableAlliesWithin30
    .Where(r => r.CurrentHp > 0 && !r.HasAura(2663) && !r.HasAura(1209) && !r.HasAura(1984) && !r.HasAura(160) && !r.HasAura(2345) && !r.HasAura(75) && !r.HasAura(712) && !r.HasAura(712) && !r.HasAura(1096) && !r.HasAura(1303))
    .OrderBy(r => r.Distance(PartyHelper.CastableAlliesWithin30.FirstOrDefault()))
    .LastOrDefault();

        SpellHelper.DrawSpellInfo(new Spell(7571u, RescueTarget), size, isActive);
    }
}
public class 即刻拉人 : IHotkeyResolver
{
    public void Draw(System.Numerics.Vector2 size)
    {
        System.Numerics.Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap idalamudTextureWrap;

        if (Core.Resolve<MemApiIcon>().GetActionTexture(24318U, out idalamudTextureWrap, true))
        {
            if (idalamudTextureWrap != null)
            {
                ImGui.Image(idalamudTextureWrap.ImGuiHandle, size1);
            }
            else
            {
                // 处理 idalamudTextureWrap 为 null 的情况
                // 例如：显示默认图标或错误消息
                Console.WriteLine("Failed to load texture for action ID: 29054");
            }
        }
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(new Spell(24318U, (IBattleChara)Core.Me), size, isActive);
    }

    public int Check() => 0;

    public void Run()
    {
        var skillTarget = PartyHelper.DeadAllies.FirstOrDefault(r => !r.HasAura(148u));
        //设定targetname
        // 检查skillTarget是否为null
        if (skillTarget != null && skillTarget.Name != null)
        {
            // 确保skillTarget.Name不为null后再访问TextValue属性
            SGESettings.Instance.targetName = skillTarget.Name.TextValue;
        }
      
        if (AI.Instance.BattleData.NextSlot == null && 24287u.IsUnlockWithCDCheck()&& SGESpells.即刻咏唱.IsUnlockWithCDCheck())
        {
            AI.Instance.BattleData.NextSlot = new Slot(1500);
            AI.Instance.BattleData.NextSlot.Add(new Spell(SGESpells.即刻咏唱, Core.Me));
            AI.Instance.BattleData.NextSlot.Add(new Spell(24287u, skillTarget));
            
        }
        
        else
        {   
            Core.Resolve<MemApiChatMessage>().Toast2("Not Ready", 1, 1000);
        }

               


    }
}
