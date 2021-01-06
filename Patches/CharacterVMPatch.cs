﻿using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

// Retired in 1.5.6 - Remote Companion Mgmt is part of the base game now.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CharacterVM), "CanAddFocusToSkillWithFocusAmount")]
    public class CharacterVMPatch
    {
        static bool Prefix(int ____unspentCharacterPoints, int currentFocusAmount, ref bool __result)
        {
            __result = currentFocusAmount < 5 && ____unspentCharacterPoints > 0;
            return false;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.RemoteCompanionSkillManagementEnabled;
    }
}
