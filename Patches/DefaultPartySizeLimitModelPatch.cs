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
            if (!(party is null) && !(party.LeaderHero is null) && BannerlordTweaksSettings.Instance is { } settings)
            {
                float num;
                if (settings.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * settings.LeadershipPartySizeBonus * ((party.LeaderHero == Hero.MainHero) ? 1 : settings.PartySizeTweakAIFactor));
                    __result.Add((float)num, new TextObject("BT 统率奖励"));
                }

                if (settings.StewardPartySizeBonusEnabled && party.LeaderHero == Hero.MainHero)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * settings.StewardPartySizeBonus * ((party.LeaderHero == Hero.MainHero) ? 1 : settings.PartySizeTweakAIFactor));
                    __result.Add((float)num, new TextObject("BT 管理奖励"));
                }
                //if (settings.BalancingPartySizeTweaksEnabled && party.LeaderHero.Clan.Kingdom != null && party.LeaderHero != Hero.MainHero)
                //{
                //    switch (party.LeaderHero.Clan.Kingdom.StringId)
                //    {
                //        case "vlandia":
                //            num = __result.ResultNumber * settings.VlandiaBoost;
                //            __result.Add((float)num, new TextObject("BT Vlandia Boost"));
                //            break;
                //        case "battania":
                //            num = (__result.ResultNumber * settings.BattaniaBoost);
                //            __result.Add((float)num, new TextObject("BT Battania Boost"));
                //            break;
                //        case "empire":
                //            num = (__result.ResultNumber * settings.Empire_N_Boost);
                //            __result.Add((float)num, new TextObject("BT Empire boost"));
                //            break;
                //        case "empire_s":
                //            num = (__result.ResultNumber * settings.Empire_S_Boost);
                //            __result.Add((float)num, new TextObject("BT Empire(S) boost"));
                //            break;
                //        case "empire_w":
                //            num = (__result.ResultNumber * settings.Empire_W_Boost);
                //            __result.Add((float)num, new TextObject("BT Empire(W) boost"));
                //            break;
                //        case "sturgia":
                //            num = (__result.ResultNumber * settings.SturgiaBoost);
                //            __result.Add((float)num, new TextObject("BT Sturgia boost"));
                //            break;
                //        case "khuzait":
                //            num = (__result.ResultNumber * settings.KhuzaitBoost);
                //            __result.Add((float)num, new TextObject("BT Khuzait boost"));
                //            break;
                //        case "aserai":
                //            num = (__result.ResultNumber * settings.Aseraiboost);
                //            __result.Add((float)num, new TextObject("BT Aserai boost"));
                //            break;

                //    }
                //}
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PartySizeTweakEnabled;
}


    //[HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "GetPartyPrisonerSizeLimit")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        private static void Postfix(PartyBase party, ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                if (BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled)
                {
                    double num = (int)Math.Ceiling(__result.ResultNumber * settings.PrisonerSizeTweakPercent);
                    __result.Add((float)num, new TextObject("BT 俘虏上限奖励"));
                }
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled;
    }
}
