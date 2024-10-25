namespace Nagomi.GNB
{
    // 直接定义好 方便编码
    public static class QTKey
    {
        public const string 停手 = "停手";
        public const string 爆发 = "爆发";
        public const string 突进 = "突进";
        public const string 无情 = "无情";
        public const string 子弹连 = "子弹连";
        public const string 倾泻爆发 = "倾泻爆发";
        public const string 领域 = "领域";
        public const string 弓形 = "弓形";
        public const string AOE = "AOE";
        public const string dot = "dot";
        public const string 无情后半 = "无情后半";
        

    }
    public static class QT
    {
        public static bool QTGET(string qtName) => GNBRotationEntry.QT.GetQt(qtName);
        public static bool QTSET(string qtName, bool qtValue) => GNBRotationEntry.QT.SetQt(qtName, qtValue);
    }
}