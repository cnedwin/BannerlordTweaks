using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using System.Windows.Forms;
using System;

// Convert TweakedSettlementFoodModel to patch due to 1.5.7 changes.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSettlementMilitiaModel), "CalculateMilitiaChange")]

    public class DefaultSettlementMilitiaModelPatch
    {
        //static void Postfix(Settlement settlement, ref ExplainedNumber result, ref int __result)
        static void Postfix(Settlement settlement, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled && !(settlement is null))
            {
                if (settlement.IsCastle)
                    __result.Add(__result.ResultNumber * (settings.CastleMilitiaBonus - 1), new TextObject("Recruitment drive"));
                if (settlement.IsTown)
                    __result.Add(__result.ResultNumber * (settings.TownMilitiaBonus - 1), new TextObject("Citizen militia"));
            }
            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled;
    }
}
