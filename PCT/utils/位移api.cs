using System.CodeDom;
using Dalamud.Interface.Utility;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.Graphics.Kernel;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Common.Math;
using FFXIVClientStructs.FFXIV.Client.UI;
using CSFramework = FFXIVClientStructs.FFXIV.Client.System.Framework.Framework;
namespace Nagomi.PCT.utils;

public class CameraHelper
{
    private static unsafe RaptureAtkModule* RaptureAtkModule
    {
        get
        {
            
                return CSFramework.Instance()->GetUIModule()->GetRaptureAtkModule();
            
        }
    }

    internal static float GetCameraRotation()
    {
        unsafe
        {
            // 获取相机旋转角度（度数）
            var cameraRotation = RaptureAtkModule->AtkModule.AtkArrayDataHolder.NumberArrays[24]->IntArray[3];
            

            // 将角度转换为弧度，并确保范围在 [-π, π]
            var sign = Math.Sign(cameraRotation) == -1 ? -1 : 1;
            var rotation = (float)((Math.Abs(cameraRotation * (Math.PI / 180)) - Math.PI) * sign);

            return rotation;
        }
    }
 /// <summary>
/// 将世界坐标转换为屏幕坐标。
/// </summary>
/// <param name="worldPos">世界坐标位置。</param>
/// <param name="screenPos">输出的屏幕坐标位置。</param>
/// <param name="inView">位置是否在视图内。</param>
/// <returns>位置是否在摄像机前方。</returns>
/*
 public bool WorldToScreen(Vector3 worldPos, out Vector2 screenPos, out bool inView)
{
    // 获取当前视口位置、视图投影矩阵和游戏窗口大小
    var windowPos = ImGuiHelpers.MainViewport.Pos;
    var viewProjectionMatrix = Control.Instance()->ViewProjectionMatrix;
    var device = Device.Instance();
    float width = device->Width;
    float height = device->Height;

    // 将世界坐标转换为裁剪坐标
    var pCoords = Vector4.Transform(new Vector4(worldPos, 1.0f), viewProjectionMatrix);
    var inFront = pCoords.W > 0.0f;

    // 检查位置是否太接近摄像机
    if (Math.Abs(pCoords.W) < float.Epsilon)
    {
        screenPos = Vector2.Zero;
        inView = false;
        return false;
    }

    // 将裁剪坐标转换为标准化设备坐标
    pCoords *= MathF.Abs(1.0f / pCoords.W);
    screenPos = new Vector2(pCoords.X, pCoords.Y);

    // 将标准化设备坐标转换为屏幕坐标
    screenPos.X = (0.5f * width * (screenPos.X + 1f)) + windowPos.X;
    screenPos.Y = (0.5f * height * (1f - screenPos.Y)) + windowPos.Y;

    // 检查位置是否在视图内
    inView = inFront &&
             screenPos.X > windowPos.X && screenPos.X < windowPos.X + width &&
             screenPos.Y > windowPos.Y && screenPos.Y < windowPos.Y + height;

    return inFront;
}
*/

    public static Vector3 向量位移(Vector3 position, float facingRadians, float distance)
    {
        // 计算 x-z 平面上移动的距离分量
        float dx = (float)(Math.Sin(facingRadians) * distance);
        float dz = (float)(Math.Cos(facingRadians) * distance);

        return new Vector3(position.X + dx, position.Y, position.Z + dz);
    }
}

