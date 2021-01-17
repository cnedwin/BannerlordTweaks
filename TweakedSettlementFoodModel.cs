using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks
{
    public class TweakedSettlementFoodModel : DefaultSettlementFoodModel
    {
        public override ExplainedNumber CalculateTownFoodStocksChange(Town town, bool includeDescriptions = false)
        {
            if (town == null) throw new ArgumentNullException(nameof(town));
            ExplainedNumber baseVal = base.CalculateTownFoodStocksChange(town, includeDescriptions);
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled)
            {
                ExplainedNumber en = baseVal;

                if (town.IsCastle)
                    en.Add(settings.CastleFoodBonus, new TextObject("Military rations"));
                else if (town.IsTown)
                    en.Add(settings.TownFoodBonus, new TextObject("Citizen food drive"));

                if (settings.SettlementProsperityFoodMalusTweakEnabled && settings.SettlementProsperityFoodMalusDivisor != 50)
                {
                    float malus = town.Owner.Settlement.Prosperity / 50f;
                    en.Add(malus, new TextObject("shouldn't be seen!"));

                    TextObject prosperityTextObj = GameTexts.FindText("str_prosperity", null);


                    malus = -town.Owner.Settlement.Prosperity / settings.SettlementProsperityFoodMalusDivisor;
                    en.Add(malus, prosperityTextObj);
                }

                return en;
            }
            else
                return baseVal;
        }
    }
}
