using System;
using BannerlordTweaks;
using MCM.Abstractions.Settings.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

public class TweakedClanTierModel : DefaultClanTierModel
{
	public override int GetCompanionLimit(Clan clan)
	{
		if (GlobalSettings<BannerlordTweaksSettings>.Instance!.CompanionLimitTweakEnabled)
		{
			int tier = clan.Tier;
			int num = tier + 3;
			ExplainedNumber explainedNumber = new ExplainedNumber(num);
			if (clan.Leader.GetPerkValue(DefaultPerks.Leadership.WePledgeOurSwords))
			{
				explainedNumber.Add(DefaultPerks.Leadership.WePledgeOurSwords.PrimaryBonus);
			}
			return GlobalSettings<BannerlordTweaksSettings>.Instance!.CompanionBaseLimit + (int)explainedNumber.ResultNumber * GlobalSettings<BannerlordTweaksSettings>.Instance!.CompanionLimitBonusPerClanTier;
		}
		return base.GetCompanionLimit(clan);
	}

	public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
	{
		if (GlobalSettings<BannerlordTweaksSettings>.Instance!.ClanPartiesLimitTweakEnabled && clan == Clan.PlayerClan)
		{
			return GlobalSettings<BannerlordTweaksSettings>.Instance!.BaseClanPartiesLimit + (int)Math.Floor((float)clanTierToCheck * GlobalSettings<BannerlordTweaksSettings>.Instance!.ClanPartiesBonusPerClanTier);
		}
		return base.GetPartyLimitForTier(clan, clanTierToCheck);
	}
}
