namespace Millusion.CharacterRefined;

public record LevelModifier(int Main, int Sub, int Div);

public static class LevelModifiers
{
    public static readonly Dictionary<int, LevelModifier> LevelTable = new()
    {
        [1] = new LevelModifier(20, 56, 56),
        [2] = new LevelModifier(21, 57, 57),
        [3] = new LevelModifier(22, 60, 60),
        [4] = new LevelModifier(24, 62, 62),
        [5] = new LevelModifier(26, 65, 65),
        [6] = new LevelModifier(27, 68, 68),
        [7] = new LevelModifier(29, 70, 70),
        [8] = new LevelModifier(31, 73, 73),
        [9] = new LevelModifier(33, 76, 76),
        [10] = new LevelModifier(35, 78, 78),
        [11] = new LevelModifier(36, 82, 82),
        [12] = new LevelModifier(38, 85, 85),
        [13] = new LevelModifier(41, 89, 89),
        [14] = new LevelModifier(44, 93, 93),
        [15] = new LevelModifier(46, 96, 96),
        [16] = new LevelModifier(49, 100, 100),
        [17] = new LevelModifier(52, 104, 104),
        [18] = new LevelModifier(54, 109, 109),
        [19] = new LevelModifier(57, 113, 113),
        [20] = new LevelModifier(60, 116, 116),
        [21] = new LevelModifier(63, 122, 122),
        [22] = new LevelModifier(67, 127, 127),
        [23] = new LevelModifier(71, 133, 133),
        [24] = new LevelModifier(74, 138, 138),
        [25] = new LevelModifier(78, 144, 144),
        [26] = new LevelModifier(81, 150, 150),
        [27] = new LevelModifier(85, 155, 155),
        [28] = new LevelModifier(89, 162, 162),
        [29] = new LevelModifier(92, 168, 168),
        [30] = new LevelModifier(97, 173, 173),
        [31] = new LevelModifier(101, 181, 181),
        [32] = new LevelModifier(106, 188, 188),
        [33] = new LevelModifier(110, 194, 194),
        [34] = new LevelModifier(115, 202, 202),
        [35] = new LevelModifier(119, 209, 209),
        [36] = new LevelModifier(124, 215, 215),
        [37] = new LevelModifier(128, 223, 223),
        [38] = new LevelModifier(134, 229, 229),
        [39] = new LevelModifier(139, 236, 236),
        [40] = new LevelModifier(144, 244, 244),
        [41] = new LevelModifier(150, 253, 253),
        [42] = new LevelModifier(155, 263, 263),
        [43] = new LevelModifier(161, 272, 272),
        [44] = new LevelModifier(166, 283, 283),
        [45] = new LevelModifier(171, 292, 292),
        [46] = new LevelModifier(177, 302, 302),
        [47] = new LevelModifier(183, 311, 311),
        [48] = new LevelModifier(189, 322, 322),
        [49] = new LevelModifier(196, 331, 331),
        [50] = new LevelModifier(202, 341, 341),
        [51] = new LevelModifier(204, 342, 366),
        [52] = new LevelModifier(205, 344, 392),
        [53] = new LevelModifier(207, 345, 418),
        [54] = new LevelModifier(209, 346, 444),
        [55] = new LevelModifier(210, 347, 470),
        [56] = new LevelModifier(212, 349, 496),
        [57] = new LevelModifier(214, 350, 522),
        [58] = new LevelModifier(215, 351, 548),
        [59] = new LevelModifier(217, 352, 574),
        [60] = new LevelModifier(218, 354, 600),
        [61] = new LevelModifier(224, 355, 630),
        [62] = new LevelModifier(228, 356, 660),
        [63] = new LevelModifier(236, 357, 690),
        [64] = new LevelModifier(244, 358, 720),
        [65] = new LevelModifier(252, 359, 750),
        [66] = new LevelModifier(260, 360, 780),
        [67] = new LevelModifier(268, 361, 810),
        [68] = new LevelModifier(276, 362, 840),
        [69] = new LevelModifier(284, 363, 870),
        [70] = new LevelModifier(292, 364, 900),
        [71] = new LevelModifier(296, 365, 940),
        [72] = new LevelModifier(300, 366, 980),
        [73] = new LevelModifier(305, 367, 1020),
        [74] = new LevelModifier(310, 368, 1060),
        [75] = new LevelModifier(315, 370, 1100),
        [76] = new LevelModifier(320, 372, 1140),
        [77] = new LevelModifier(325, 374, 1180),
        [78] = new LevelModifier(330, 376, 1220),
        [79] = new LevelModifier(335, 378, 1260),
        [80] = new LevelModifier(340, 380, 1300),
        [81] = new LevelModifier(345, 382, 1360),
        [82] = new LevelModifier(350, 384, 1420),
        [83] = new LevelModifier(355, 386, 1480),
        [84] = new LevelModifier(360, 388, 1540),
        [85] = new LevelModifier(365, 390, 1600),
        [86] = new LevelModifier(370, 392, 1660),
        [87] = new LevelModifier(375, 394, 1720),
        [88] = new LevelModifier(380, 396, 1780),
        [89] = new LevelModifier(385, 398, 1840),
        [90] = new LevelModifier(390, 400, 1900),
        [91] = new LevelModifier(395, 402, 1988),
        [92] = new LevelModifier(400, 404, 2076),
        [93] = new LevelModifier(405, 406, 2164),
        [94] = new LevelModifier(410, 408, 2252),
        [95] = new LevelModifier(415, 410, 2340),
        [96] = new LevelModifier(420, 412, 2428),
        [97] = new LevelModifier(425, 414, 2516),
        [98] = new LevelModifier(430, 416, 2604),
        [99] = new LevelModifier(435, 418, 2692),
        [100] = new LevelModifier(440, 420, 2780)
    };

    // this seems to be the modifiers after some testing..
    public static double AttackModifier(int level)
    {
        return level switch
        {
            <= 50 => 75,
            <= 70 => (level - 50) * 2.5 + 75,
            <= 80 => (level - 70) * 4 + 125,
            <= 90 => (level - 80) * 3 + 165,
            _ => (level - 90) * 4.2 + 195
        };
    }

    public static double HealModifier(int level)
    {
        return level switch
        {
            < 60 => level * 1.5 + 10,
            < 70 => (level - 60) * 2 + 100,
            < 80 => 120,
            _ => (level - 80) * 2.5 + 120.8
        };
    }

    public static double TankAttackModifier(int level)
    {
        return level switch
        {
            <= 80 => level + 35,
            <= 90 => (level - 80) * 4.1 + 115,
            _ => (level - 90) * 3.4 + 156
        };
    }

    public static double HpModifier(int level)
    {
        return Math.Floor(5.71 * level - 270) / 10;
    }

    public static double TankHpModifier(int level)
    {
        return Math.Floor(8.31 * level - 401) / 10;
    }
}