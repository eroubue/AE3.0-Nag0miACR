using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Helper;

namespace Millusion.Interface;

public abstract class BaseSlotResolver : ISlotResolver
{
    public virtual string Name => SpellId.GetSpell().Name;

    public abstract uint SpellId { get; }

    public abstract SlotMode Mode { get; }

    public virtual SpellEffectType EffectType => SpellEffectType.Harm;


    /// <summary>
    /// 此威力只适用于治疗技能,且只计算直接治疗威力
    /// </summary>
    public virtual uint Power { get; } = 0;


    /// <summary>
    /// 检查结果, 大于等于0为通过
    /// </summary>
    public int CheckCode { get; private set; } = -9999;

    protected IBattleChara Target { get; set; }

    public IBattleChara SpellTarget =>
        Target ?? Core.Resolve<MemApiSpell>().GetSkillTarget(SpellId).GetTarget();

    public bool SpellReady { get; private set; }

    public int Check()
    {
        if (AST_View.UI.GetQt(AST_QT_Key.Stop)) return PreCheckCode.Stop;

        var spell = GetSpell();

        if (spell == null) return PreCheckCode.NotSpell;

        if (spell.RecentlyUsed(500)) return PreCheckCode.RecentlyUsed;

        return CheckCode;
    }

    public virtual void Build(Slot slot)
    {
        slot.Add(GetSpell());
    }

    protected abstract int RunCheck();


    protected virtual Spell GetSpell()
    {
        return Target != null && Target.IsValid() ? new Spell(SpellId, Target) : SpellId.GetSpell();
    }


    /// <summary>
    ///     构建一个技能，返回此技能
    /// </summary>
    /// <returns></returns>
    public void Update()
    {
        Reset();
        CheckCode = RunCheck();
        SpellReady = GetSpell().IsAllReady();
        if (CheckCode < 0) return;
        if (!SpellReady) CheckCode = PreCheckCode.NotReady;
    }

    public void Reset()
    {
        Target = null;
        CheckCode = PreCheckCode.Initial;
        SpellReady = false;
    }
}