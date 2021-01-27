﻿using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartyWageModel), "GetTotalWage")]
    public class DefaultPartyWageModelPatch
    {

        static void Postfix(MobileParty mobileParty, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.PartyWageTweaksEnabled && mobileParty != null)
            {
                if (mobileParty.IsMainParty || (mobileParty.Party.MapFaction == Hero.MainHero.MapFaction && settings.ApplyWageTweakToFaction && !mobileParty.IsGarrison))
                {
                    float num = settings.PartyWagePercent;
                    //__result = MathF.Round(__result * num);
                    __result.AddFactor(num, new TextObject("BT 部队工资调整"));
                }
                if (mobileParty.IsGarrison && mobileParty.Party.Owner == Hero.MainHero)
                {
                    float num2 = settings.GarrisonWagePercent;
                    __result.AddFactor(num2, new TextObject("BT 驻军工资调整"));
                    // DebugHelpers.DebugMessage("Adjusted garrison " + mobileParty.Name + "by " + num2 + ". Value: " + __result);
                }
                //DebugHelpers.DebugMessage("Relevant data: mobileParty.Party.Owner:" + mobileParty.Party.Owner + "\nmobileParty.Party.MapFaction:" + mobileParty.Party.MapFaction + "\nmobileParty.Party.LeaderHero:" + mobileParty.Party.LeaderHero + "\nmobileParty.Party.Leader" + mobileParty.Party.Leader);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PartyWageTweaksEnabled;
    }
}