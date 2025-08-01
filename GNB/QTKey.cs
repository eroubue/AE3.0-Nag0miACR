namespace Nagomi.GNB
{
    // 直接定义好 方便编码
    public static class QTKey
    {
        public const string 停手 = "停手";
        public const string 爆发 = "爆发";
        public const string 突进起手 = "突进起手";
        public const string 无情 = "无情";
        public const string 子弹连 = "子弹连";
        public const string 倾泻爆发 = "倾泻爆发";
        public const string 领域 = "领域";
        public const string 弓形 = "弓形";
        public const string AOE = "AOE";
        public const string dot = "dot";
        public const string 音速破 = "音速破";
        public const string 无情不延后 = "无情不延后";
        public const string 血壤 = "血壤";
        public const string 爆发击 = "爆发击";
        public const string 狮心连 = "狮心连";
        public const string 倍攻 = "倍攻";
        public const string 二弹 = "二弹120";
        public const string 零弹 = "零弹120";
        public const string 闪雷弹 = "闪雷弹";
        public const string 命运之环 = "命运之环";
        public const string 仅使用爆发击卸除子弹 = "仅使用爆发击卸除子弹";
        public const string 小于3目标时不用弓形 = "小于3目标时不用弓形";
        public const string 弓形冲波允许错开无情 = "弓形冲波允许错开无情";
        
        
        
        

    }
    public static class QT
    {
        public static bool QTGET(string qtName) => GNBRotationEntry.QT.GetQt(qtName);
        public static bool QTSET(string qtName, bool qtValue) => GNBRotationEntry.QT.SetQt(qtName, qtValue);
    }
}