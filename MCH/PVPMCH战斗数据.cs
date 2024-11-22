namespace Nagomi.PVPMCH;

public class PVPMCHBattleData
{
        private static readonly PVPMCHBattleData BattleData = new();
        public static PVPMCHBattleData Instance = BattleData;

        public void Reset()
        {
            Instance = new PVPMCHBattleData();
        }
        
}