using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.Enum;
using Millusion.Helper;

namespace Millusion.Interface;

public abstract class MsSpell
{
    public abstract uint Id { get; }

    public abstract SlotMode Mode { get; }

    public abstract SpellEffectType EffectType { get; }

    public abstract uint HealPower { get; }

    public virtual Spell GetSpell(IBattleChara target)
    {
        return new Spell(Id, target);
    }

    public bool IsReady => Id.IsUnlockWithCD();

    public bool CanUseToTarget(IBattleChara target)
    {
        return Id.CanCastToTarget(target);
    }
}