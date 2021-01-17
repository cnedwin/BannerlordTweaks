using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultCaravanPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, bool includeDescriptions, ref ExplainedNumber __result)
        {
            if (party.IsCaravan && party.Party?.Owner != null && party.Party.Owner == Hero.MainHero && BannerlordTweaksSettings.Instance is not null)
            {
                ExplainedNumber result = new ExplainedNumber(BannerlordTweaksSettings.Instance.PlayerCaravanPartySize);
                __result = result;
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled;
    }
}
