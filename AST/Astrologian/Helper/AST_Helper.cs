using AEAssist.CombatRoutine;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.DalamudServices;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.Setting;
using Millusion.Helper;
using Action = Lumina.Excel.GeneratedSheets.Action;

namespace Millusion.ACR.Astrologian.Helper;

public static class AST_Helper
{
    public static List<Action> Actions { get; set; } =
        Svc.Data.GetExcelSheet<Action>()
            ?.Where(r => r.ClassJobCategory.Row == 99 && r.ClassJob.Value != null && r is { IsPvP: false }).ToList();

    public static int NeedHealWithASTPower(this IBattleChara battleChara)
    {
        var p = 1f;
        var anotherHelper = AST_BattleData.Instance.AnotherHealer;
        if (anotherHelper != null)
        {
            var job = anotherHelper.CurrentJob();
            p = job switch
            {
                Jobs.Astrologian => 0.6f,
                Jobs.Scholar => 0.8f,
                Jobs.WhiteMage => 0.6f,
                Jobs.Sage => 0.8f,
                _ => p
            };
        }

        var r = battleChara.NeedHealWithPower(AST_Settings.Instance.MaxHealPercent);

        return (int)(r * p);
    }
}