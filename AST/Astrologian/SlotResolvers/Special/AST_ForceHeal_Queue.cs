using AEAssist.CombatRoutine.Module;
using Millusion.ACR.Astrologian.Setting;

namespace Millusion.ACR.Astrologian.SlotResolvers.Special;

public class AST_ForceHeal_Queue : ISlotSequence
{
    public List<Action<Slot>> Sequence { get; }

    public int StartCheck()
    {
        if (!AST_Settings.Instance.ForceHealTarget) return -1;

        return 0;
    }

    public int StopCheck(int index)
    {
        return 0;
    }
}