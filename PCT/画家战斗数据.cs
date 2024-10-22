namespace Nagomi.PCT
{
    public class PCTBattleData
    {
        private static readonly PCTBattleData BattleData = new();
        public static PCTBattleData Instance = BattleData;

        public void Reset()
        {
            Instance = new PCTBattleData();
        }
    }
}