using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.Helper;

namespace Millusion.ACR.Astrologian.SlotResolvers;

public static class NotTargetSlotResolvers
{
    public static async Task Run()
    {
        var battleData = AI.Instance.BattleData;
        foreach (var v in AST_SlotResolvers.SlotResolverDatas)
        {
            var valid = false;
            switch (v.SlotMode)
            {
                case SlotMode.Gcd when GCDHelper.CanUseGCD():
                case SlotMode.OffGcd when GCDHelper.CanUseOffGcd():
                case SlotMode.Always:
                    valid = true;
                    break;
                default:
                    valid = false;
                    break;
            }

            LogHelper.Debug($"RunSlotResolvers {v.SlotResolver.GetType().Name} {valid}");
            if (!valid || !await PVE_RunSlotHelper.CheckNext(battleData, v.SlotResolver)) continue;
            return;
        }
    }
}