using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Millusion.ACR.Astrologian.BattleData;
using Millusion.ACR.Astrologian.SlotResolvers;
using Millusion.CharacterRefined;
using Millusion.Helper;

namespace Millusion.ACR.Astrologian.Events;

public class AST_RotationEventHandler : IRotationEventHandler
{
    public async Task OnPreCombat()
    {
        CharacterStatus.Update();

        MsData.Instance.Update();

        await Task.CompletedTask;
    }

    public void OnResetBattle()
    {
        AST_BattleData.Reset();
        AST_SlotResolvers.Reset();
    }

    public async Task OnNoTarget()
    {
        await NotTargetSlotResolvers.Run();

        await Task.CompletedTask;
    }

    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
        AST_BattleData.Instance.LastTime = TimeHelper.Now();
    }

    public void OnBattleUpdate(int currTimeInMs)
    {
        CharacterStatus.Update();

        MsData.Instance.Update();

        AST_BattleData.Update();

        // AST_SlotResolvers.Update();
    }


    public void OnEnterRotation()
    {
    }

    public void OnExitRotation()
    {
    }

    public void OnTerritoryChanged()
    {
    }
}