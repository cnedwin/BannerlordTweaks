using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    public class TweakedSiegeEventModel : DefaultSiegeEventModel
    {
        public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side)
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
                return base.GetConstructionProgressPerHour(type, siegeEvent, side) * settings.SiegeConstructionProgressPerDayMultiplier;
            else
                return base.GetConstructionProgressPerHour(type, siegeEvent, side);
        }

        public override int GetColleteralDamageCasualties(SiegeEngineType siegeEngineType, MobileParty party)
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
                return base.GetColleteralDamageCasualties(siegeEngineType, party) + settings.SiegeCollateralDamageCasualties;
            else
                return base.GetColleteralDamageCasualties(siegeEngineType, party);
        }

        public override int GetDestructionCasualties(SiegeEvent siegeEvent, BattleSideEnum side, SiegeEngineType destroyedSiegeEngine)
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
                return base.GetDestructionCasualties(siegeEvent, side, destroyedSiegeEngine) + settings.SiegeDestructionCasualties;
            else
                return base.GetDestructionCasualties(siegeEvent, side, destroyedSiegeEngine);
        }

    }
}