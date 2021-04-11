﻿using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace BannerlordTweaks.Patches
{
    static public class QuestPartySizeHelper
    {
        static public int GetPartySize(MobileParty mobileParty)
        {
            if (mobileParty == null) throw new ArgumentNullException(nameof(mobileParty));
            int partySize = mobileParty.Party.NumberOfAllMembers;

            if (mobileParty.MemberRoster != null)
            {
                foreach (TroopRosterElement troopRosterElement in mobileParty.MemberRoster.GetTroopRoster())
                {
                    if (troopRosterElement.Character != null && troopRosterElement.Character.Culture != null && troopRosterElement.Character.Culture.Villager != null &&
                        (troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>(troopRosterElement.Character.Culture.Villager.StringId)) ||
                        troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("borrowed_troop") ||
                        troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("veteran_borrowed_troop"))
                    {
                        partySize -= troopRosterElement.Number;
                    }
                }
            }

            return partySize;
        }
    }
    [HarmonyPatch(typeof(DefaultPartyMoraleModel), "GetPartySizeMoraleEffect")]
    public class GetPartySizeMoraleEffectPatch
    {
        static bool Prefix(MobileParty mobileParty, ref ExplainedNumber result, TextObject ____partySizeMoraleText)
        {
            if (mobileParty != null && mobileParty.Party != null && mobileParty.Party.LeaderHero != null && !mobileParty.IsMilitia && !mobileParty.IsVillager)
            {
                int num = QuestPartySizeHelper.GetPartySize(mobileParty) - mobileParty.Party.PartySizeLimit;
                if (num > 0)
                {
                    result.Add(-1f * (float)Math.Sqrt((double)num), ____partySizeMoraleText);
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.QuestCharactersIgnorePartySize;
    }

    [HarmonyPatch(typeof(DefaultPartyMoraleModel), "NumberOfDesertersDueToPaymentRatio")]
    public class NumberOfDesertersDueToPaymentRatioPatch
    {
        static bool Prefix(MobileParty mobileParty, ref int __result)
        {
            if (mobileParty != null && mobileParty.Party != null && mobileParty.Party.LeaderHero != null && mobileParty.Party.LeaderHero == Hero.MainHero)
            {
                int partySizeLimit = mobileParty.Party.PartySizeLimit;
                __result = MBRandom.RoundRandomized(((float)QuestPartySizeHelper.GetPartySize(mobileParty) - mobileParty.PaymentRatio * (float)partySizeLimit) * 0.2f);
                return false;
            }
            else
                return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.QuestCharactersIgnorePartySize;
    }
}