namespace Nagomi.GNB;

public class GNBBattleData
{
    private static readonly GNBBattleData BattleData = new();
    public static GNBBattleData Instance = BattleData;

    // 计时器相关属性
    public DateTime NoTargetStartTime { get; set; } = DateTime.MinValue;
    public TimeSpan NoTargetDuration => NoTargetStartTime != DateTime.MinValue ? DateTime.Now - NoTargetStartTime : TimeSpan.Zero;
    public bool IsNoTargetActive => NoTargetStartTime != DateTime.MinValue;

    public void Reset()
    {
        Instance = new GNBBattleData();
    }

    // 开始无目标计时器
    public void StartNoTargetTimer()
    {
        NoTargetStartTime = DateTime.Now;
    }

    // 停止无目标计时器
    public void StopNoTargetTimer()
    {
        NoTargetStartTime = DateTime.MinValue;
    }
}