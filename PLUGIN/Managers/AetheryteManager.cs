using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ChatCoordinates.Models;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;

namespace ChatCoordinates.Managers
{
    /// <summary>
    /// 管理艾瑟里特相关数据的类。
    /// </summary>
    public class AetheryteManager
    {
        /// <summary>
        /// 存储所有艾瑟里特详细信息的字典，键为地图ID，值为该地图上的艾瑟里特列表。
        /// </summary>
        private readonly Dictionary<uint, List<AetheryteDetail>> _aetherytes;

        /// <summary>
        /// Dalamud的数据管理服务接口。
        /// </summary>
        private readonly IDataManager _data;

        /// <summary>
        /// AetheryteManager的构造函数。
        /// </summary>
        /// <param name="data">Dalamud的数据管理服务接口。</param>
        public AetheryteManager(IDataManager data)
        {
            _data = data;
            _aetherytes = LoadAetherytes();
        }

        /// <summary>
        /// 获取相对于指定坐标最近的艾瑟里特。
        /// </summary>
        /// <param name="coordinate">指定的坐标。</param>
        /// <returns>最近的艾瑟里特详细信息，如果找不到则返回null。</returns>
        public AetheryteDetail? GetClosestAetheryte(Coordinate coordinate)
        {
            // 尝试从字典中获取与指定坐标相同地图ID的艾瑟里特列表
            if (!_aetherytes.TryGetValue(coordinate.TerritoryDetail!.MapId, out var aetherytes)) return null;

            // 使用Aggregate方法遍历艾瑟里特列表，找到距离指定坐标最近的艾瑟里特
            return aetherytes.Aggregate((min, x) =>
                x.Distance(coordinate) < min.Distance(coordinate) ? x : min);
        }

        /// <summary>
        /// 加载所有艾瑟里特的数据。
        /// </summary>
        /// <returns>包含所有艾瑟里特详细信息的字典。</returns>
        private Dictionary<uint, List<AetheryteDetail>> LoadAetherytes()
        {
            // 获取所有地图标记，并筛选出类型为3的标记（假设这表示艾瑟里特）
            var mapMarkers = _data.GetSubrowExcelSheet<MapMarker>()
                .SelectMany(m => m).Cast<MapMarker?>()
                .Where(m => m!.Value.DataType == 3).ToList();

            // 获取艾瑟里特数据表
            var aetheryteSheet = _data.GetExcelSheet<Aetheryte>();

            // 初始化艾瑟里特详细信息的字典
            var aetherytes = new Dictionary<uint, List<AetheryteDetail>>();

            // 遍历艾瑟里特数据表，加载每个艾瑟里特的详细信息
            foreach (var aetheryte in aetheryteSheet)
            {
                // 跳过无效的艾瑟里特数据
                if (aetheryte.RowId <= 0) continue;
                if (!aetheryte.IsAetheryte) continue;

                // 尝试找到与当前艾瑟里特关联的地图标记
                var marker = mapMarkers.FirstOrDefault(x => x!.Value.DataKey.RowId == aetheryte.RowId);
                if (marker == null) continue;

                // 如果当前艾瑟里特所在地图的ID不在字典中，则添加新的地图ID键和空的艾瑟里特列表值
                if (!aetherytes.ContainsKey(aetheryte.Map.Value.RowId))
                    aetherytes.Add(aetheryte.Map.Value.RowId, new List<AetheryteDetail>());

                // 向当前艾瑟里特所在地图的艾瑟里特列表中添加艾瑟里特详细信息
                aetherytes[aetheryte.Map.Value.RowId].Add(new AetheryteDetail
                {
                    Name = aetheryte.PlaceName.Value.Name.ToString(),
                    SizeFactor = aetheryte.Map.Value.SizeFactor,
                    RawCoordinates = new Vector2(marker.Value.X, marker.Value.Y)
                });
            }

            // 返回加载的艾瑟里特数据字典
            return aetherytes;
        }
    }
}
