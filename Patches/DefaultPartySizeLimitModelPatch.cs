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
        static void Postfix(MobileParty party, bool includeDescriptions,ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero && BannerlordTweaksSettings.Instance is { } settings)
            {
                int num;
                if (BannerlordTweaksSettings.Instance.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * settings.LeadershipPartySizeBonus);
                    __result.Add((float)num, new TextObject("综合设置统帅奖励", null),null);
                }

                if (BannerlordTweaksSettings.Instance.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * settings.StewardPartySizeBonus);
                    __result.Add((float)num, new TextObject("综合设置管理奖励", null), null);
                }
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PartySizeTweakEnabled;
    }
    
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        static void Postfix(PartyBase party, bool includeDescriptions, ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                if (BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled)
                {
                    double percent = Math.Abs((double)(settings.PrisonerSizeTweakPercent) / 100);
                    double num = (int)Math.Ceiling(__result.ResultNumber * percent);
                    __result.Add((float)num, new TextObject("综合设置俘虏上限", null), null);
                }
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled;
    }
}
