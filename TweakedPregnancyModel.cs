﻿using Helpers;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedPregnancyModel : DefaultPregnancyModel
    {
        public override float StillbirthProbability => BannerlordTweaksSettings.Instance is { } settings && settings.NoStillbirthsTweakEnabled
           ? -1
           : base.StillbirthProbability;

        public override float MaternalMortalityProbabilityInLabor => BannerlordTweaksSettings.Instance is { } settings && settings.NoMaternalMortalityTweakEnabled
            ? -1
            : base.MaternalMortalityProbabilityInLabor;

        public override float DeliveringTwinsProbability => BannerlordTweaksSettings.Instance is { } settings && settings.TwinsProbabilityTweakEnabled
            ? settings.TwinsProbability
            : base.DeliveringTwinsProbability;

        public override float DeliveringFemaleOffspringProbability => BannerlordTweaksSettings.Instance is { } settings && settings.FemaleOffspringProbabilityTweakEnabled
            ? settings.FemaleOffspringProbability
            : base.DeliveringFemaleOffspringProbability;

        public override float PregnancyDurationInDays => BannerlordTweaksSettings.Instance is { } settings && settings.PregnancyDurationTweakEnabled
            ? settings.PregnancyDuration
            : base.PregnancyDurationInDays;

        public override float GetDailyChanceOfPregnancyForHero(Hero hero)
        {
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            if (BannerlordTweaksSettings.Instance is { } settings && hero != null)
            {
                if (!settings.DailyChancePregnancyTweakEnabled)
                    return base.GetDailyChanceOfPregnancyForHero(hero);

                float num = 0f;
                if (settings.PlayerCharacterFertileEnabled && HeroIsMainOrSpouseOfMain(hero))
                {
                    return num;
                }

                if (hero.Children != null && hero.Children.Any() && hero.Children.Count >= BannerlordTweaksSettings.Instance.MaxChildren)
                {
                    return num;
                }

                if (hero != null && hero.Spouse != null && IsHeroAgeSuitableForPregnancy(hero))
                {
                    ExplainedNumber bonuses = new ExplainedNumber(1f, false);
                    PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.PerfectHealth, hero.Clan.Leader.CharacterObject, true, ref bonuses);
                    num = (float)((6.9 - ((double)hero.Age - settings.MinPregnancyAge) * 0.2) * 0.02) / ((hero.Children!.Count + 1) * 0.2f) * bonuses.ResultNumber;
                }

                if (hero!.Clan == Hero.MainHero.Clan)
                    num *= settings.ClanFertilityBonus;

                return num;
            }
            return base.GetDailyChanceOfPregnancyForHero(hero);
        }

        private bool IsHeroAgeSuitableForPregnancy(Hero hero)
        {
            if (!hero.IsFemale)
                return true;

            return (double)hero.Age >= BannerlordTweaksSettings.Instance!.MinPregnancyAge && (double)hero.Age <= BannerlordTweaksSettings.Instance!.MaxPregnancyAge;
        }

        private bool HeroIsMainOrSpouseOfMain(Hero hero)
        {
            if (hero == Hero.MainHero)
                return true;

            if (hero.Spouse == Hero.MainHero)
                return true;

            return false;
        }


    }
}