namespace Nagomi.GNB.utils;

public class GNB小帮手
{
    public static void 互斥零弹120(bool isSet)
    {
        if (!isSet)
            return;
        QT.QTSET("二弹120", false);
    }

    public static void 互斥二弹120(bool isSet)
    {
        if (!isSet)
            return;
        QT.QTSET("零弹120", false);
    }
}