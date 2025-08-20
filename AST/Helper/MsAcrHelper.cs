using System.Numerics;
using AEAssist;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using ECommons.DalamudServices;
using Millusion.Define;

namespace Millusion.Helper;

public static class MsAcrHelper
{
    public static Vector3 MapCenter(Vector3 fallback, bool force = false)
    {
        if (Svc.ClientState.LocalPlayer == null)
            return fallback;
        var mapId = Svc.ClientState.MapId;
        if (MapsDefine.TryGetBossPlaceCenter(mapId, out var centers))
            foreach (var center in centers)
            {
                // var center3 = new Vector3(center.X, Svc.ClientState.LocalPlayer.Position.Y, center.Y);
                if (force) return center;
                if (Vector3.Distance(center, Svc.ClientState.LocalPlayer.Position) < 30.0) return center;
            }

        var fromTerritoryTypeId =
            MemApiZoneInfo.GetMapInfoFromTerritoryTypeId(Svc.ClientState.TerritoryType);
        var index = 0;
        if (index >= fromTerritoryTypeId.Length)
            return fallback;
        var mapCoordinates = fromTerritoryTypeId[index].GetMapCoordinates(new Vector2(0.5f, 0.5f) * 2048f);
        var vector3 = new Vector3(mapCoordinates.X, Svc.ClientState.LocalPlayer.Position.Y, mapCoordinates.Y);
        if (force) return vector3;
        return Vector3.Distance(vector3, Svc.ClientState.LocalPlayer.Position) < 30.0 ? vector3 : fallback;
    }

    public static int DutyMembersNumber()
    {
        return Core.Resolve<MemApiDuty>().DutyMembersNumber();
    }

    public static bool CanUseOffGCD()
    {
        if (GCDHelper.CanUseOffGcd()) return true;

        return false;
    }

    public static uint GetCurrTerrId()
    {
        return Svc.ClientState == null ? 0U : (uint)Svc.ClientState.TerritoryType;
    }
}