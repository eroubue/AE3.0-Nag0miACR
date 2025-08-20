using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.QT;
using Millusion.ACR.Astrologian.UI;
using Millusion.Enum;
using Millusion.Interface;
using SpellsDefine = Millusion.Define.SpellsDefine;

namespace Millusion.ACR.Astrologian.SlotResolvers.Ability;

/// <summary>
///     占卜
/// </summary>
public class AST_Ability_Divination : BaseSlotResolver
{
    public static AST_Ability_Divination Instance { get; } = new();
    public override uint SpellId => SpellsDefine.Divination;
    public override SlotMode Mode => SlotMode.OffGcd;

    protected override int RunCheck()
    {
        if (!AST_View.UI.GetQt(AST_QT_Key.UseDivination)) return PreCheckCode.NotQT;

        // if (AI.Instance.BattleData.CurrBattleTimeInMs < GCDHelper.GetGCDDuration() &&
        //     AI.Instance.BattleData.CurrBattleTimeInMs < 2000) return -50;

        if (Data.IsInHighEndDuty)
        {
            if (AI.Instance.BattleData.CurrBattleTimeInMs < 10000)
            {
                uint play1 = 37023;
                if (play1.RecentlyUsed(10000)) return 10;
                return -2;
            }

            return 2;
        }

        if (!AST_BattleData.Instance.IsBossBattle &&
            (AST_BattleData.Instance.BattleRemainingTime < 12000 || Core.Me.IsMoving())) return -110;

        return PreCheckCode.Success;
    }
}