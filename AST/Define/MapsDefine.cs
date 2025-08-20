using System.Numerics;

namespace Millusion.Define;

public static class MapsDefine
{
    public static bool TryGetBossPlaceCenter(uint mapId, out Vector3[] centers)
    {
        return BossPlaceCenterDictionary.TryGetValue(mapId, out centers);
    }

    private static Dictionary<uint, Vector3[]> BossPlaceCenterDictionary { get; } = new()
    {
        { 719, [new Vector3(-6, 164, 471)] }
    };
}