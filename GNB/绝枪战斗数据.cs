namespace Nagomi.GNB;

public class GNBBattleData
{
    private static readonly GNBBattleData BattleData = new();
    public static GNBBattleData Instance = BattleData;

    public void Reset()
    {
        Instance = new GNBBattleData();
    }
}