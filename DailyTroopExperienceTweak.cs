using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    public class DailyTroopExperienceTweak
    {
        public static void Apply(Campaign campaign)
        {
            var obj = new DailyTroopExperienceTweak();
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(obj, (MobileParty mp) => { obj.DailyTick(mp); });
        }

        private void DailyTick(MobileParty party)
        {
            if (party.LeaderHero != null)
            {
                int count = party.MemberRoster.Troops.Count();
                if (party.LeaderHero == Hero.MainHero || BannerlordTweaksSettings.Instance.DailyTroopExperienceApplyToAllNPC ||
                    BannerlordTweaksSettings.Instance.DailyTroopExperienceApplyToPlayerClanMembers && party.LeaderHero.Clan == Clan.PlayerClan)
                {
                    int experienceAmount = ExperienceAmount(party.LeaderHero);
                    if (experienceAmount > 0)
                    {
                        foreach (var troop in party.MemberRoster.Troops)
                        {
                            party.MemberRoster.AddXpToTroop(experienceAmount, troop);
                        }

                        if (BannerlordTweaksSettings.Instance.DisplayMessageDailyExperienceGain)
                        {
                            string troops = count == 1 ? "战士" : "部队";
                            //Debug
                            //InformationManager.DisplayMessage(new InformationMessage($"{party.LeaderHero.Name}'s party granted {experienceAmount} experience to {count} {troops}."));
                            if (party.LeaderHero == Hero.MainHero)
                                InformationManager.DisplayMessage(new InformationMessage($"综合设置Mod授予 {experienceAmount} 经验 给 {count} {troops}."));
                        }
                    }
                }
            }
        }

        private static int ExperienceAmount(Hero h)
        {
            int leadership = h.GetSkillValue(DefaultSkills.Leadership);
            if (leadership >= BannerlordTweaksSettings.Instance.DailyTroopExperienceRequiredLeadershipLevel)
                return (int)(BannerlordTweaksSettings.Instance.LeadershipPercentageForDailyExperienceGain * leadership);
            return 0;
        }
    }
}
