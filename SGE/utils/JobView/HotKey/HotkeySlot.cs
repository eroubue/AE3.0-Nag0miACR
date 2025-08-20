using System.Numerics;

namespace Nagomi.SGE.utils.JobView.HotKey;

/// <summary>
/// 快捷键事件
/// </summary>
public interface IHotkeyResolver
{
    /// <summary>
    /// 画快捷键的显示. 只能画快捷键图片本身
    /// </summary>
    /// <param name="size">显示的总区域大小</param>
    void Draw(Vector2 size);

    /// <summary>
    /// 画快捷键显示的额外信息 (相当于新建了一个图层).拆分开来是因为前者需要统一加个ToolTip和点击处理
    /// </summary>
    /// <param name="size"></param>
    /// <param name="isActive">快捷键现在是否被激活(持续0.5s)</param>
    void DrawExternal(Vector2 size, bool isActive);

    /// <summary>
    /// 返回大于等于0说明这个快捷键事件可以触发
    /// </summary>
    /// <returns></returns>
    int Check();

    /// <summary>
    /// 执行当前的快捷键事件
    /// </summary>
    void Run();
}