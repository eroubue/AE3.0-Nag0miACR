// Decompiled with JetBrains decompiler
// Type: Linto.LintoPvP.PVPApi.PVPHelper
// Assembly: Linto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDB54278-8776-48ED-88DE-295E616BB94A
// Assembly location: D:\XL\XIVLauncherCN\Roaming\devPlugins\3.0\ACR\Linto\Linto.dll

using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using System.Windows; // 引入剪贴板操作所需的命名空间

using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures.TextureWraps;
using ECommons.DalamudServices;
using ImGuiNET;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AEAssist.Verify;
using Nagomi;
using Nagomi.PVPMCH;
using Nagomi.PVPMCH.依赖;
using Nagomi.依赖.Helper;
using Nagomi.utils;
using Helper = GNB.utils.Helper.Helper;
using System;
using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.UI;

#nullable enable
namespace Nagomi.PvP.PVPApi
{
  

  public static class ClipboardHelper
{
    // 导入 user32.dll 中的 GetClipboardData 函数，用于获取剪贴板数据
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetClipboardData(uint uFormat);

    // 导入 user32.dll 中的 OpenClipboard 函数，用于打开剪贴板
    [DllImport("user32.dll")]
    private static extern bool OpenClipboard(IntPtr hWnd);

    // 导入 user32.dll 中的 CloseClipboard 函数，用于关闭剪贴板
    [DllImport("user32.dll")]
    private static extern bool CloseClipboard();

    // 导入 kernel32.dll 中的 GlobalLock 函数，用于锁定全局内存对象
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GlobalLock(IntPtr hMem);

    // 导入 kernel32.dll 中的 GlobalUnlock 函数，用于解锁全局内存对象
    [DllImport("kernel32.dll")]
    private static extern bool GlobalUnlock(IntPtr hMem);

    // 定义剪贴板格式常量，CF_TEXT 表示文本格式
    public const uint CF_TEXT = 1;

    // 设置剪贴板文本的方法
    public static void SetText(string text)
    {
        // 尝试打开剪贴板
        if (!OpenClipboard(IntPtr.Zero))
            throw new Exception("无法打开剪贴板");

        // 获取当前剪贴板中的文本数据
        IntPtr handle = GetClipboardData(CF_TEXT);
        if (handle != IntPtr.Zero)
            GlobalUnlock(handle); // 解锁当前剪贴板数据

        // 将要复制的文本转换为字节数组，并添加一个终止符
        byte[] data = System.Text.Encoding.ASCII.GetBytes(text + "\0");

        // 分配内存来存储字节数组
        IntPtr memory = Marshal.AllocCoTaskMem(data.Length);

        // 将字节数组复制到分配的内存中
        Marshal.Copy(data, 0, memory, data.Length);

        // 将内存中的数据设置为剪贴板数据
        SetClipboardData(CF_TEXT, memory);

        // 释放分配的内存
        Marshal.FreeCoTaskMem(memory);

        // 关闭剪贴板
        CloseClipboard();
    }

    // 导入 user32.dll 中的 SetClipboardData 函数，用于设置剪贴板数据
    [DllImport("user32.dll")]
    private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
}


  public class PVPHelper
  {

    private static bool 警报 = true;
    private static IBattleChara? Target;
    private static ushort 当前LB槽;

    public static bool active = true;
    public static bool CanActive()
    {
    
      if (!active)
      {
        return false;
      }
      if(!utils.Map.pvp地图.Contains(Helper.当前地图id))
      {
        return false;
      }
     
      if (Core.Me.HasAura(3054u))
      {
        return false;
      }
      if(Check坐骑())
      {
        return false;
      }
      if (PVPMCHSpells.防御.GetSpell().RecentlyUsed(5000))
      {
        return false;
      }
      return true;
    }

    public static Spell 不等服务器Spell(uint id, IBattleChara? target)
    {
      return new Spell(id, target) { WaitServerAcq = false };
    }

    public static bool Check坐骑()
    {
      return ((double)((IGameObject)Core.Me).HitboxRadius == 0.5 ? 1U : 0U) <= 0U;
    }

    public static Dictionary<uint, IBattleChara> GetList_all(float range = 50f)
    {
      if (!LocalPlayerExtension.IsPvP(Core.Me))
        return (Dictionary<uint, IBattleChara>)null;
      Dictionary<uint, IBattleChara> units = TargetMgr.Instance.Units;
      Dictionary<uint, IBattleChara> listAll = new Dictionary<uint, IBattleChara>();
      foreach (IBattleChara ibattleChara in units.Values)
      {
        if (!((IGameObject)ibattleChara).IsDead &&
            (double)GameObjectExtension.DistanceToPlayer((IGameObject)ibattleChara) <= (double)range)
        {
          listAll.Add(((IGameObject)ibattleChara).DataId, ibattleChara);
          break;
        }
      }

      return listAll;
    }

    public static List<IBattleChara> GetList_LookatMe(IBattleChara target, float range = 50f)
    {
      if (!LocalPlayerExtension.IsPvP(Core.Me))
        return (List<IBattleChara>)null;
      List<IBattleChara> listLookatMe = new List<IBattleChara>();
      foreach (IBattleChara ibattleChara in PVPHelper.GetList_all().Values)
      {
        if ((double)GameObjectExtension.Distance((IGameObject)ibattleChara, (IGameObject)Core.Me, (DistanceMode)7) <=
            (double)range)
          listLookatMe.Add(ibattleChara);
      }

      return listLookatMe;
    }


    public static bool HasBuff(IBattleChara BattleChara, uint buffId)
    {
      return GameObjectExtension.HasAura(BattleChara, buffId, 0);
    }

    public static void 技能图标(uint id)
    {
      uint num = id;
      Vector2 size = new Vector2(40f, 40f);
      IDalamudTextureWrap idalamudTextureWrap;

      if (Core.Resolve<MemApiIcon>().GetActionTexture(num, out idalamudTextureWrap, true))
      {
        if (idalamudTextureWrap != null)
        {
          ImGui.Image(idalamudTextureWrap.ImGuiHandle, size);
        }
        else
        {
          // 处理 idalamudTextureWrap 为 null 的情况
          // 例如：显示默认图标或错误消息
          Console.WriteLine("Failed to load texture for action ID: " + num);
        }
      }
    }



    public static IBattleChara? Get最近目标()
    {
      if (!LocalPlayerExtension.IsPvP(Core.Me))
        return (IBattleChara)Core.Me;
      Dictionary<uint, IBattleChara>.ValueCollection values = TargetMgr.Instance.EnemysIn25.Values;
      IBattleChara ibattleChara1 = (IBattleChara)null;
      float num = float.MaxValue;
      foreach (IBattleChara ibattleChara2 in values)
      {
        if (((IGameObject)ibattleChara2).IsTargetable & !GameObjectExtension.HasAura(ibattleChara2, 3054U, 0))
        {
          float player = GameObjectExtension.DistanceToPlayer((IGameObject)ibattleChara2);
          if ((double)player < (double)num)
          {
            ibattleChara1 = ibattleChara2;
            num = player;
          }
        }
      }

      return (IBattleChara)((object)ibattleChara1 ?? (object)Core.Me);
    }

    public static IBattleChara? Get最远目标()
    {
      if (!LocalPlayerExtension.IsPvP(Core.Me))
        return (IBattleChara)Core.Me;
      Dictionary<uint, IBattleChara>.ValueCollection values = TargetMgr.Instance.EnemysIn25.Values;
      IBattleChara ibattleChara1 = (IBattleChara)null;
      float num = 0.0f;
      foreach (IBattleChara ibattleChara2 in values)
      {
        if (((IGameObject)ibattleChara2).IsTargetable && !GameObjectExtension.HasAura(ibattleChara2, 3054U, 0))
        {
          float player = GameObjectExtension.DistanceToPlayer((IGameObject)ibattleChara2);
          if ((double)player > (double)num)
          {
            ibattleChara1 = ibattleChara2;
            num = player;
          }
        }
      }

      return (IBattleChara)((object)ibattleChara1 ?? (object)Core.Me);
    }




    public static IBattleChara? Get最合适目标(int 技能距离)
    {
      if (!LocalPlayerExtension.IsPvP(Core.Me))
        return (IBattleChara)Core.Me;
      Dictionary<uint, IBattleChara>.ValueCollection values = TargetMgr.Instance.EnemysIn25.Values;
      IBattleChara ibattleChara1 = (IBattleChara)null;
      float num = float.MaxValue;
      foreach (IBattleChara ibattleChara2 in values)
      {
        if (((IGameObject)ibattleChara2).IsTargetable &&
            !GameObjectExtension.HasAura(ibattleChara2, 3054U, 0) &
            !GameObjectExtension.HasAura(ibattleChara2, 3039U, 0) &&
            !GameObjectExtension.HasAura(ibattleChara2, 2413U, 0) &&
            !GameObjectExtension.HasAura(ibattleChara2, 1301U, 0) &&
            (double)GameObjectExtension.DistanceToPlayer((IGameObject)ibattleChara2) <= (double)技能距离 &&
            (double)((ICharacter)ibattleChara2).CurrentHp < (double)num)
        {
          ibattleChara1 = ibattleChara2;
          num = (float)((ICharacter)ibattleChara2).CurrentHp;
        }
      }

      return (IBattleChara)((object)ibattleChara1 ?? (object)Core.Me);
    }
public static IBattleChara Get多斩Target(int 多斩count)
{
    // 检查是否处于PvP模式并且LB槽值大于等于4000
    if (!LocalPlayerExtension.IsPvP(Core.Me) || LocalPlayerExtension.LimitBreakCurrentValue(Core.Me) < 4000)
    {
        return (IBattleChara) Core.Me;
    }

    var enemies = TargetMgr.Instance.EnemysIn25.Values;
    var validTargets = new List<IBattleChara>();

    // 遍历敌人列表，筛选出符合条件的目标
    foreach (var enemy in enemies)
    {
        if (GameObjectExtension.HasLocalPlayerAura(enemy, 3202U) &&
            !GameObjectExtension.HasAura(enemy, 2413U, 0) &&
            !GameObjectExtension.HasAura(enemy, 1301U, 0) &&
            GameObjectExtension.CurrentHpPercent((ICharacter) enemy) + ((ICharacter) enemy).ShieldPercentage / 100.0 <= 1.0)
        {
            validTargets.Add(enemy);
        }
    }

    // 遍历符合条件的目标，计算每个目标周围的敌人数量
    foreach (var target in validTargets)
    {
        int nearbyEnemiesCount = 0;
        foreach (var enemy in enemies)
        {
            if ((int)((IGameObject) target).DataId != (int)((IGameObject) enemy).DataId &&
                GameObjectExtension.Distance((IGameObject) target, (IGameObject) enemy, DistanceMode.Point) <= 50.0 &&
                GameObjectExtension.HasLocalPlayerAura(enemy, 3202U) &&
                GameObjectExtension.CurrentHpPercent((ICharacter) enemy) + ((ICharacter) enemy).ShieldPercentage / 100.0 <= 1.0 &&
                !GameObjectExtension.HasAura(enemy, 2413U, 0) &&
                !GameObjectExtension.HasAura(enemy, 1301U, 0))
            {
                nearbyEnemiesCount++;
                if (nearbyEnemiesCount >= 多斩count)
                {
                    return ((IGameObject) target).IsTargetable ? target : (IBattleChara) Core.Me;
                }
            }
        }
    }
    

    // 如果没有找到符合条件的目标，返回当前玩家
    return (IBattleChara) Core.Me;
    
}






    public static void 通用设置配置(string 说明)
    {
      ImGui.Checkbox("脱战也喝热水(测试)", ref PvPSettings.Instance.脱战嗑药);
      ImGui.Checkbox("自动选中25米内最近目标", ref PvPSettings.Instance.自动选中);
      ImGui.Checkbox("技能自动对最近敌人释放", ref PvPSettings.Instance.技能自动选中);
      if (!PvPSettings.Instance.技能自动选中)
        return;
      ImGui.Checkbox("!!选择技能范围内最合适目标(血量最低)!!", ref PvPSettings.Instance.最合适目标);
    }
  




    public static void 技能配置界面(uint 技能图标id, string 技能名字)
    {
      ImGui.Separator();
      PVPHelper.技能图标(技能图标id);
      ImGui.SameLine();
      ImGui.Text(技能名字);
    }

    public static void 技能配置切换(string 描述文字, ref bool 切换配置, int id)
    {
      ImGui.Text(描述文字);
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
      interpolatedStringHandler.AppendFormatted<bool>(切换配置);
      ImGui.Text(interpolatedStringHandler.ToStringAndClear());
      ImGui.SameLine();
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
      interpolatedStringHandler.AppendLiteral("切换##");
      interpolatedStringHandler.AppendFormatted<int>(id);
      if (ImGui.Button(interpolatedStringHandler.ToStringAndClear()))
        切换配置 = !切换配置;
      PVPMCHSettings.Instance.Save();
    }

    public static void 通用技能释放(Slot slot, uint skillid, int 距离)
    {
      if (PvPSettings.Instance.技能自动选中)
      {
        if (PvPSettings.Instance.最合适目标)
          slot.Add(PVPHelper.不等服务器Spell(skillid, PVPHelper.Get最合适目标(距离)));
        else
          slot.Add(PVPHelper.不等服务器Spell(skillid, PVPHelper.Get最近目标()));
      }
      else
        slot.Add(PVPHelper.不等服务器Spell(skillid, GameObjectExtension.GetCurrTarget((IBattleChara)Core.Me)));
    }

    public static bool 通用距离检查(int 距离)
    {
      if (PvPSettings.Instance.技能自动选中)
      {
        if (PvPSettings.Instance.最合适目标)
        {
          if ((double)GameObjectExtension.DistanceToPlayer((IGameObject)PVPHelper.Get最合适目标(距离)) > (double)距离)
            return true;
        }
        else if ((double)GameObjectExtension.DistanceToPlayer((IGameObject)PVPHelper.Get最近目标()) > (double)距离)
          return true;
      }
      else if (!PvPSettings.Instance.技能自动选中 &&
               (double)GameObjectExtension.DistanceToPlayer(
                 (IGameObject)GameObjectExtension.GetCurrTarget((IBattleChara)Core.Me)) > (double)距离)
        return true;

      return false;
    }

    public class 龟壳 : IHotkeyResolver
    {
      public void Draw(Vector2 size)
      {
        Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        IDalamudTextureWrap idalamudTextureWrap;

        if (Core.Resolve<MemApiIcon>().GetActionTexture(29054U, out idalamudTextureWrap, true))
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
        SpellHelper.DrawSpellInfo(new Spell(29054U, (IBattleChara)Core.Me), size, isActive);
      }

      public int Check() => 0;

      public void Run()
      {
        if (AI.Instance.BattleData.NextSlot == null)
          AI.Instance.BattleData.NextSlot = new Slot(1500);

        if (!GameObjectExtension.HasLocalPlayerAura((IBattleChara)Core.Me, 3054U) &&
            GameObjectExtension.InCombat((ICharacter)Core.Me))
        {
          AI.Instance.BattleData.NextSlot.Add(new Spell(29054U, (IBattleChara)Core.Me));
        }
      }
    }
  }
}
