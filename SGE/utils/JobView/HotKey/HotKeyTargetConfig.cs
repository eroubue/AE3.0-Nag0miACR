using AEAssist.CombatRoutine;
using Dalamud.Game.ClientState.Objects.Types;
using Nagomi;

// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Nagomi.SGE.utils.JobView.HotKey;

public static class HotKeyTargetConfig
{
    public static readonly Dictionary<int, HotKeyTarget> List = new()
    {
        {1,new HotKeyTarget("自己", SpellTargetType.Self)},
        {2,new HotKeyTarget("目标", SpellTargetType.Target)},
        {3,new HotKeyTarget("目标的目标", SpellTargetType.TargetTarget)},
        {4,new HotKeyTarget("小队列表1", SpellTargetType.Pm1)},
        {5,new HotKeyTarget("小队列表2", SpellTargetType.Pm2)},
        {6,new HotKeyTarget("小队列表3", SpellTargetType.Pm3)},
        {7,new HotKeyTarget("小队列表4", SpellTargetType.Pm4)},
        {8,new HotKeyTarget("小队列表5", SpellTargetType.Pm5)},
        {9,new HotKeyTarget("小队列表6", SpellTargetType.Pm6)},
        {10,new HotKeyTarget("小队列表7", SpellTargetType.Pm7)},
        {11,new HotKeyTarget("小队列表8", SpellTargetType.Pm8)},
        {12,new HotKeyTarget("距离最远目标", null,Helper.获取距离最远成员)},
        {13,new HotKeyTarget("血量最低成员", null,Helper.获取血量最低成员)},
        {14,new HotKeyTarget("血量最低T", null,Helper.获取最低血量T)},
        {15,new HotKeyTarget("死亡队员",null,Helper.没有复活状态的死亡队友)}
    };

}



public class HotKeyTarget(
    string? n,
    SpellTargetType? s = null,
    Func<IBattleChara?>? f = null,
    IBattleChara? c = null)
{
    public  string? Name = n;
    public  SpellTargetType? SpellTargetType = s;
    public  Func<IBattleChara?>? Func = f;
    public  IBattleChara? CharacterAgent = c;
}