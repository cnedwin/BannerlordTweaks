using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero && BannerlordTweaksSettings.Instance != null)
            {
                int num;
                if (BannerlordTweaksSettings.Instance.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * BannerlordTweaksSettings.Instance.LeadershipPartySizeBonus);
                    __result.Add(num, new TextObject("BT Leadership bonus"));
                }

                if (BannerlordTweaksSettings.Instance.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * BannerlordTweaksSettings.Instance.StewardPartySizeBonus);
                    __result.Add(num, new TextObject("BT Steward bonus"));
                }
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.PartySizeTweakEnabled;
        }
    }
    
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        static void Postfix(PartyBase party, ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                if (BannerlordTweaksSettings.Instance.PrisonerSizeTweakEnabled)
                {
                    float percent = Math.Abs(BannerlordTweaksSettings.Instance.PrisonerSizeTweakPercent / 100);
                    float num = __result.ResultNumber * percent;
                    __result.Add(num, new TextObject("BT Prisoner Limit Bonus"));
                }
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.PrisonerSizeTweakEnabled;
        }
    }

}
