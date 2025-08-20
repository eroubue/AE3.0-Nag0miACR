using AEAssist;
using AEAssist.MemoryApi;
using AEAssist.Helper;

namespace Nagomi.Utils
{
    /// <summary>
    /// 地图相关的工具类，提供安全的地图信息获取方法
    /// </summary>
    public static class MapUtils
    {
        /// <summary>
        /// 安全地获取当前地图基础名称
        /// </summary>
        /// <returns>地图基础名称，如果获取失败返回null</returns>
        public static string GetMapBaseNameSafely()
        {
            try
            {
                var zoneInfo = Core.Resolve<MemApiZoneInfo>();
                if (zoneInfo == null)
                {
                    LogHelper.PrintError("无法获取区域信息API");
                    return null;
                }

                var currZoneInfo = zoneInfo.GetCurrZoneInfo();
                // ZoneInfo是值类型，检查其MapBaseName是否为空来判断是否有效
                if (string.IsNullOrEmpty(currZoneInfo.MapBaseName))
                {
                    LogHelper.PrintError("无法获取当前区域信息或地图基础名称为空");
                    return null;
                }

                return currZoneInfo.MapBaseName;
            }
            catch (Exception ex)
            {
                LogHelper.PrintError($"获取地图基础名称时发生异常: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 安全地获取当前区域ID
        /// </summary>
        /// <returns>区域ID，如果获取失败返回0</returns>
        public static uint GetCurrentTerritoryIdSafely()
        {
            try
            {
                var zoneInfo = Core.Resolve<MemApiZoneInfo>();
                if (zoneInfo == null)
                {
                    LogHelper.PrintError("无法获取区域信息API");
                    return 0;
                }

                return zoneInfo.GetCurrTerrId();
            }
            catch (Exception ex)
            {
                LogHelper.PrintError($"获取当前区域ID时发生异常: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 安全地获取当前天气ID
        /// </summary>
        /// <returns>天气ID，如果获取失败返回0</returns>
        public static byte GetCurrentWeatherIdSafely()
        {
            try
            {
                var zoneInfo = Core.Resolve<MemApiZoneInfo>();
                if (zoneInfo == null)
                {
                    LogHelper.PrintError("无法获取区域信息API");
                    return 0;
                }

                return zoneInfo.GetWeatherId();
            }
            catch (Exception ex)
            {
                LogHelper.PrintError($"获取当前天气ID时发生异常: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 检查是否能够安全地获取地图信息
        /// </summary>
        /// <returns>如果能够获取地图信息返回true，否则返回false</returns>
        public static bool CanGetMapInfo()
        {
            var mapBaseName = GetMapBaseNameSafely();
            return !string.IsNullOrEmpty(mapBaseName);
        }
    }
}
