namespace Nagomi.SMN
{
    // 直接定义好 方便编码
    public static class QTKey
    {
        public const string AOE = "AOE";
        public const string 锤连击 = "锤连击";
        public const string CYM = "CYM";
        public const string sb = "保留黑豆";
        public const string RGB = "RGB";

    }
    public static class QT
    {
        public static bool QTGET(string qtName) => SMNRotationEntry.QT.GetQt(qtName);
        public static bool QTSET(string qtName, bool qtValue) => SMNRotationEntry.QT.SetQt(qtName, qtValue);
    }
}