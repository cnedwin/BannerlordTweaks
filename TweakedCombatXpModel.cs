// BannerlordTweaks.TweakedCombatXpModel
using System;
using BannerlordTweaks;
using MCM.Abstractions.Settings.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Library;

public class TweakedCombatXpModel : DefaultCombatXpModel
{
	public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject captain, CharacterObject attackedTroop, PartyBase party, int damage, bool isFatal, MissionTypeEnum missionType, out int xpAmount)
	{
		if (attackerTroop == null || attackedTroop == null)
		{
			xpAmount = 0;
			return;
		}
		int num = attackerTroop.MaxHitPoints();
		xpAmount = MBMath.Round(0.4f * ((attackedTroop.GetPower() + 0.5f) * (float)(Math.Min(damage, num) + (isFatal ? num : 0))));
		if (attackerTroop.IsHero)
		{
			switch (missionType)
			{
				case MissionTypeEnum.Tournament:
					if (GlobalSettings<BannerlordTweaksSettings>.Instance!.TournamentHeroExperienceMultiplierEnabled)
					{
						xpAmount = MathF.Round(GlobalSettings<BannerlordTweaksSettings>.Instance!.TournamentHeroExperienceMultiplier * (float)xpAmount);
					}
					else
					{
						xpAmount = MathF.Round((float)xpAmount * 0.33f);
					}
					break;
				case MissionTypeEnum.PracticeFight:
					if (GlobalSettings<BannerlordTweaksSettings>.Instance!.ArenaHeroExperienceMultiplierEnabled)
					{
						xpAmount = MathF.Round(GlobalSettings<BannerlordTweaksSettings>.Instance!.ArenaHeroExperienceMultiplier * (float)xpAmount);
					}
					else
					{
						xpAmount = MathF.Round((float)xpAmount * 0.0625f);
					}
					break;
			}
		}
		else if (missionType == MissionTypeEnum.Battle || missionType == MissionTypeEnum.SimulationBattle)
		{
			if (GlobalSettings<BannerlordTweaksSettings>.Instance!.TroopBattleSimulationExperienceMultiplierEnabled && missionType == MissionTypeEnum.SimulationBattle)
			{
				xpAmount = MathF.Round((float)xpAmount * GlobalSettings<BannerlordTweaksSettings>.Instance!.TroopBattleSimulationExperienceMultiplier);
			}
			else if (GlobalSettings<BannerlordTweaksSettings>.Instance!.TroopBattleExperienceMultiplierEnabled && missionType == MissionTypeEnum.Battle)
			{
				xpAmount = MathF.Round((float)xpAmount * GlobalSettings<BannerlordTweaksSettings>.Instance!.TroopBattleExperienceMultiplier);
			}
		}
	}
}
