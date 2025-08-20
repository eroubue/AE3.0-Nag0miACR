using Dalamud.Game.ClientState.Objects.Types;

namespace Millusion.Structs;

public struct DeathBattleCharaInfo
{
    public IBattleChara Chara { get; set; }

    public long DeathTime { get; set; }
}