using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace BannerlordTweaks
{
	public class ChangeSettlementCulture : CampaignBehaviorBase
	{
		public override void RegisterEvents()
		{
			CampaignEvents.ClanChangedKingdom.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, bool, bool>(this.OnClanChangedKingdom));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.WeeklyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.OnWeeklyTickSettlement));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.OnSiegeAftermathAppliedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, SiegeAftermathCampaignBehavior.SiegeAftermath, Clan, Dictionary<MobileParty, float>>(this.OnSiegeAftermathApplied));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
		}

		private void OnGameLoaded(CampaignGameStarter obj)
		{
			Dictionary<Settlement, CultureObject> startingCultures = new Dictionary<Settlement, CultureObject>();
			foreach (Settlement settlement in from settlement in Campaign.Current.Settlements where settlement.IsTown || settlement.IsCastle || settlement.IsVillage select settlement)
			{
				startingCultures.Add(settlement, settlement.Culture);
				if (settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != null && settlement.OwnerClan.Culture != settlement.Culture && !this.WeekCounter.ContainsKey(settlement))
				{
					this.AddCounter(settlement);
				}
				else if ((settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != null && settlement.OwnerClan.Culture != settlement.Culture) && !this.WeekCounter.ContainsKey(settlement) && this.IsSettlementDue(settlement))
				{
					ChangeSettlementCulture.Transform(settlement, false);
				}
			}
			ChangeSettlementCulture.initialCultureDictionary = startingCultures;
		}

		private void OnSiegeAftermathApplied(MobileParty arg1, Settlement settlement, SiegeAftermathCampaignBehavior.SiegeAftermath arg3, Clan arg4, Dictionary<MobileParty, float> arg5)
		{
			this.AddCounter(settlement);
		}

		private void OnSettlementOwnerChanged(Settlement settlement, bool arg2, Hero arg3, Hero arg4, Hero arg5, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{

			if (detail != ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.BySiege)
			{
				this.AddCounter(settlement);
			}
			else
			{
				settlement.Culture = ChangeSettlementCulture.initialCultureDictionary[settlement];
			}
		}

		private void OnClanChangedKingdom(Clan clan, Kingdom arg2, Kingdom arg3, bool arg4, bool arg5)
		{
			if (clan.Culture != clan.Kingdom.Culture)
			{
				foreach (Settlement settlement in from settlement in clan.Settlements where settlement.IsTown || settlement.IsCastle || settlement.IsVillage select settlement)
				{
					this.AddCounter(settlement);
				}
			}
		}

		public static void Transform(Settlement settlement, bool removeTroops)
		{
			if (settlement.IsVillage || settlement.IsCastle || settlement.IsTown)
			{

				if (settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != null && settlement.OwnerClan.Culture != settlement.Culture)
				{
					settlement.Culture = settlement.OwnerClan.Culture;
					if (removeTroops)
					{
						ChangeSettlementCulture.RemoveTroopsfromNotable(settlement);
					}
					foreach (Village boundVillage in settlement.BoundVillages)
					{
						if (removeTroops)
						{
							ChangeSettlementCulture.Transform(boundVillage.Settlement, true);
						}
						else
						{
							ChangeSettlementCulture.Transform(boundVillage.Settlement, false);
						}
					}
				}
			}
		}

		public static void RemoveTroopsfromNotable(Settlement settlement)
		{
			if ((settlement.IsTown || settlement.IsVillage) && settlement.Notables != null)
			{
				foreach (Hero notable in settlement.Notables)
				{
					if (notable.CanHaveRecruits)
					{
						for (int index = 0; index < 6; index++)
						{
							notable.VolunteerTypes[index] = null;
						}
					}
				}
			}
		}

		public void OnWeeklyTickSettlement(Settlement settlement)
		{
			if (this.WeekCounter.ContainsKey(settlement))
			{
				Dictionary<Settlement, int> dictionary = this.WeekCounter;
				dictionary[settlement]++;

				if (this.IsSettlementDue(settlement))
				{
					ChangeSettlementCulture.Transform(settlement, true);
				}
			}
		}

		public bool IsSettlementDue(Settlement settlement)
		{
			if (BannerlordTweaksSettings.Instance is { } settings && settings.TimeToChanceCulture > 0)
			{
				return this.WeekCounter[settlement] == settings.TimeToChanceCulture;
			}
			else
			{
				return false;
			}
		}

		public void AddCounter(Settlement settlement)
		{
			if (settlement.IsVillage || settlement.IsCastle || settlement.IsTown)
			{
				if (this.WeekCounter.ContainsKey(settlement))
				{
					this.WeekCounter[settlement] = 0;
				}
				else
				{
					this.WeekCounter.Add(settlement, 0);
				}
			}
		}

		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Settlement, int>>("SettlementCultureTransformation", ref this.WeekCounter);
		}


		private static Dictionary<Settlement, CultureObject> initialCultureDictionary = new Dictionary<Settlement, CultureObject>();
		private Dictionary<Settlement, int> WeekCounter = new Dictionary<Settlement, int>();
	}
}
