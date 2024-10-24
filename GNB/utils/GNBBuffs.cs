namespace Nagomi.GNB.utils;

public class GNBBuffs
{
    public const uint 命运之印预备 = 3839;//Ready to Raze 命运之环续剑
    public const uint 心有灵狮 = 3840; //Ready to Reign
    public const uint Medicated = 49;//爆发药状态
    public const uint 超高速预备 = 2686;//续剑爆发击
    public const uint 撕喉预备 = 1842;
    public const uint 裂膛预备 = 1843;//续剑2
    public const uint 穿目预备 = 1844;//续剑3
    public const uint 无情 = 1831;//无情
    public const uint 王室亲卫 = 1833;//Royal Guard
    public const uint 超火流星 = 1836;
    public const uint 行尸走肉 = 810;
    public const uint 出死入生 = 3255u;
    public const uint 死斗 = 409;
    public const uint 死而不僵 = 811u;
    public const uint 出生入死 = 3255u;

    public const uint 铁壁 = 1191;
    public const uint 刚玉之心 = 2683; 
    public const uint 石之心 = 1840;
    public const uint 星云 = 1834;
    public const uint 大星云 = 3838;//Great Nebula
    public const uint 伪装 = 1832;
    public const uint 极光 = 1835;

    /** 死亡宣告类 **/
    public const uint 塞壬的歌声 = 370u;

    public const uint 死亡宣告_1769 = 1769u;
    public const uint 死亡宣告_210 = 210u;

    //Boss无敌
    public const uint 无敌_325 = 325u;

    public const uint 无敌_529 = 529u;
    public const uint 无敌_656 = 656u;
    public const uint 无敌_671 = 671u;
    public const uint 无敌_775 = 775u;
    public const uint 无敌_776 = 776u;
    public const uint 无敌_969 = 969u;
    public const uint 无敌_981 = 981u;
    public const uint 无敌_1570 = 1570u;
    public const uint 无敌_1697 = 1697u;
    public const uint 无敌_1829 = 1829u;
    public const uint 土神的心石 = 328u;
    public const uint 纯正神圣领域 = 2287u;
    public const uint 冥界行 = 2670u;
    public const uint 风神障壁 = 3012u;
    public const uint 不死救赎 = 3039u;
  //  public const uint 出死入生 = 3255u;

    //特殊的
    public const uint 远程物理攻击无效化 = 941u; //远程物理攻击无法造成伤害
    public const uint 魔法攻击无效化_942 = 942u; //魔法攻击无法造成伤害
    public const uint 魔法攻击无效化_3621 = 3621u; //魔法攻击无法造成伤害

    public static readonly List<uint> 敌人无敌BUFF =
    [
        无敌_325,
        无敌_529,
        无敌_656,
        无敌_671,
        无敌_775,
        无敌_776,
        无敌_969,
        无敌_981,
        无敌_1570,
        无敌_1697,
        无敌_1829,
        土神的心石,
        纯正神圣领域,
        冥界行,
        风神障壁,
        不死救赎,
        出死入生,
        死而不僵,
        行尸走肉,
        出生入死,
        死斗,
        超火流星
    ];

}