namespace Nagomi.GNB.utils;

public class GNB小帮手
{
    public static void 互斥零弹120(bool isSet)
    {
        if (!isSet)
            return;
        QT.QTSET("二弹", false);
    }

    public static void 互斥二弹120(bool isSet)
    {
        if (!isSet)
            return;
        QT.QTSET("零弹", false);
    }
    public static void 延迟关闭落地无情()
    {
        var timer = new System.Timers.Timer(600); // 600毫秒
        timer.Elapsed += (sender, e) => 
        {
            QT.QTSET(QTKey.落地无情, false);
            ((System.Timers.Timer)sender).Stop();
            ((System.Timers.Timer)sender).Dispose();
        };
        timer.Start();
    }

}