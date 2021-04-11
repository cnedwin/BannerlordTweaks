using HarmonyLib;
using System;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;


namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultArmyManagementCalculationModel), "CalculateCohesionChange")]
    public class CalculateCohesionChangePatch
    {
        static bool Prefix(Army army, bool includeDescriptions, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.ClanArmyLosesNoCohesionEnabled)
            {
                int num1 = 0;
                foreach (MobileParty party in army.Parties)
                {
                    if (!(party.LeaderHero is null) && party.LeaderHero.Clan != army.LeaderParty.LeaderHero.Clan)
                    {
                        num1++;
                    }
                }
                if (num1 > 0)
                {
                    return true;
                }
                else
                {
                    __result.Add(0, new TextObject("Clan-Only Armies Suffer No Cohesion Loss"));
                    return false;
                }
            }
            return true;
        }
        static void Postfix(Army army, bool includeDescriptions, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.BTCohesionTweakEnabled)
            {
                float num1 = Math.Abs(__result.ResultNumber) * (1f - settings.BTCohesionTweakv2);
                __result.Add(num1, new TextObject("BT Cohesion Tweak"));
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BTCohesionTweakEnabled;
    }
}