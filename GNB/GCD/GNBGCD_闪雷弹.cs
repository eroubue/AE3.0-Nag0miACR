// Decompiled with JetBrains decompiler
// Type: KKxb.Gunbreaker.SlotResolvers.GNBGCD_LightningShot
// Assembly: KKxb-EN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 47503ED8-500C-4AF6-83D9-A4468832A6B6
// Assembly location: C:\Users\ASUS\AppData\Roaming\XIVLauncherCN\devPlugins\3.0\ACR\KKxb-EN\KKxb-EN.dll

using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi.GNB.utils;

#nullable enable
namespace Nagomi.GNB.GCD
{
    public class GNBGCD_闪雷弹 : ISlotResolver
    {
        public SlotMode SlotMode { get; } = SlotMode.Gcd;

        public int Check()
        {
            if (Core.Me.GetCurrTarget().HasAnyAura(GNBBuffs.敌人无敌BUFF)) return -152;
            if (QT.QTGET(QTKey.停手))
            {
                return -100;
            } 
            if (!QT.QTGET(QTKey.闪雷弹))
            {
                return -100;
            } 
            if (!SpellExtension.IsReadyWithCanCast(GNBSpells.闪雷弹.GetSpell()))
                return -50;
            if (GNBSettings.Instance.额外技能距离!=0&&Core.Me.Distance(Core.Me.GetCurrTarget()) < 5+GNBSettings.Instance.额外技能距离)
                return -12;
            if (GNBSettings.Instance.额外技能距离==0&&Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) <
                SettingMgr.GetSetting<GeneralSettings>().AttackRange+5) return -5;
            if (Helper.技能是否刚使用过(GNBSpells.弹道, 1000)) return -10;
            return 0;

        }

        public void Build(Slot slot) => slot.Add(GNBSpells.闪雷弹.GetSpell());
    }
}