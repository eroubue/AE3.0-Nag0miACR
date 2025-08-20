using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;
using Millusion.CharacterRefined;
using Millusion.Define;

namespace Millusion.Helper;

public static class MsTargetHelper
{
    /// <summary>
    ///     目标是否可以治疗
    /// </summary>
    /// <param name="battleChara">目标</param>
    /// <returns></returns>
    public static bool CanHeal(this IBattleChara battleChara)
    {
        if (battleChara == null) return false;

        if (battleChara.CurrentHp == 0) return false;

        if (battleChara.CurrentHpPercent() >= 1) return false;

        if (battleChara.NotInvulnerable()) return false;

        if (battleChara.HasAura(AurasDefine.LivingDead)) return false;

        if (battleChara.HasAura(AurasDefine.WalkingDead)) return false;

        return true;
    }

    public static bool HasSwiftcastAuras(this IBattleChara battleChara)
    {
        return battleChara.HasAura(AurasDefine.Swiftcast) || battleChara.HasAura(AurasDefine.Lightspeed);
    }

    /// <summary>
    ///     获取单位当前治疗缺口，及需要多少威力治疗量
    /// </summary>
    /// <param name="battleChara"></param>
    /// <param name="maxHpPercent">到此百分比需要的治疗量，默认满血</param>
    /// <param name="isCurrentHp">是否比较当前生命值,false从0计算</param>
    /// <returns></returns>
    public static int NeedHealWithPower(this IBattleChara battleChara, float maxHpPercent = 1f, bool isCurrentHp = true)
    {
        var maxHp = battleChara.MaxHp * maxHpPercent;
        var currentHp = isCurrentHp ? battleChara.CurrentHp : 0;
        var missingHp = maxHp - currentHp;
        if (missingHp <= 0) return 0;
        var result = (int)Math.Floor((maxHp - currentHp) / CharacterStatus.AvgHeal * 100);

        return result;
    }

    /// <summary>
    ///     获取单位从空奶满需要的治疗量，及需要多少威力治疗量
    /// </summary>
    /// <param name="battleChara"></param>
    /// <param name="maxHpPercent">到此百分比需要的治疗量，默认满血</param>
    /// <returns></returns>
    public static int NeedMaxHealWithPower(this IBattleChara battleChara, float maxHpPercent = 1f)
    {
        return battleChara.NeedHealWithPower(1f, false);
    }


    public static bool CanRaise(this IBattleChara battleChara)
    {
        if (battleChara == null || !battleChara.IsValid()) return false;

        if (battleChara.CurrentHpPercent() > 0 || !battleChara.IsDead) return false;

        if (battleChara.HasAura(AurasDefine.Raise)) return false;

        return true;
    }
}