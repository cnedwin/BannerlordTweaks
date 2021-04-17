using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using System;


namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSettlementMilitiaModel), "CalculateMilitiaChange")]

    public class DefaultSettlementMilitiaModelPatch
    {
        static void Postfix(Settlement settlement, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled && !(settlement is null))
            {
                if (settlement.IsCastle)
                {
                    __result.Add(settlement.Militia * 0.025f, new TextObject("退休的", null));
                    __result.Add(settings.CastleMilitiaRetirementModifier * -settlement.Militia, new TextObject("退休的", null));
                    __result.Add(settings.CastleMilitiaBonusFlat, new TextObject("招聘驱动"));
                }
                if (settlement.IsTown)
                {
                    __result.Add(settlement.Militia * 0.025f, new TextObject("退休的", null));
                    __result.Add(settings.TownMilitiaRetirementModifier * -settlement.Militia, new TextObject("退休的", null));
                    __result.Add(settings.TownMilitiaBonusFlat, new TextObject("公民民兵"));
                }
            }
            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled;
    }
}