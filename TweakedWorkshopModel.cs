using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Library;

namespace BannerlordTweaks
{
    public class TweakedWorkshopModel : DefaultWorkshopModel
    {
        public override int GetMaxWorkshopCountForPlayer()
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.MaxWorkshopCountTweakEnabled)
                return settings.BaseWorkshopCount + Clan.PlayerClan.Tier * settings.BonusWorkshopsPerClanTier;
            else
                return base.GetMaxWorkshopCountForPlayer();
        }

        public override int GetBuyingCostForPlayer(Workshop workshop)
        {
            if (workshop == null) throw new ArgumentNullException(nameof(workshop));
            if (BannerlordTweaksSettings.Instance is { } settings && settings.WorkshopBuyingCostTweakEnabled)
                return workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Prosperity / 2 + settings.WorkshopBaseCost;
            else
                return base.GetBuyingCostForPlayer(workshop);
        }
        public override int GetConvertProductionCost(WorkshopType workshopType)
        {
            if (workshopType == null) throw new ArgumentNullException(nameof(workshopType));
            if (BannerlordTweaksSettings.Instance is { } settings && settings.WorkshopEffectivnessEnabled)
                return (int)MathF.Round(base.GetConvertProductionCost(workshopType) * (1f/settings.WorkshopEffectivnessFactor));
            else
                return base.GetConvertProductionCost(workshopType);
        }
        public override int GetDailyExpense(int level)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.WorkshopEffectivnessEnabled)
                return (int)MathF.Round(base.GetDailyExpense(level) * (1f/settings.WorkshopEffectivnessFactor));
            else
                return base.GetDailyExpense(level);
        }
    }
}
