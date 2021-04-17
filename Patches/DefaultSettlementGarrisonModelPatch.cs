﻿using System;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks.Patches
{

    [HarmonyPatch]
    public class DefaultSettlementGarrisonModelPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("DefaultSettlementGarrisonModel"), "FindNumberOfTroopsToLeaveToGarrison", new Type[]
            {
                typeof(MobileParty),
                typeof(Settlement)
            }, null);
        }

        private static void Postfix(MobileParty mobileParty, Settlement settlement, ref int __result)
        {
            if (settlement == null || mobileParty == null) return;

            if (BannerlordTweaksSettings.Instance is { } settings && mobileParty.LeaderHero.Clan == Clan.PlayerClan)
            {
                bool DisableDonationClan = settlement.OwnerClan == Clan.PlayerClan && settings.DisableTroopDonationPatchEnabled;
                bool DisableForAnySettlement = settings.DisableTroopDonationAnyEnabled;

                if (DisableDonationClan || DisableForAnySettlement)
                {
                    __result = 0;
                }
            }
        }
        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.DisableTroopDonationPatchEnabled;
    }
}