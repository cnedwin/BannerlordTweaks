using System;
using System.Reflection;
using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace BannerlordTweaks.Patches
{
	[HarmonyPatch(typeof(LordPartyComponent), "InitializeLordPartyProperties")]
	public class SpawnLordPartyAuxPatch
	{
		static bool Prefix(MobileParty mobileParty, Vec2 position, float spawnRadius, Settlement spawnSettlement)
		{
			//MobileParty mobileParty = MBObjectManager.Instance.CreateObject<MobileParty>(hero.CharacterObject.StringId + "_party_1");

			Hero hero = mobileParty.LordPartyComponent.Owner;
			mobileParty.AddElementToMemberRoster(hero.CharacterObject, 1, true);
			mobileParty.ActualClan = hero.Clan;
			int troopNumberLimit = (hero != Hero.MainHero && hero.Clan != Clan.PlayerClan) ? BannerlordTweaksSettings.Instance.Strategy_ModifyRespawnParty_AILordPartySizeOnRespawn : BannerlordTweaksSettings.Instance.Strategy_ModifyRespawnParty_PlayerPartySizeOnRespawn;
			if (!Campaign.Current.GameStarted)
			{
				float randomFloat = MBRandom.RandomFloat;
				double num = Math.Sqrt((double)MBRandom.RandomFloat);
				double num2 = 1.0 - (double)randomFloat * num;
				troopNumberLimit = (int)((double)mobileParty.Party.PartySizeLimit * num2);
			}
			mobileParty.InitializeMobileParty(hero.Clan.DefaultPartyTemplate, position, spawnRadius, 0f, troopNumberLimit);
			mobileParty.Party.Owner = hero;
			mobileParty.Party.Visuals.SetMapIconAsDirty();
			if (spawnSettlement != null)
			{
				mobileParty.SetMoveGoToSettlement(spawnSettlement);
			}
			mobileParty.Aggressiveness = 0.9f + 0.1f * (float)hero.GetTraitLevel(DefaultTraits.Valor) - 0.05f * (float)hero.GetTraitLevel(DefaultTraits.Mercy);
			mobileParty.ItemRoster.Add(new ItemRosterElement(DefaultItems.Grain, MBRandom.RandomInt(15, 30), null));
			hero.PassedTimeAtHomeSettlement = (float)((int)(MBRandom.RandomFloat * 100f));
			if (spawnSettlement != null)
			{
				mobileParty.Ai.SetAIState(AIState.VisitingNearbyTown, null);
				mobileParty.SetMoveGoToSettlement(spawnSettlement);
			}
			return false;
		}

		static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.Strategy_ModifyRespawnParty;
		

		public SpawnLordPartyAuxPatch()
		{
		}

		static SpawnLordPartyAuxPatch()
		{
		}

		//private static readonly MethodInfo Owner = typeof(LordPartyComponent).GetMethod("LordPartyComponent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	}
}
