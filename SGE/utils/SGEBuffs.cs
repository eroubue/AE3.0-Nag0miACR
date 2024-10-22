namespace Nagomi.SGE.utils;



public static class SGEBuffs
{
    /** 白魔 **/
    public const uint 即刻咏唱 = 167u;

    public const uint 复活 = 148u;

    /** 限制类 **/
    public const uint 限制复活 = 3380u;


    /** 驱散类 **/
    public const uint 窒息_938 = 938u;

    public const uint 窒息_700 = 700u;
    public const uint 物理受伤加重_200 = 200u;

    /** 死亡宣告类 **/
    public const uint 塞壬的歌声 = 370u;

    public const uint 死亡宣告_1769 = 1769u;
    public const uint 死亡宣告_210 = 210u;


    /** 无敌类 **/
    public const uint 死而不僵 = 811u;

    public const uint 行尸走肉 = 810u;
    public const uint 出生入死 = 3255u;
    public const uint 神圣领域 = 1302u;
    public const uint 死斗 = 409u;
    public const uint 超火流星 = 1836u;

    public static List<uint> 无敌Buff =
    [
        死而不僵,
        行尸走肉,
        出生入死,
        死斗,
        超火流星,
        神圣领域
    ];
    
    public static List<uint> 真无敌Buff =
    [
        超火流星,
        神圣领域
    ];

    public static readonly List<uint> 假无敌Buff =
    [
        死而不僵,
        行尸走肉,
        出生入死,
        死斗
    ];

    /** 无敌类-BOSS **/
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
    public const uint 出死入生 = 3255u;

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


    /** 无法造成伤害类 **/
    public const uint 愤怒的化身 = 2208u; //愤怒不已，愤怒状态的玩家无法造成伤害

    public const uint 悲叹的化身 = 2209u; //悲叹不已，悲叹状态的玩家无法造成伤害
    public const uint 青蛙 = 2671u; //变成一只青蛙，无法发动任何技能

    public static readonly List<uint> 无法造成伤害 = [愤怒的化身, 悲叹的化身 ,青蛙];

    public static readonly List<uint> 无法发动技能类 = [青蛙];

    /** 持续掉血类 **/
    public const uint 中毒18 = 18u; //毒素会导致体力逐渐减少

    public const uint 猛毒19 = 19u; //毒素会导致体力逐渐减少
    public const uint 秘孔拳106 = 106u; //体力逐渐减少
    public const uint 樱花怒放118 = 118u; //体力逐渐减少
    public const uint 二重刺119 = 119u; //体力逐渐减少
    public const uint 毒咬箭124 = 124u; //身中剧毒，体力会逐渐减少
    public const uint 风蚀箭129 = 129u; //风属性持续伤害，体力逐渐流失
    public const uint 疾风143 = 143u; //风属性持续伤害，体力逐渐流失
    public const uint 烈风144 = 144u; //风属性持续伤害，体力逐渐流失
    public const uint 闪雷161 = 161u; //雷属性持续伤害，体力逐渐流失
    public const uint 震雷162 = 162u; //雷属性持续伤害，体力逐渐流失
    public const uint 暴雷163 = 163u; //雷属性持续伤害，体力逐渐流失
    public const uint 毒菌179 = 179u; //体力逐渐减少
    public const uint 瘴气180 = 180u; //体力逐渐减少
    public const uint 瘴疠188 = 188u; //体力逐渐减少
    public const uint 猛毒菌189 = 189u; //体力逐渐减少
    public const uint 裂伤235 = 235u; //风属性持续伤害，体力逐渐流失
    public const uint 陆行鸟猛啄236 = 236u; //体力逐渐减少
    public const uint 碎骨打244 = 244u; //体力逐渐减少
    public const uint 破碎拳246 = 246u; //体力逐渐减少
    public const uint 厄运流转248 = 248u; //体力逐渐减少

    public const uint 火伤250 = 250u; //火属性持续伤害，体力逐渐流失
    public const uint 切伤264 = 264u; //斩击属性持续伤害，体力逐渐流失
    public const uint 刺伤265 = 265u; //突刺属性持续伤害，体力逐渐流失
    public const uint 打伤266 = 266u; //打击属性持续伤害，体力逐渐流失
    public const uint 火伤267 = 267u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤268 = 268u; //冰属性持续伤害，体力逐渐流失
    public const uint 裂伤269 = 269u; //风属性持续伤害，体力逐渐流失
    public const uint 污泥270 = 270u; //土属性持续伤害，体力逐渐流失
    public const uint 感电271 = 271u; //雷属性持续伤害，体力逐渐流失
    public const uint 水毒272 = 272u; //水属性持续伤害，体力逐渐流失
    public const uint 出血273 = 273u; //无属性持续伤害，体力逐渐流失
    public const uint 中毒275 = 275u; //毒素会导致体力逐渐减少
    public const uint 水毒283 = 283u; //水属性持续伤害，体力逐渐流失
    public const uint 火伤284 = 284u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤285 = 285u; //冰属性持续伤害，体力逐渐流失
    public const uint 裂伤286 = 286u; //风属性持续伤害，体力逐渐流失
    public const uint 污泥287 = 287u; //土属性持续伤害，体力逐渐流失
    public const uint 感电288 = 288u; //雷属性持续伤害，体力逐渐流失
    public const uint 水毒289 = 289u; //水属性持续伤害，体力逐渐流失
    public const uint 黄金沼295 = 295u; //受到黄毒沼的影响，体力逐渐减少
    public const uint 地狱之火炎314 = 314u; //火属性持续伤害，体力逐渐流失
    public const uint 出血320 = 320u; //无属性持续伤害，体力逐渐流失
    public const uint 元灵之怒324 = 324u; //惹怒了森林的元灵，体力逐渐减少
    public const uint 火焰流335 = 335u; //受到高热射线的照射，体力逐渐减少
    public const uint 出血339 = 339u; //无属性持续伤害，体力逐渐流失
    public const uint 出血343 = 343u; //无属性持续伤害，体力逐渐流失
    public const uint 神枪昆古尼尔的魔力352 = 352u; //受到神枪昆古尼尔的影响，体力逐渐流失
    public const uint 抓捕401 = 401u; //被抓住，无法行动，体力逐渐减少
    public const uint 捕食421 = 421u; //被吞了下去无法做出任何行动，体力逐渐减少
    public const uint 荆棘436 = 436u; //被荆棘缠住了双脚，移动速度降低，并且体力会逐渐减少 但该状态下敌人攻击中的吸引和击退效果会被无效化
    public const uint 荆棘丛生445 = 445u; //被荆棘缠住，体力会逐渐减少
    public const uint 亚拉戈蛇毒453 = 453u; //中了亚拉戈蛇毒，全体小队队员都会逐渐流失体力
    public const uint 毒墨484 = 484u; //被克拉肯喷上了墨汁，移动速度降低，体力逐渐流失
    public const uint 水毒485 = 485u; //水属性持续伤害，体力逐渐流失
    public const uint 冻结487 = 487u; //身体被冻结无法行动，体力逐渐流失
    public const uint 无双旋492 = 492u; //体力逐渐减少
    public const uint 火伤503 = 503u; //火属性持续伤害，体力逐渐流失
    public const uint 影牙508 = 508u; //体力逐渐减少
    public const uint 滚雷515 = 515u; //变成雷电的发射源，体力会逐渐流失，经过一段时间之后会积累得越来越多，体力流失速度会变快
    public const uint 火伤530 = 530u; //火属性持续伤害，体力逐渐流失
    public const uint 水毒531 = 531u; //水属性持续伤害，体力逐渐流失
    public const uint 裂伤532 = 532u; //风属性持续伤害，体力逐渐流失
    public const uint 感电533 = 533u; //雷属性持续伤害，体力逐渐流失
    public const uint 污泥534 = 534u; //土属性持续伤害，体力逐渐流失
    public const uint 冻伤535 = 535u; //冰属性持续伤害，体力逐渐流失
    public const uint 中毒559 = 559u; //毒素会导致体力逐渐减少
    public const uint 中毒560 = 560u; //毒素会导致体力逐渐减少
    public const uint 粘液569 = 569u; //身上粘粘的，动作迟缓，体力逐渐流失
    public const uint 魔科学粒子α580 = 580u; //身上附带着古代亚拉戈技术产生的魔科学粒子α，在特定条件下会逐渐流失体力
    public const uint 魔科学粒子β581 = 581u; //身上附带着古代亚拉戈技术产生的魔科学粒子β，在特定条件下会逐渐流失体力
    public const uint 冻伤605 = 605u; //冰属性持续伤害，体力逐渐流失
    public const uint 抓捕609 = 609u; //被抓住，无法行动，体力逐渐减少
    public const uint 火伤619 = 619u; //火属性持续伤害，体力逐渐流失
    public const uint 切伤624 = 624u; //斩击属性持续伤害，体力逐渐流失
    public const uint 出血642 = 642u; //无属性持续伤害，体力逐渐流失
    public const uint 出血643 = 643u; //无属性持续伤害，体力逐渐流失
    public const uint 消化中645 = 645u; //正在被消化，体力逐渐流失且受到的伤害有所增加
    public const uint 毒雾659 = 659u; //无属性持续伤害，体力逐渐流失，且受到的伤害有所增加
    public const uint 感电666 = 666u; //雷属性持续伤害，体力逐渐流失
    public const uint 剧毒677 = 677u; //身中剧毒，自身所受的治疗魔法效果降低，体力逐渐减少
    public const uint 中毒686 = 686u; //毒素会导致体力逐渐减少
    public const uint 湍流716 = 716u; //受到了气流影响，体力逐渐减少，积累的越多越容易受到俾斯麦魔力的影响
    public const uint 沥血剑725 = 725u; //体力逐渐减少
    public const uint 灾变741 = 741u; //体力逐渐减少
    public const uint 烈焰链769 = 769u; //被烈焰链连接，体力逐渐减少
    public const uint 暴风798 = 798u; //风属性持续伤害，体力逐渐流失
    public const uint 中毒801 = 801u; //毒素会导致体力逐渐减少
    public const uint 粘液809 = 809u; //身上粘粘的，动作迟缓，体力逐渐流失
    public const uint 烧灼838 = 838u; //体力逐渐减少
    public const uint 炽灼843 = 843u; //体力逐渐减少
    public const uint 铅弹854 = 854u; //体力逐渐减少
    public const uint 出血940 = 940u; //无属性持续伤害，体力逐渐流失
    public const uint 抓捕961 = 961u; //被抓住，无法行动，体力逐渐减少
    public const uint 持续伤害979 = 979u; //体力逐渐减少
    public const uint 出血毒1004 = 1004u; //中了出血毒，体力逐渐减少。积累档数过多会陷入无法战斗状态
    public const uint 剧毒1011 = 1011u; //身中剧毒，自身所受的治疗魔法效果降低，体力逐渐减少
    public const uint 剧毒1046 = 1046u; //身中剧毒，自身所受的治疗魔法效果降低，体力逐渐减少
    public const uint 消化粘液1073 = 1073u; //沾到强酸性粘液，移动速度降低，体力逐渐减少
    public const uint 出血1074 = 1074u; //无属性持续伤害，体力逐渐流失
    public const uint 闪电链1077 = 1077u; //被闪电链连接，体力逐渐减少
    public const uint 诅咒1087 = 1087u; //攻击所造成的伤害降低，体力无法自然恢复并且会逐渐减少
    public const uint 刺伤1117 = 1117u; //突刺属性持续伤害，体力逐渐流失
    public const uint 双刃剑1145 = 1145u; //攻击所造成的伤害增加，但体力会逐渐减少
    public const uint 影之脚镣1147 = 1147u; //被影之脚镣铐住，移动速度降低，体力逐渐减少
    public const uint 冻结1150 = 1150u; //身体被冻结无法行动，体力逐渐流失
    public const uint 无尽痛苦刻印1159 = 1159u; //阵阵剧痛从刻印传来，体力逐渐减少，受到攻击的伤害增加
    public const uint 弱化试剂1160 = 1160u; //被投用了弱化试剂，体力逐渐减少
    public const uint 无尽痛苦刻印1162 = 1162u; //阵阵剧痛从刻印传来，体力逐渐减少，受到攻击的伤害增加
    public const uint 烈毒咬箭1200 = 1200u; //身中剧毒，体力会逐渐减少
    public const uint 狂风蚀箭1201 = 1201u; //风属性持续伤害，体力逐渐流失
    public const uint 霹雷1210 = 1210u; //雷属性持续伤害，体力逐渐流失
    public const uint 剧毒菌1214 = 1214u; //体力逐渐减少
    public const uint 瘴暍1215 = 1215u; //体力逐渐减少
    public const uint 彼岸花1228 = 1228u; //体力逐渐减少
    public const uint 冻结1254 = 1254u; //身体被冻结无法行动，体力逐渐流失
    public const uint 抓捕1287 = 1287u; //被抓住，无法行动，体力逐渐减少
    public const uint 感电1328 = 1328u; //雷属性持续伤害，体力逐渐流失
    public const uint 生命吸收1377 = 1377u; //生命力被吸收，体力逐渐流失
    public const uint 至高无上1379 = 1379u; //受到天体魔法影响，体力逐渐减少
    public const uint 污泥1386 = 1386u; //土属性持续伤害，体力逐渐流失
    public const uint 神龙诅咒1419 = 1419u; //受到了神龙的诅咒，一切体力恢复效果失效，且体力逐渐减少，但可通过发动攻击逐渐恢复体力
    public const uint 刺伤1466 = 1466u; //突刺属性持续伤害，体力逐渐流失
    public const uint 污泥1474 = 1474u; //土属性持续伤害，体力逐渐流失
    public const uint 连接1478 = 1478u; //和魔列车连接在一起，受到的伤害增加，体力逐渐减少
    public const uint 震荡1498 = 1498u; //受到了强烈冲击，偶尔会无法行动，体力逐渐流失
    public const uint 猛毒1507 = 1507u; //毒素会导致体力逐渐减少
    public const uint 裂伤1508 = 1508u; //风属性持续伤害，体力逐渐流失
    public const uint 打伤1519 = 1519u; //打击属性持续伤害，体力逐渐流失
    public const uint 诅咒之炎1527 = 1527u; //火属性持续伤害，体力逐渐流失
    public const uint 火伤1577 = 1577u; //火属性持续伤害，体力逐渐流失
    public const uint 出血1714 = 1714u; //无属性持续伤害，体力逐渐流失
    public const uint 裂伤1723 = 1723u; //风属性持续伤害，体力逐渐流失
    public const uint 水毒1736 = 1736u; //水属性持续伤害，体力逐渐流失
    public const uint 冻结1758 = 1758u; //身体被冻结无法行动，体力逐渐流失
    public const uint 紫阳花1779 = 1779u; //体力逐渐减少
    public const uint 火伤1787 = 1787u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤1788 = 1788u; //冰属性持续伤害，体力逐渐流失
    public const uint 影之脚镣1790 = 1790u; //被影之脚镣铐住，移动速度降低，体力逐渐减少
    public const uint 切伤1813 = 1813u; //斩击属性持续伤害，体力逐渐流失
    public const uint 音速破1837 = 1837u; //体力逐渐减少
    public const uint 弓形冲波1838 = 1838u; //体力逐渐减少
    public const uint 毒菌冲击1866 = 1866u; //体力逐渐减少
    public const uint 天辉1871 = 1871u; //体力逐渐减少
    public const uint 焚灼1881 = 1881u; //体力逐渐减少
    public const uint 蛊毒法1895 = 1895u; //体力逐渐减少
    public const uint 感电1899 = 1899u; //雷属性持续伤害，体力逐渐流失
    public const uint 体温下降1940 = 1940u; //因体温降低，体力逐渐流失
    public const uint 隆卡咒毒1941 = 1941u; //受到隆卡咒毒的影响，体力逐渐减少
    public const uint 毒菌冲击2019 = 2019u; //体力逐渐减少
    public const uint 酸咬箭2073 = 2073u; //身中剧毒，体力会逐渐减少
    public const uint 切伤2079 = 2079u; //斩击属性持续伤害，体力逐渐流失
    public const uint 刺伤2080 = 2080u; //突刺属性持续伤害，体力逐渐流失
    public const uint 打伤2081 = 2081u; //打击属性持续伤害，体力逐渐流失
    public const uint 火伤2082 = 2082u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤2083 = 2083u; //冰属性持续伤害，体力逐渐流失
    public const uint 裂伤2084 = 2084u; //风属性持续伤害，体力逐渐流失
    public const uint 污泥2085 = 2085u; //土属性持续伤害，体力逐渐流失
    public const uint 感电2086 = 2086u; //雷属性持续伤害，体力逐渐流失
    public const uint 水毒2087 = 2087u; //水属性持续伤害，体力逐渐流失
    public const uint 出血2088 = 2088u; //无属性持续伤害，体力逐渐流失
    public const uint 中毒2089 = 2089u; //毒素会导致体力逐渐减少
    public const uint 打伤2103 = 2103u; //打击属性持续伤害，体力逐渐流失
    public const uint 中毒2104 = 2104u; //毒素会导致体力逐渐减少
    public const uint 毒雾2192 = 2192u; //无属性持续伤害，体力逐渐流失，且受到的伤害有所增加
    public const uint 火伤2194 = 2194u; //火属性持续伤害，体力逐渐流失
    public const uint 火伤2199 = 2199u; //火属性持续伤害，体力逐渐流失
    public const uint 感电2200 = 2200u; //雷属性持续伤害，体力逐渐流失
    public const uint 拘束2285 = 2285u; //无法做出任何行动，体力逐渐流失
    public const uint 加速模式2294 = 2294u; //机体性能上升，战技的咏唱时间和复唱时间有所缩短，移动速度上升，体力逐渐减少
    public const uint 以太屏障2295 = 2295u; //展开以太屏障，受到的伤害降低，能量逐渐减少
    public const uint 舍身境地2327 = 2327u; //以体力逐渐流失为代价提高攻击所造成的伤害
    public const uint 暗之荆棘2386 = 2386u; //被暗之荆棘缠住，体力会逐渐减少
    public const uint 出血2389 = 2389u; //无属性持续伤害，体力逐渐流失
    public const uint 出血2399 = 2399u; //无属性持续伤害，体力逐渐流失
    public const uint 火伤2401 = 2401u; //火属性持续伤害，体力逐渐流失
    public const uint 出血2432 = 2432u; //无属性持续伤害，体力逐渐流失
    public const uint 失传耀星2440 = 2440u; //体力逐渐减少
    public const uint 芥末灼烧2499 = 2499u; //火属性持续伤害，体力逐渐流失
    public const uint 均衡注药2614 = 2614u; //体力逐渐减少
    public const uint 均衡注药II2615 = 2615u; //体力逐渐减少
    public const uint 均衡注药III2616 = 2616u; //体力逐渐减少
    public const uint 出血2636 = 2636u; //无属性持续伤害，体力逐渐流失
    public const uint 感电2646 = 2646u; //雷属性持续伤害，体力逐渐流失
    public const uint 莱韦耶勒尔注药III2650 = 2650u; //体力逐渐减少
    public const uint 樱花缭乱2719 = 2719u; //体力逐渐减少
    public const uint 英勇之剑2721 = 2721u; //体力逐渐减少
    public const uint 苦痛2726 = 2726u; //被苦痛吞噬，体力逐渐流失
    public const uint 猛毒2735 = 2735u; //毒素会导致体力逐渐减少
    public const uint 水毒2774 = 2774u; //水属性持续伤害，体力逐渐流失
    public const uint 光明之环2795 = 2795u; //体力逐渐减少
    public const uint 灭杀的誓言2896 = 2896u; //邪龙尼德霍格在失去了心爱的诗龙时立下了灭杀的誓言，被选为了灭杀的目标 自身发动的体力恢复效果降低，并逐渐流失体力  此外效果结束时会对周围造成痛苦
    public const uint 持续伤害_暗2900 = 2900u; //受到暗属性的持续伤害，体力逐渐流失
    public const uint 持续伤害_光2901 = 2901u; //受到光属性的持续伤害，体力逐渐流失
    public const uint 切伤2913 = 2913u; //斩击属性持续伤害，体力逐渐流失
    public const uint 刺伤2914 = 2914u; //突刺属性持续伤害，体力逐渐流失
    public const uint 打伤2915 = 2915u; //打击属性持续伤害，体力逐渐流失
    public const uint 火伤2916 = 2916u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤2917 = 2917u; //冰属性持续伤害，体力逐渐流失
    public const uint 裂伤2918 = 2918u; //风属性持续伤害，体力逐渐流失
    public const uint 污泥2919 = 2919u; //土属性持续伤害，体力逐渐流失
    public const uint 感电2920 = 2920u; //雷属性持续伤害，体力逐渐流失
    public const uint 水毒2921 = 2921u; //水属性持续伤害，体力逐渐流失
    public const uint 出血2922 = 2922u; //无属性持续伤害，体力逐渐流失
    public const uint 持续伤害2935 = 2935u; //无属性持续伤害，体力逐渐流失
    public const uint 切伤2942 = 2942u; //斩击属性持续伤害，体力逐渐流失
    public const uint 刺伤2943 = 2943u; //突刺属性持续伤害，体力逐渐流失
    public const uint 打伤2944 = 2944u; //打击属性持续伤害，体力逐渐流失
    public const uint 火伤2945 = 2945u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤2946 = 2946u; //冰属性持续伤害，体力逐渐流失
    public const uint 裂伤2947 = 2947u; //风属性持续伤害，体力逐渐流失
    public const uint 污泥2948 = 2948u; //土属性持续伤害，体力逐渐流失
    public const uint 感电2949 = 2949u; //雷属性持续伤害，体力逐渐流失
    public const uint 水毒2950 = 2950u; //水属性持续伤害，体力逐渐流失
    public const uint 出血2951 = 2951u; //无属性持续伤害，体力逐渐流失
    public const uint 与风共歌的蛇2962 = 2962u; //身中剧毒，体力会逐渐减少
    public const uint 与风共舞的狼2963 = 2963u; //风属性持续伤害，体力逐渐流失
    public const uint 持续伤害2967 = 2967u; //无属性持续伤害，体力逐渐流失
    public const uint 颅骨切开2968 = 2968u; //因颅骨切开精神错乱攻击同伴 同时因肉体活性异常，体力逐渐流失
    public const uint 绝望的锁链2993 = 2993u; //被绝望的锁链拴住，体力逐渐流失
    public const uint 猛毒3000 = 3000u; //毒素会导致体力逐渐减少
    public const uint 决死的忠义3008 = 3008u; //在忠义之心的驱使下，勉强站立起来 体力逐渐流失
    public const uint 大地腐秽3038 = 3038u; //体力逐渐减少
    public const uint 切伤3059 = 3059u; //���击属性持续伤害，体力逐渐流失
    public const uint 切伤3060 = 3060u; //斩击属性持续伤害，体力逐渐流失
    public const uint 刺伤3061 = 3061u; //突刺属性持续伤害，体力逐渐流失
    public const uint 刺伤3062 = 3062u; //突刺属性持续伤害，体力逐渐流失
    public const uint 打伤3063 = 3063u; //打击属性持续伤害，体力逐渐流失
    public const uint 打伤3064 = 3064u; //打击属性持续伤害，体力逐渐流失
    public const uint 火伤3065 = 3065u; //火属性持续伤害，体力逐渐流失
    public const uint 火伤3066 = 3066u; //火属性持续伤害，体力逐渐流失
    public const uint 冻伤3067 = 3067u; //冰属性持续伤害，体力逐渐流失
    public const uint 冻伤3068 = 3068u; //冰属性持续伤害，体力逐渐流失
    public const uint 裂伤3069 = 3069u; //风属性持续伤害，体力逐渐流失
    public const uint 裂伤3070 = 3070u; //风属性持续伤害，体力逐渐流失
    public const uint 污泥3071 = 3071u; //土属性持续伤害，体力逐渐流失
    public const uint 污泥3072 = 3072u; //土属性持续伤害，体力逐渐流失
    public const uint 感电3073 = 3073u; //雷属性持续伤害，体力逐渐流失
    public const uint 感电3074 = 3074u; //雷属性持续伤害，体力逐渐流失
    public const uint 水毒3075 = 3075u; //水属性持续伤害，体力逐渐流失
    public const uint 水毒3076 = 3076u; //水属性持续伤害，体力逐渐流失
    public const uint 出血3077 = 3077u; //无属性持续伤害，体力逐渐流失
    public const uint 出血3078 = 3078u; //无属性持续伤害，体力逐渐流失
    public const uint 土遁之术_腐化3079 = 3079u; //体力逐渐减少
    public const uint 狂欢3080 = 3080u; //陷入狂欢，抑制不住地想要跳舞，体力逐渐减少
    public const uint 猛毒3081 = 3081u; //毒素会导致体力逐渐减少
    public const uint 猛毒3082 = 3082u; //毒素会导致体力逐渐减少
    public const uint 蛊毒法3089 = 3089u; //体力逐渐减少
    public const uint 均衡注药III3108 = 3108u; //体力逐渐减少
    public const uint 痛苦3120 = 3120u; //体力逐渐减少
    public const uint 劫火灭却之术3184 = 3184u; //体力逐渐减少
    public const uint 火伤3218 = 3218u; //体力逐渐减少
    public const uint 螺旋气流中3227 = 3227u; //体力逐渐减少
    public const uint 天启3232 = 3232u; //体力逐渐减少
    public const uint 魔回刺3237 = 3237u; //体力逐渐减少
    public const uint 魔交击斩3238 = 3238u; //体力逐渐减少
    public const uint 魔连攻3239 = 3239u; //体力逐渐减少
    public const uint 剧毒3261 = 3261u; //身中剧毒，自身所受的治疗魔法效果降低，体力逐渐减少
    public const uint 火伤3266 = 3266u; //火属性持续伤害，体力逐渐流失
    public const uint 火伤3267 = 3267u; //火属性持续伤害，体力逐渐流失
    public const uint 咒发束缚3284 = 3284u; //被巴尔巴莉希娅的咒发所束缚，移动速度降低，体力逐渐减少 和相连的对象距离过远时将会受到吸引效果，同时附加持续伤害状态
    public const uint 冻结3287 = 3287u; //身体被冻结无法行动，体力逐渐流失
    public const uint 咒毒之锁3294 = 3294u; //被锁链拴住，体力逐渐流失
    public const uint 火精的概念3340 = 3340u; //生成火精的概念，效果时间结束前体力逐渐减少
    public const uint 有毒生物的概念3341 = 3341u; //生成有毒生物的概念，效果时间结束前体力逐渐减少
    public const uint 草木生物的概念3342 = 3342u; //生成草木生物的概念，效果时间结束前体力逐渐减少
    public const uint 持续伤害3359 = 3359u; //无属性持续伤害，体力逐渐流失
    public const uint 中毒3390 = 3390u; //毒素会导致体力逐渐减少
    public const uint 中毒3399 = 3399u; //毒素会导致体力逐渐减少
    public const uint 猛毒3407 = 3407u; //毒素会导致体力逐渐减少
    public const uint 术式记录3412 = 3412u; //被打上了赫淮斯托斯的术式，体力逐渐减少 受到强制咏唱所发动的特定技能时会发动术式崩坏
    public const uint 中毒3462 = 3462u; //毒素会导致体力逐渐减少
    public const uint 冻结3519 = 3519u; //身体被冻结无法行动，体力逐渐流失
    public const uint 打伤3521 = 3521u; //打击属性持续伤害，体力逐渐流失
    public const uint 火伤3537 = 3537u; //火属性持续伤害，体力逐渐流失
    public const uint 心神风息陷阱_止步3547 = 3547u; //受到粘丝的影响无法做出任何行动，体力逐渐流失。随时间经过增加档数，积攒到5档之后会陷入无法战斗状态
    public const uint 心神风息陷阱_加重3548 = 3548u; //受到粘丝的影响移动速度降低，体力逐渐流失。积攒到4档之后会变为剧毒
    public const uint 失调的烙印3559 = 3559u; //以太平衡发生紊乱，对部分攻击的耐性有所降低。体力逐渐流失
    public const uint 苦痛锁链3587 = 3587u; //被锁链拴住，体力逐渐流失
    public const uint 泥污3636 = 3636u; //土属性持续伤害，体力逐渐流失
    public const uint 必灭之炎3643 = 3643u; //受到火属性伤害，体力逐渐流失。该火焰在将目标彻底烧成灰烬前不会消失
    public const uint 剧毒3692 = 3692u; //身中剧毒，自身所受的体力恢复效果降低，体力逐渐减少
    public const uint 抓捕3697 = 3697u; //被抓住，无法行动，体力逐渐减少
    public const uint 裂伤3705 = 3705u; //风属性持续伤害，体力逐渐流失
    public const uint 裂伤3706 = 3706u; //风属性持续伤害，体力逐渐流失
    public const uint 魔法吐息3712 = 3712u; //无属性持续伤害，体力逐渐流失
    public const uint 热浪3754 = 3754u; //体力逐渐减少
    public const uint 宇宙大反弹3761 = 3761u; //无属性持续伤害，体力逐渐流失
    public const uint 黑暗咒链3767 = 3767u; //被锁链拴住，体力逐渐流失
    public const uint 水毒3777 = 3777u; //水属性持续伤害，体力逐渐流失
    public const uint 水毒3778 = 3778u; //水属性持续伤害，体力逐渐流失
    public const uint 感电3779 = 3779u; //雷属性持续伤害，体力逐渐流失
    public const uint 持续伤害3795 = 3795u; //无属性持续伤害，体力逐渐流失
    public const uint 水毒3797 = 3797u; //水属性持续伤害，体力逐渐流失
    public const uint 水毒3798 = 3798u; //水属性持续伤害，体力逐渐流失

    public static readonly List<uint> 流血BUFF =
    [
        中毒18,
        猛毒19,
        秘孔拳106,
        樱花怒放118,
        二重刺119,
        毒咬箭124,
        风蚀箭129,
        闪雷161,
        震雷162,
        暴雷163,
        毒菌179,
        瘴气180,
        瘴疠188,
        猛毒菌189,
        裂伤235,
        陆行鸟猛啄236,
        碎骨打244,
        破碎拳246,
        厄运流转248,
        火伤250,
        切伤264,
        刺伤265,
        打伤266,
        火伤267,
        冻伤268,
        裂伤269,
        污泥270,
        感电271,
        水毒272,
        出血273,
        中毒275,
        水毒283,
        火伤284,
        冻伤285,
        裂伤286,
        污泥287,
        感电288,
        水毒289,
        黄金沼295,
        地狱之火炎314,
        出血320,
        元灵之怒324,
        火焰流335,
        出血339,
        出血343,
        神枪昆古尼尔的魔力352,
        抓捕401,
        捕食421,
        荆棘436,
        荆棘丛生445,
        亚拉戈蛇毒453,
        毒墨484,
        水毒485,
        冻结487,
        无双旋492,
        火伤503,
        影牙508,
        滚雷515,
        火伤530,
        水毒531,
        裂伤532,
        感电533,
        污泥534,
        冻伤535,
        中毒559,
        中毒560,
        粘液569,
        魔科学粒子α580,
        魔科学粒子β581,
        冻伤605,
        抓捕609,
        火伤619,
        切伤624,
        出血642,
        出血643,
        消化中645,
        毒雾659,
        感电666,
        剧毒677,
        中毒686,
        湍流716,
        沥血剑725,
        灾变741,
        烈焰链769,
        暴风798,
        中毒801,
        粘液809,
        烧灼838,
        炽灼843,
        铅弹854,
        出血940,
        抓捕961,
        持续伤害979,
        出血毒1004,
        剧毒1011,
        剧毒1046,
        消化粘液1073,
        出血1074,
        闪电链1077,
        诅咒1087,
        刺伤1117,
        双刃剑1145,
        影之脚镣1147,
        冻结1150,
        无尽痛苦刻印1159,
        弱化试剂1160,
        无尽痛苦刻印1162,
        烈毒咬箭1200,
        狂风蚀箭1201,
        霹雷1210,
        剧毒菌1214,
        瘴暍1215,
        彼岸花1228,
        冻结1254,
        抓捕1287,
        感电1328,
        生命吸收1377,
        至高无上1379,
        污泥1386,
        神龙诅咒1419,
        刺伤1466,
        污泥1474,
        连接1478,
        震荡1498,
        猛毒1507,
        裂伤1508,
        打伤1519,
        诅咒之炎1527,
        火伤1577,
        出血1714,
        裂伤1723,
        水毒1736,
        冻结1758,
        紫阳花1779,
        火伤1787,
        冻伤1788,
        影之脚镣1790,
        切伤1813,
        音速破1837,
        弓形冲波1838,
        毒菌冲击1866,
        天辉1871,
        焚灼1881,
        蛊毒法1895,
        感电1899,
        体温下降1940,
        隆卡咒毒1941,
        毒菌冲击2019,
        酸咬箭2073,
        切伤2079,
        刺伤2080,
        打伤2081,
        火伤2082,
        冻伤2083,
        裂伤2084,
        污泥2085,
        感电2086,
        水毒2087,
        出血2088,
        中毒2089,
        打伤2103,
        中毒2104,
        毒雾2192,
        火伤2194,
        火伤2199,
        感电2200,
        拘束2285,
        加速模式2294,
        以太屏障2295,
        舍身境地2327,
        暗之荆棘2386,
        出血2389,
        出血2399,
        火伤2401,
        出血2432,
        失传耀星2440,
        芥末灼烧2499,
        均衡注药2614,
        均衡注药II2615,
        均衡注药III2616,
        出血2636,
        感电2646,
        莱韦耶勒尔注药III2650,
        樱花缭乱2719,
        英勇之剑2721,
        苦痛2726,
        猛毒2735,
        水毒2774,
        光明之环2795,
        灭杀的誓言2896,
        持续伤害_暗2900,
        持续伤害_光2901,
        切伤2913,
        刺伤2914,
        打伤2915,
        火伤2916,
        冻伤2917,
        裂伤2918,
        污泥2919,
        感电2920,
        水毒2921,
        出血2922,
        持续伤害2935,
        切伤2942,
        刺伤2943,
        打伤2944,
        火伤2945,
        冻伤2946,
        裂伤2947,
        污泥2948,
        感电2949,
        水毒2950,
        出血2951,
        与风共歌的蛇2962,
        与风共舞的狼2963,
        持续伤害2967,
        颅骨切开2968,
        绝望的锁链2993,
        猛毒3000,
        决死的忠义3008,
        大地腐秽3038,
        切伤3059,
        切伤3060,
        刺伤3061,
        刺伤3062,
        打伤3063,
        打伤3064,
        火伤3065,
        火伤3066,
        冻伤3067,
        冻伤3068,
        裂伤3069,
        裂伤3070,
        污泥3071,
        污泥3072,
        感电3073,
        感电3074,
        水毒3075,
        水毒3076,
        出血3077,
        出血3078,
        土遁之术_腐化3079,
        狂欢3080,
        猛毒3081,
        猛毒3082,
        蛊毒法3089,
        均衡注药III3108,
        痛苦3120,
        劫火灭却之术3184,
        火伤3218,
        螺旋气流中3227,
        天启3232,
        魔回刺3237,
        魔交击斩3238,
        魔连攻3239,
        剧毒3261,
        火伤3266,
        火伤3267,
        咒发束缚3284,
        冻结3287,
        咒毒之锁3294,
        火精的概念3340,
        有毒生物的概念3341,
        草木生物的概念3342,
        持续伤害3359,
        中毒3390,
        中毒3399,
        猛毒3407,
        术式记录3412,
        中毒3462,
        冻结3519,
        打伤3521,
        火伤3537,
        心神风息陷阱_止步3547,
        心神风息陷阱_加重3548,
        失调的烙印3559,
        苦痛锁链3587,
        泥污3636,
        必灭之炎3643,
        剧毒3692,
        抓捕3697,
        裂伤3705,
        裂伤3706,
        魔法吐息3712,
        热浪3754,
        宇宙大反弹3761,
        黑暗咒链3767,
        水毒3777,
        水毒3778,
        感电3779,
        持续伤害3795,
        水毒3797,
        水毒3798
    ];


    /** 异常 **/
    public const uint 渐渐石化 = 1628u; //不断石化，体力完全恢复时效果解除


    /** 防击退类 **/
    public const uint 钢铁意志_75 = 75u; //除特定攻击之外其他所有击退与吸引无效

    public const uint 钢铁意志_712 = 712u; //击退与吸引无效
    public const uint 击退无效_1096 = 1096u; //击退与吸引效果无效
    public const uint 击退无效_1355 = 1355u; //击退与吸引效果无效
    public const uint 击退无效_1512 = 1512u; //击退与吸引效果无效
    public const uint 隔离罩 = 1983u; //不受眩晕、睡眠、止步、加重、沉默、击退、吸引的效果影响
    public const uint 荆棘 = 436u; //被荆棘缠住了双脚，移动速度降低，并且体力会逐渐减少 但该状态下敌人攻击中的吸引和击退效果会被无效化
    public const uint 反推 = 870u; //受到物理攻击时给予对方反击伤害，并且在受到击退效果时，令击退无效的同时击退并击倒对方
    public const uint 亲疏自行_1209 = 1209u; //除特定攻击之外击退与吸引无效 受到攻击时对攻击者附加减速状态
    public const uint 亲疏自行_1984 = 1984u; //受到攻击的伤害减少，并且可以防御下次的眩晕、睡眠、止步、加重、减速、沉默、击退、吸引的效果
    public const uint 失传坚壁 = 2345u; //令自身所受到的伤害减少，同时除特定攻击之外其他所有击退与吸引无效
    public const uint 原初的觉悟 = 2663u; //不会受到眩晕、睡眠、止步、加重和除特定攻击之外其他所有击退、吸引的影响
    public const uint 原初的解放 = 1303u; //移动速度提高，不受能通过净化解除的异常状态以及击退、吸引的效果影响，并且蛮荒崩裂与混沌旋风的威力提高
    public const uint 防御 = 3054u; //受到攻击的伤害减少，同时不受能通过净化解除的异常状态以及击退、吸引的效果影响持续时间内移动速度降低
    public const uint 沉稳咏唱 = 160u; //下次魔法咏唱不会被攻击打断

    public static List<uint> 防击退Buff =
    [
        钢铁意志_75,
        钢铁意志_712,
        击退无效_1096,
        击退无效_1355,
        击退无效_1512,
        隔离罩,
        荆棘,
        反推,
        亲疏自行_1209,
        亲疏自行_1984,
        失传坚壁,
        原初的觉悟,
        原初的解放,
        防御,
        沉稳咏唱
    ];
}
