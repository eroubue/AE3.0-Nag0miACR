using AEAssist.CombatRoutine.Module;
using Millusion.ACR.Astrologian.SlotResolvers.Ability;
using Millusion.ACR.Astrologian.SlotResolvers.GCD;
using Millusion.Interface;

namespace Millusion.ACR.Astrologian.SlotResolvers;

public static class AST_SlotResolvers
{
    public static List<BaseSlotResolver> SlotResolvers { get; } =
    [
        // 复活
        AST_GCD_Ascend.Instance,

        // 其他
        AST_Ability_Lightspeed.Instance,
        AST_Ability_Play1.Instance,
        AST_Ability_Divination.Instance,
        AST_Ability_MinorArcana.Instance,
        AST_Ability_Draw.Instance,
        AST_Ability_EarthlyStar.Instance,

        // 群体治疗
        AST_Ability_StellarDetonation.Instance,
        AST_Ability_CelestialOpposition.Instance,
        AST_Ability_CollectiveUnconscious.Instance,
        AST_Ability_Horoscope.Instance,

        // 单体治疗
        AST_Ability_EssentialDignity.Instance,
        AST_Ability_CelestialIntersection.Instance,
        AST_Ability_Exaltation.Instance,
        AST_Ability_Synastry.Instance,
        AST_Ability_Play3.Instance,
        AST_Ability_Play2.Instance,

        // buff
        AST_Ability_NeutralSect.Instance,
        AST_Ability_SunSign.Instance,

        // 范围伤害
        AST_Ability_Oracle.Instance,

        // 醒梦，康复
        AST_Ability_LucidDreaming.Instance,
        AST_GCD_Esuna.Instance,

        // GCD治疗
        AST_GCD_Macrocosmos.Instance,
        AST_GCD_AspectedHelios.Instance,
        AST_GCD_Helios.Instance,
        AST_GCD_AspectedBenefic.Instance,
        AST_GCD_Benefic.Instance,

        // GCD伤害
        AST_GCD_Gravity.Instance,
        AST_GCD_Combust.Instance,
        AST_GCD_Malefic.Instance
    ];

    private static List<SlotResolverData> PreSlotResolverData { get; } = [new(new PreSlotResolver(), SlotMode.Always)];

    public static List<SlotResolverData> SlotResolverDatas { get; } = PreSlotResolverData
        .Concat(SlotResolvers.Select(r => new SlotResolverData(r, r.Mode))).ToList();

    public static void Update()
    {
        foreach (var slotResolver in SlotResolvers) slotResolver.Update();
    }

    public static void Reset()
    {
        foreach (var slotResolver in SlotResolvers) slotResolver.Reset();
    }

    private class PreSlotResolver : ISlotResolver
    {
        public int Check()
        {
            Update();
            return -1;
        }

        public void Build(Slot slot)
        {
        }
    }
}