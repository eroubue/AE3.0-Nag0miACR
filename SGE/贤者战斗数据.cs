namespace Nagomi.SGE;

public class SGEBattleData
{
    private static readonly SGEBattleData BattleData = new();
    public static SGEBattleData Instance = BattleData;

    public void Reset()
    {
        Instance = new SGEBattleData();
    }
}