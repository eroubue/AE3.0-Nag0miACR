namespace Nagomi.SMN
{
    public class SMNBattleData
    {
        private static readonly SMNBattleData BattleData = new();
        public static SMNBattleData Instance = BattleData;

        public void Reset()
        {
            Instance = new SMNBattleData();
        }
    }
}