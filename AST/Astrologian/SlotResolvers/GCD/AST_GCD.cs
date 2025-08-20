using AEAssist.CombatRoutine.Module;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers.GCD;

public class AST_GCD: MsSlotResolver
{
    public override int RunCheck(out Action<Slot> build)
    {
        if (AST_GCD_Malefic.Instance.Check() >= 0)
        {
            build = AST_GCD_Malefic.Instance.Build;
            return 1;
        }
        build = null;
        return 0;
    }
}