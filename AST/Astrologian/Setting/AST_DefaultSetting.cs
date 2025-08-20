using AEAssist.CombatRoutine;

namespace Millusion.ACR.Astrologian.Setting;

public class AST_DefaultSetting
{
    public Dictionary<Jobs, int> BalanceCardTargetDict = new()
    {
        { Jobs.Monk, 0 },
        { Jobs.Dragoon, 1 },
        { Jobs.Ninja, 2 },
        { Jobs.Samurai, 3 },
        { Jobs.Reaper, 4 },
        { Jobs.Viper, 5 },
        { Jobs.BlackMage, 6 },
        { Jobs.Summoner, 7 },
        { Jobs.RedMage, 8 },
        { Jobs.Pictomancer, 9 },
        { Jobs.Bard, 10 },
        { Jobs.Machinist, 11 },
        { Jobs.Dancer, 12 }
    };

    public List<Jobs> BalanceCardTargetList =
    [
        Jobs.Monk, Jobs.Dragoon, Jobs.Ninja, Jobs.Samurai, Jobs.Reaper, Jobs.Viper, Jobs.BlackMage, Jobs.Summoner,
        Jobs.RedMage, Jobs.Pictomancer, Jobs.Bard, Jobs.Machinist, Jobs.Dancer
    ];

    public Dictionary<Jobs, int> SpareCardTargetDict = new()
    {
        { Jobs.BlackMage, 0 },
        { Jobs.Summoner, 1 },
        { Jobs.RedMage, 2 },
        { Jobs.Pictomancer, 3 },
        { Jobs.Bard, 4 },
        { Jobs.Machinist, 5 },
        { Jobs.Dancer, 6 },
        { Jobs.Monk, 7 },
        { Jobs.Dragoon, 8 },
        { Jobs.Ninja, 9 },
        { Jobs.Samurai, 10 },
        { Jobs.Reaper, 11 },
        { Jobs.Viper, 12 }
    };

    public List<Jobs> SpareCardTargetList =
    [
        Jobs.BlackMage, Jobs.Summoner, Jobs.RedMage, Jobs.Pictomancer, Jobs.Bard,
        Jobs.Machinist, Jobs.Dancer,
        Jobs.Monk, Jobs.Dragoon, Jobs.Ninja, Jobs.Samurai, Jobs.Reaper, Jobs.Viper
    ];
}