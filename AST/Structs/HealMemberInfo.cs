using Dalamud.Game.ClientState.Objects.Types;
using Millusion.Helper;

namespace Millusion.Structs;

public struct HealMemberInfo
{
    public IBattleChara Chara { get; set; }

    public int Priority { get; set; }

    public int NeedMaxHealWithPower => Chara.NeedMaxHealWithPower();
}