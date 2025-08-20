using AEAssist.CombatRoutine.Module;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;

namespace Millusion.Interface;

public abstract class MsSlotResolver : ISlotResolver
{
    private Action<Slot> BuildAction { get; set; }

    public abstract int RunCheck(out Action<Slot> build);

    public int Code { get; private set; } = PreCheckCode.Initial;

    public int Check()
    {
        if (AST_View.UI.GetQt(AST_QT_Key.Stop)) return PreCheckCode.Stop;

        Update();

        if (BuildAction == null) return PreCheckCode.NotBuild;

        return Code;
    }

    public void Build(Slot slot)
    {
        BuildAction?.Invoke(slot);
    }

    public void Update()
    {
        Reset();

        Code = RunCheck(out var build);
        BuildAction = build;
    }

    public void Reset()
    {
        Code = PreCheckCode.Initial;
        BuildAction = null;
    }
}