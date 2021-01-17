﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{

    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum) })]
    public class TweakedBattleSizePiatch
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
        {

            if (BannerlordTweaksSettings.Instance is { } settings && settings.BattleSize > 0)
            {
                //DebugHelpers.DebugMessage("MissonAgentSpawnLogic Battle Size Adjustment Triggered");
                ____battleSize = BannerlordTweaksSettings.Instance.BattleSize;
            }

            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleSizeTweakEnabled;
    }


    [HarmonyPatch(typeof(BannerlordConfig), "BattleSize", MethodType.Getter)]

    public class BattleSizesPatch
    {
        static bool Prefix(ref int __result)
        {
            if (BannerlordTweaksSettings.Instance is not null)
            {
                __result = BannerlordTweaksSettings.Instance.BattleSize;
                return false;
            }
            return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleSizeTweakEnabled;
    }
}