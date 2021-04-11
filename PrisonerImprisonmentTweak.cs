using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;


namespace BannerlordTweaks
{
    public static class PrisonerImprisonmentTweak
    {
        public static void Apply(Campaign campaign)
        {
            if (campaign == null) throw new ArgumentNullException(nameof(campaign));
            var escapeBehaviour = campaign.GetCampaignBehavior<PrisonerReleaseCampaignBehavior>();
            if (escapeBehaviour != null && CampaignEvents.DailyTickHeroEvent != null)
            {
                CampaignEvents.DailyTickHeroEvent.ClearListeners(escapeBehaviour);
                CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(escapeBehaviour, (Hero h) => { Check(escapeBehaviour, h); });
            }
        }

        private static void Check(PrisonerReleaseCampaignBehavior escapeBehaviour, Hero hero)
        {
            if (escapeBehaviour == null || !(BannerlordTweaksSettings.Instance is { } settings) || !hero.IsPrisoner) return;

            if (hero.PartyBelongedToAsPrisoner != null && (hero.PartyBelongedToAsPrisoner.MapFaction != null || hero.PartyBelongedToAsPrisoner.LeaderHero?.Clan == Hero.MainHero.Clan))
            {
                bool flag = hero.PartyBelongedToAsPrisoner.MapFaction == Hero.MainHero.MapFaction || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan);

                if ((settings.PrisonerImprisonmentPlayerOnly && flag) || (settings.PrisonerImprisonmentPlayerOnly == false && (Kingdom.All.Contains(hero.PartyBelongedToAsPrisoner.MapFaction) || hero.PartyBelongedToAsPrisoner.IsSettlement)))
                    flag = true;

                if (flag == true)
                {
                    if ((hero.PartyBelongedToAsPrisoner.NumberOfHealthyMembers < hero.PartyBelongedToAsPrisoner.NumberOfPrisoners && !hero.PartyBelongedToAsPrisoner.IsSettlement) ||
                        hero.PartyBelongedToAsPrisoner.IsStarving ||
                        (hero.MapFaction != null && FactionManager.IsNeutralWithFaction(hero.MapFaction, hero.PartyBelongedToAsPrisoner.MapFaction)) ||
                        (int)hero.CaptivityStartTime.ElapsedDaysUntilNow > settings.MinimumDaysOfImprisonment)
                    {
                        typeof(PrisonerReleaseCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
                        return;
                    }
                    return;
                }

                else
                {
                    typeof(PrisonerReleaseCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
                }
                return;

            }
            else
            {
                typeof(PrisonerReleaseCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
            }
        }

        public static void DailyTick()
        {
            foreach (Hero hero in Hero.All)
            {
                if (hero == null) return;
                if (hero.PartyBelongedToAsPrisoner == null && hero.IsPrisoner && hero.IsAlive && !hero.IsActive && !hero.IsNotSpawned && !hero.IsReleased)
                {
                    float days = hero.CaptivityStartTime.ElapsedDaysUntilNow;
                    if (BannerlordTweaksSettings.Instance is { } settings && (days > (settings.MinimumDaysOfImprisonment + 3)))
                    {
                        DebugHelpers.ColorGreenMessage("释放 " + hero.Name + " 因为英雄丢失BUG失踪. (" + (int)days + " 天)");
                        DebugHelpers.QuickInformationMessage("释放 " + hero.Name + " 因为英雄丢失BUG失踪. (" + (int)days + " 天)");
                        EndCaptivityAction.ApplyByReleasing(hero);
                    }
                }
            }
        }
    }
}