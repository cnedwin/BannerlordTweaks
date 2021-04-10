using HarmonyLib;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;

// Convert TweakedSettlementFoodModel to patch due to 1.5.7 changes.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSettlementFoodModel), "CalculateTownFoodStocksChange")]
    public class DefaultSettlementFoodModelPatch
    {
        //static void Postfix(Town town, ref ExplainedNumber result, ref int __result)
        static void Postfix(Town town, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled && !(town is null))
            {
                if (town.IsCastle)
                    __result.Add(__result.ResultNumber * settings.CastleFoodBonus, new TextObject("Military rations"));
                else if (town.IsTown)
                    __result.Add(__result.ResultNumber * settings.TownFoodBonus, new TextObject("Citizen food drive"));

                if (settings.SettlementProsperityFoodMalusTweakEnabled && settings.SettlementProsperityFoodMalusDivisor != 50)
                {
                    float malus = town.Owner.Settlement.Prosperity / 50f;
                    TextObject prosperityTextObj = GameTexts.FindText("str_prosperity", null);
                    __result.Add(malus, prosperityTextObj);
                    //explanation?.Lines.Remove(explanation.Lines.Last());


                    /*
                    var line = explanation?.Lines.Where((x) => !string.IsNullOrWhiteSpace(x.Name) && x.Name == prosperityTextObj.ToString()).FirstOrDefault();
                    if (line != null) explanation?.Lines.Remove(line);
                    */

                    malus = -town.Owner.Settlement.Prosperity / settings.SettlementProsperityFoodMalusDivisor;
                    __result.Add(malus, prosperityTextObj);
                }
            }
            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled;
    }
}
