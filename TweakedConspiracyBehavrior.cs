using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using StoryMode.StoryModePhases;

namespace BannerlordTweaks
{
	public class ConspiracyQuestTimerTweak
	{
		public static void Apply(Campaign campaign)
		{
			var obj = new ConspiracyQuestTimerTweak();
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(obj, new Action(obj.ExtendDeadline));
		}

		private void ExtendDeadline()
		{
			if (Campaign.Current != null && Campaign.Current.QuestManager != null)
			{
				foreach (QuestBase questBase in Campaign.Current.QuestManager.Quests)
				{
					bool flag2 = questBase.GetName().ToString().StartsWith("stop_conspiracy_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(5f);
					if (flag2)
					{
						DebugHelpers.ColorGreenMessage("将停止阴谋任务延长1年.");
						questBase.ChangeQuestDueTime(CampaignTime.YearsFromNow(1f));
						DebugHelpers.ColorGreenMessage("新任务截止日期: " + questBase.QuestDueTime.ToString());
					}
					bool flag3 = questBase.StringId.StartsWith("conspiracy_quest_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(7f);
					if (flag3)
					{
						questBase.ChangeQuestDueTime(CampaignTime.WeeksFromNow(3f));
						DebugHelpers.ColorGreenMessage("BT 扩展阴谋调整：扩展阴谋任务.");
						float cStrngth = SecondPhase.Instance.ConspiracyStrength;
						if (cStrngth > 1000 && cStrngth > 250)
						{
							SecondPhase.Instance.DecreaseConspiracyStrength(150);
							DebugHelpers.ColorGreenMessage("BT 扩展阴谋调整：降低阴谋力量.");
						}

					}
				}
			}
		}
	}
}