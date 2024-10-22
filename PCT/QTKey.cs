namespace Nagomi.PCT
{
    // 直接定义好 方便编码
    public static class QTKey
    {
        public const string 减色混合 = "减色混合";
        public const string AOE = "AOE";
        public const string 锤连击 = "锤连击";
        public const string CYM = "CYM";
        public const string sb = "保留黑豆";
        public const string RGB = "RGB";
        public const string 动物彩绘 = "动物彩绘";
        public const string 武器彩绘 = "武器彩绘";
        public const string 风景彩绘 = "风景彩绘";
        public const string 动物构想 = "动物构想";
        public const string 武器构想 = "武器构想";
        public const string 风景构想 = "风景构想";
        public const string 莫古力激流 = "莫古力激流";
        public const string 马蒂恩惩罚 = "马蒂恩惩罚";
        public const string 保留1层锤 = "保留1层锤";
    }
    public static class QT
    {
        public static bool QTGET(string qtName) => PictomancerRotationEntry.QT.GetQt(qtName);
        public static bool QTSET(string qtName, bool qtValue) => PictomancerRotationEntry.QT.SetQt(qtName, qtValue);
    }
}