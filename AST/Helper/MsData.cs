using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using ImGuiNET;

namespace Millusion.Helper;

public class MsData
{
    public static MsData Instance { get; private set; } = new();

    public long MovedDuration { get; private set; }

    public long StopMovedDuration { get; private set; }

    private long _startMoveTime;

    private long _stopMoveTime;

    public void Update()
    {
        var currentTime = TimeHelper.Now();

        if (Core.Me.IsMoving())
        {
            if (_startMoveTime == 0) _startMoveTime = currentTime;

            MovedDuration = currentTime - _startMoveTime;
            _stopMoveTime = 0;
            StopMovedDuration = 0;
        }
        else
        {
            if (_stopMoveTime == 0) _stopMoveTime = currentTime;

            StopMovedDuration = currentTime - _stopMoveTime;
            _startMoveTime = 0;
            MovedDuration = 0;
        }
    }

    public static void Reset()
    {
        Instance = new MsData();
    }

    public void Draw(JobViewWindow jobViewWindow)
    {
        ImGui.Text($"移动的持续时间: {MovedDuration}");
        ImGui.Text($"停止移动的持续时间: {StopMovedDuration}");
    }
}