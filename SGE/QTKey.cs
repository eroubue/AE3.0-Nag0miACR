namespace Nagomi.SGE
{
    // 直接定义好 方便编码
    public static class QTKey
    {
        public const string 停手 = "停手";
        public const string 康复 = "康复";
        public const string DOT = "DOT";
        public const string AOE = "AOE";
        public const string 复活 = "复活";
        public const string 发炎 = "发炎";
        public const string 保留发炎 = "保留发炎";
        public const string 爆发 = "爆发";
        public const string 心神风息 = "心神风息";
        public const string 心关 = "心关";
        public const string 根素 = "根素";
        public const string 红豆 = "红豆";
        public const string 保留红豆 = "保留红豆";
        public const string 走位 = "走位不读条";
        

    }
    public static class QT
    {
        public static bool QTGET(string qtName) => SGERotationEntry.QT.GetQt(qtName);
        public static bool QTSET(string qtName, bool qtValue) => SGERotationEntry.QT.SetQt(qtName, qtValue);
    }
}