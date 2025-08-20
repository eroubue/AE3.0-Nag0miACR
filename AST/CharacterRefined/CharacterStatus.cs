using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace Millusion.CharacterRefined;

public class CharacterStatus
{
    /// <summary>
    ///     每一百威力的平均治疗量
    /// </summary>
    public static double AvgHeal { get; private set; }

    public static unsafe void Update()
    {
        var uiState = UIState.Instance();
        var lvl = uiState->PlayerState.CurrentLevel;
        var levelModifier = LevelModifiers.LevelTable[lvl];
        var jobId = (JobId)uiState->PlayerState.CurrentClassJobId;

        var dh = Equations.CalcDh(uiState->PlayerState.Attributes[(int)Attributes.DirectHit], levelModifier);
        var det = Equations.CalcDet(uiState->PlayerState.Attributes[(int)Attributes.Determination], levelModifier);
        var critDmg =
            Equations.CalcCritDmg(uiState->PlayerState.Attributes[(int)Attributes.CriticalHit], levelModifier);
        var critRate =
            Equations.CalcCritRate(uiState->PlayerState.Attributes[(int)Attributes.CriticalHit], levelModifier);
        var ten = Equations.CalcTenacityDmg(uiState->PlayerState.Attributes[(int)Attributes.Tenacity], levelModifier);
        var (ilvlSync, ilvlSyncType) = IlvlSync.GetCurrentIlvlSync();

        var (avgHeal, normalHeal, critHeal) =
            Equations.CalcExpectedOutput(uiState, jobId, det, critDmg, critRate, dh, ten, levelModifier, ilvlSync,
                ilvlSyncType);

        AvgHeal = avgHeal;
    }
}