using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Localization;


namespace BannerlordTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateRenownGain")]
    [HarmonyPriority(Priority.Last)]
    public class DefaultBattleRewardModelPatch
    {
        static void Postfix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        {
            if ((BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero))
            {
                float battleRenownMultiplier = settings.BattleRenownMultiplier;
                battleRenownMultiplier -= 1f;
                if (party.LeaderHero == Hero.MainHero && settings.BattleRewardShowDebug)
                {
                    String BTTweak = "";

                    if ((float)Math.Round((double)battleRenownMultiplier * 100f, 2) > 0f)
                    {
                        BTTweak = "+";
                    }

                    DebugHelpers.DebugMessage("荣誉值 = " + (float)Math.Round((double)renownValueOfBattle, 2) + "| 你的分享 = " + (float)Math.Round((double)renownValueOfBattle * contributionShare, 2) + "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                                "\nPerkBonus = " + (float)Math.Round((double)result.ResultNumber - result.BaseNumber, 2) +
                                                "(" + (float)Math.Round((double)(result.ResultNumber / result.BaseNumber - 1f) * 100f, 1) + "%)" +
                                                "\nSum = " + (float)Math.Round((double)result.ResultNumber, 2) +
                                                "\nBT Tweak = " + (float)Math.Round((double)battleRenownMultiplier * result.ResultNumber, 2) + "(" + BTTweak + (float)Math.Round((double)battleRenownMultiplier * 100f, 1) + "%)" +
                                                "\n\n");
                }
                __result = result.ResultNumber + (battleRenownMultiplier * result.ResultNumber);
                result.Add(battleRenownMultiplier * result.ResultNumber, new TextObject("BT Renown Tweak"), null);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleRewardTweaksEnabled;
    }

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateInfluenceGain")]
    public class DefaultBattleRewardModelInfluencePatch
    {

        static void Postfix(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        {
            if ((BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero))
            {
                float battleInfluenceMultiplier = settings.BattleInfluenceMultiplier;
                battleInfluenceMultiplier -= 1f;

                if (party.LeaderHero == Hero.MainHero && settings.BattleRewardShowDebug)
                {
                    String BTTweak = "";

                    if ((float)Math.Round((double)battleInfluenceMultiplier * 100f, 0) > 0f)
                    {
                        BTTweak = "+";
                    }

                    DebugHelpers.DebugMessage("影响力值 = " + (float)Math.Round((double)influenceValueOfBattle, 2) + " | 你的分享 = " + (float)Math.Round((double)influenceValueOfBattle * contributionShare, 2) + "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                            "\nBT Tweak = " + (float)Math.Round((double)(battleInfluenceMultiplier * result.ResultNumber), 2) + "(" + BTTweak + (float)Math.Round((double)battleInfluenceMultiplier * 100f, 1) + "%)");
                }
                __result = result.ResultNumber + (battleInfluenceMultiplier * result.ResultNumber);
                result.Add(party.MapFaction.IsKingdomFaction ? (result.ResultNumber * battleInfluenceMultiplier) : 0f, new TextObject("BT 影响力调整"), null);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleRewardTweaksEnabled;

    }
    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateMoraleGainVictory")]
    public class DefaultBattleRewardModelMoralePatch
    {

        static void Postfix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        {
            if ((BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero))
            {
                float battleMoraleMultiplier = settings.BattleMoraleMultiplier;
                battleMoraleMultiplier -= 1f;

                if (party.LeaderHero == Hero.MainHero && settings.BattleRewardShowDebug)
                {
                    String BTTweak = "";

                    if ((float)Math.Round((double)battleMoraleMultiplier * 100f, 0) > 0f)
                    {
                        BTTweak = "+";
                    }

                    DebugHelpers.DebugMessage("Morale Value = " + (float)Math.Round((double)(result.ResultNumber / contributionShare), 2) + " | Your share = " + (float)Math.Round((double)result.ResultNumber * contributionShare, 2) + "(" + (float)Math.Round((double)contributionShare * 100f, 1) + "%)" +
                                            "\nBT Tweak = " + (float)Math.Round((double)(battleMoraleMultiplier * result.ResultNumber), 2) + "(" + BTTweak + (float)Math.Round((double)battleMoraleMultiplier * 100f, 1) + "%)");
                }
                __result = result.ResultNumber + (battleMoraleMultiplier * result.ResultNumber);
                result.Add(party.MapFaction.IsKingdomFaction ? (result.ResultNumber * battleMoraleMultiplier) : 0f, new TextObject("BT Influence Tweak"), null);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleRewardTweaksEnabled;

    }
}