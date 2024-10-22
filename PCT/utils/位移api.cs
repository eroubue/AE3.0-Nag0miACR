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

    public static Vector3 向量位移(Vector3 position, float facingRadians, float distance)
    {
        // 计算 x-z 平面上移动的距离分量
        float dx = (float)(Math.Sin(facingRadians) * distance);
        float dz = (float)(Math.Cos(facingRadians) * distance);

        return new Vector3(position.X + dx, position.Y, position.Z + dz);
    }
}

