using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

// Replaced by DefaultBanditDensityModelPatch in 1.4.3 due to change to allow you to select troops for hideout battles.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
    public class DefaultTroopCountLimitModelPatch
    {
        static bool Prefix(ref int __result)
        {
            __result = Math.Min(BannerlordTweaksSettings.Instance.HideoutBattleTroopLimit, 90);
            return false;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.HideoutBattleTroopLimitTweakEnabled;
        }
    }
}