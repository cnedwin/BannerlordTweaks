﻿using HarmonyLib;
using System;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Barterables;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.Library;
using System.Text.RegularExpressions;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(JoinKingdomAsClanBarterable), "GetUnitValueForFaction")]
    public class BarterablePatches
    {
        static void Postfix(ref int __result, IFaction factionForEvaluation, Kingdom ___TargetKingdom)
        {
            if (!(BannerlordTweaksSettings.Instance is { } settings)) return;

            Hero factionLeader = factionForEvaluation.Leader;

            // Only proceed if the main hero is involved and it's not intra-faction barter.
            if (___TargetKingdom.MapFaction == factionForEvaluation.MapFaction || ___TargetKingdom.MapFaction != Hero.MainHero.MapFaction || ___TargetKingdom.Leader != Hero.MainHero) return;


            // Don't let Faction Leaders Defect from their Own Factions
            if (factionLeader == null || factionLeader.IsFactionLeader) return;

            if (settings.BarterablesTweaksEnabled)
            {
                double cost = __result * settings.BarterablesJoinKingdomAsClanAdjustment;
                //cost = Math.Abs(__result * (double)((num) / 100));
                __result = (int)Math.Round(cost);
            }

            if (settings.BarterablesJoinKingdomAsClanAltFormulaEnabled)
            {
                //int original_result = __result;
                __result /= 10;

                int relations = Hero.MainHero.GetRelation(factionLeader);
                if (relations > 100) relations = 99;

                double percent = Math.Abs(((double)(relations) / 100) - 1);

                // Make it very expensive to try to lure a Lord w/ negative relations.
                double num2 = (relations > -1) ? (__result * percent) : (__result * percent) * 100;

                __result = (int)(Math.Round(num2));
                //DebugHelpers.DebugMessage("Relations = " + relations + " | Original = " + original_result + " | adjusted = " + percent + " | num2 =" + num2 + " | Result = " + __result);
                //DebugHelpers.DebugMessage("Leader: "+leader.Name+" | MapFaction: "+leader.MapFaction.Name+" | MapFaction Leader: "+leader.MapFaction.Leader.Name);
            }
        }

        static bool Prepare => BannerlordTweaksSettings.Instance is { } settings && settings.BarterablesTweaksEnabled;
    }
}
