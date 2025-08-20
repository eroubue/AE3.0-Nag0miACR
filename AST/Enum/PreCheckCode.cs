namespace Millusion.Enum;

public static class PreCheckCode
{
    #region 检查失败码

    public const int
        Initial = -9999,
        NotSpell = -1000,
        NotQT = -1001,
        NotReady = -1002,
        NotInRange = -1003,
        NotInCombat = -1004,
        NotEnoughMp = -1005,
        RecentlyUsed = -1006,
        NotCanCast = -1007,
        Stop = -1008,
        NotAction = - 1009,
        NotBuild = -1010,
        Test = -9999;

    #endregion

    #region 检查成功码

    public const int Success = 0;

    #endregion
}