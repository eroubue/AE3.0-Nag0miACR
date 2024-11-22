using AEAssist.CombatRoutine.Module;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Define;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Nagomi.GNB.utils;


namespace Nagomi.GNB.能力;

public class GNB能力_续剑 : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Always;
    public bool CheckSpell()
    {
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑)==GNBSpells.超音速) return true;
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑)==GNBSpells.撕喉) return true;
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑)==GNBSpells.裂膛) return true;
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑)==GNBSpells.穿目) return true;
        if (Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑)==GNBSpells.命运之印) return true;
        return false; 
    }
    public int Check()
    {
        if (QT.QTGET(QTKey.停手))
        {
            return -100;
        }
        if (!CheckSpell()) return -1;
       if (!Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑).IsUnlockWithCDCheck())
        {
            return -66;
        }
       if (GNBSettings.Instance.启用覆盖额外距离&&Core.Me.Distance(Core.Me.GetCurrTarget()) > 3+GNBSettings.Instance.额外技能距离)
            return -12;
     
        if (Core.Me.Distance(Core.Me.GetCurrTarget(), DistanceMode.IgnoreSourceHitbox | DistanceMode.IgnoreTargetHitbox) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -5;
       
        return 0;
    }


    public void Build(Slot slot)
    {
        slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(GNBSpells.续剑.GetSpell().Id).GetSpell());

    }
}