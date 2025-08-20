using AEAssist.Helper;
using ECommons.DalamudServices;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.Sheets;

namespace Millusion.CharacterRefined;

public class Equations
{
    private static (uint Ilvl, int Dmg)? cachedIlvl;

    public static double CalcDh(int dh, in LevelModifier lvlModifier)
    {
        var cVal = Math.Floor(550d * (dh - lvlModifier.Sub) / lvlModifier.Div) / 1000d;
        return cVal;
    }

    public static double CalcDet(int det, in LevelModifier lvlModifier)
    {
        var cVal = Math.Floor(140d * (det - lvlModifier.Main) / lvlModifier.Div) / 1000d;
        return cVal;
    }

    public static double CalcCritRate(int crit, in LevelModifier lvlModifier)
    {
        var cVal = Math.Floor(200d * (crit - lvlModifier.Sub) / lvlModifier.Div + 50) / 1000d;
        return cVal;
    }

    public static double CalcCritDmg(int crit, in LevelModifier lvlModifier)
    {
        var cVal = Math.Floor(200d * (crit - lvlModifier.Sub) / lvlModifier.Div + 1400) / 1000d;
        return cVal;
    }

    public static double CalcTenacityDmg(int ten, in LevelModifier lvlModifier)
    {
        var cVal = Math.Floor(112d * (ten - lvlModifier.Sub) / lvlModifier.Div) / 1000d;
        return cVal;
    }

    public static unsafe (double AvgHeal, double NormalHeal, double CritHeal) CalcExpectedOutput(UIState* uiState,
        JobId jobId, double det, double critMult, double critRate,
        double dh, double ten, in LevelModifier lvlModifier, uint? ilvlSync, IlvlSyncType ilvlSyncType)
    {
        try
        {
            var lvl = uiState->PlayerState.CurrentLevel;
            var ap = uiState->PlayerState.Attributes[
                (int)(jobId.IsCaster() ? Attributes.AttackMagicPotency : Attributes.AttackPower)];
            var inventoryItemData = (ushort*)((IntPtr)InventoryManager.Instance() + 9272);
            var weaponBaseDamage = /* phys/magic damage */
                inventoryItemData[jobId.IsCaster() ? 17 : 16] + /* hq bonus */ inventoryItemData[29];
            if (ilvlSync != null &&
                ( /* equip lvl */ inventoryItemData[35] > lvl || ilvlSyncType == IlvlSyncType.Strict))
            {
                if (cachedIlvl?.Ilvl != ilvlSync)
                    cachedIlvl = (ilvlSync.Value,
                        Svc.Data.GetExcelSheet<ItemLevel>()!.GetRow(ilvlSync.Value)!.PhysicalDamage);
                weaponBaseDamage = Math.Min(cachedIlvl.Value.Dmg, weaponBaseDamage);
            }

            var weaponDamage = Math.Floor(weaponBaseDamage + lvlModifier.Main * jobId.AttackModifier() / 1000.0) /
                               100.0;

            var healPot =
                Math.Floor(100 + LevelModifiers.HealModifier(lvl) * (ap - lvlModifier.Main) / lvlModifier.Main) / 100;
            var healBaseMultiplier = Math.Floor(100 * healPot * weaponDamage);
            var healWithDet = Math.Floor(healBaseMultiplier * (1 + det));
            var healWithTen = Math.Floor(healWithDet * (1 + ten));
            var normalHeal = Math.Floor(healWithTen * (jobId.IsCaster() ? jobId.TraitModifiers(lvl) : 1));
            var avgHeal = Math.Floor(normalHeal * (1 + (critMult - 1) * critRate));
            var critHeal = Math.Floor(normalHeal * critMult);
            return (avgHeal, normalHeal, critHeal);
        }
        catch (Exception e)
        {
            // Service.PluginLog.Warning(e, "Failed to calculate raw damage");
            LogHelper.Error(e + "Failed to calculate raw damage");
            return (0, 0, 0);
        }
    }
}