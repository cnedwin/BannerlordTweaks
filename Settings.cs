using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace BannerlordTweaks
{
    public class BannerlordTweaksSettings : AttributeGlobalSettings<BannerlordTweaksSettings>
    {
        public override string Id { get; } = "BannerlordTweaksSettings";
        public override string DisplayName => "综合设置1.5.6.6 (cnedwin)";
        public override string FolderName { get; } = "BannerlordTweaksSettings";
        public override string FormatType { get; } = "json2";

        #region Miscellaneous

        [SettingPropertyBool("禁用军团士气计算", Order = 1, RequireRestart = false, HintText = "启用后,任务部队例如 \"军团\"计算团队士气时,将忽略您的团队.")]
        public bool QuestCharactersIgnorePartySize { get; set; } = false;

        [SettingPropertyBool("显示食物天数", Order = 2, RequireRestart = false, HintText = "原版显示食物数量而不是食物的天数(大地图UI的右下角")]
        public bool ShowFoodDaysRemaining { get; set; } = false;

        [SettingPropertyBool("启用“停止阴谋”任务计时器的自动扩展", Order = 4, RequireRestart = false, HintText = "由于TW尚未完成，因此自动延长“停止阴谋”任务的计时器。")]
        public bool TweakedConspiracyQuestTimerEnabled { get; set; } = true;

        #endregion

        #region Campaign Tweaks - Difficulty Settings

        [SettingPropertyBool("难度调整", Order = 1, RequireRestart = false, HintText = "允许您更改难度设置。这些选项会覆盖游戏设置菜单中的选项."), SettingPropertyGroup("难度调整")]
        public bool DifficultyTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("玩家承受伤害", Order = 1, RequireRestart = false, HintText = "允许您更改玩家受到伤害的乘数."), SettingPropertyGroup("难度调整/玩家承受伤害")]
        public bool DamageToPlayerTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("承受伤害倍数", 0.1f, 5.0f, HintText = "原生值：非常简单：0.3，简单：0.67，真实：1。这个值用来计算玩家受到的伤害"), SettingPropertyGroup("难度调整/玩家承受伤害")]
        public float DamageToPlayerMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("友军承受伤害", Order = 1, RequireRestart = false, HintText = "允许你改变玩家友军受到的伤害。"), SettingPropertyGroup("难度调整/友军承受伤害")]
        public bool DamageToFriendsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("友军承受伤害倍数", 0.1f, 5.0f, HintText = "本地值：非常简单：0.3分，简单：0.67分，现实：1。此值用于计算玩家朋友受到的伤害."), SettingPropertyGroup("难度调整/友军承受伤害")]
        public float DamageToFriendsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("玩家部队承受伤害", Order = 1, RequireRestart = false, HintText = "允许你改变玩家部队受到伤害的乘数。"), SettingPropertyGroup("难度调整/部队承受伤害")]
        public bool DamageToTroopsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("部队承受伤害倍数", 0.1f, 5.0f, HintText = "原生值：非常简单：0.3，简单：0.67，真实：1。此值用于计算对玩家部队的伤害。"), SettingPropertyGroup("难度调整/部队承受伤害")]
        public float DamageToTroopsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("AI难度调整", Order = 1, RequireRestart = false, HintText = "允许你改变人工智能的战斗难度."), SettingPropertyGroup("难度调整/AI难度调整")]
        public bool CombatAIDifficultyTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("AI难度调整倍数", 0.1f, 1.0f, HintText = "原生值：非常简单：0.1，简单：0.32，真实：0.96。此值用于计算人工智能战斗难度."), SettingPropertyGroup("难度调整/AI难度调整")]
        public float CombatAIDifficultyMultiplier { get; set; } = 0.96f;

        [SettingPropertyBool("地图移速调整", Order = 1, RequireRestart = false, HintText = "允许您更改玩家获得的奖励地图移动速度倍增."), SettingPropertyGroup("难度调整/玩家地图移动速度")]
        public bool PlayerMapMovementSpeedBonusTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("地图移速倍数", 0.0f, 2.0f, HintText = "原生值：非常简单：0.1，简单：0.05，真实：0。此值用于计算玩家的地图移动速度."), SettingPropertyGroup("难度调整/玩家地图移动速度")]
        public float PlayerMapMovementSpeedBonusMultiplier { get; set; } = 0.0f;

        #endregion

        #region Campaign Tweaks - Battle Size Tweak

        //[SettingPropertyBool("战斗规模调整", Order = 1, RequireRestart = false, HintText = "允许您在本机值之外设置作战规模限制。警告：将此值设置为1000以上可能会导致性能下降和崩溃"), SettingPropertyGroup("战斗规模调整")]
        //public bool BattleSizeTweakEnabled { get; set; } = false;

        //[SettingPropertyInteger("战斗人数上限", 2, 1300, HintText = "设定战场上部队人数的上限。警告：将此值设置为1000以上可能会导致性能下降和崩溃"), SettingPropertyGroup("战斗规模调整")]
        //public int BattleSize { get; set; } = 600;

        #endregion

        #region Battle Tweaks - Battle Reward Tweaks

        [SettingPropertyBool("战斗奖励调整", Order = 1, RequireRestart = false, HintText = "将设定倍数应用于声望和影响力获得的收益（仅适用于玩家）。"), SettingPropertyGroup("战斗奖励")]
        public bool BattleRewardTweaksEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("启用声望调整", 1f, 5f, HintText = "本机值为1.0。 您从战斗中获得的声望值乘以该值。"), SettingPropertyGroup("战斗奖励")]
        public float BattleRenownMultiplier { get; set; } = 2f;

        [SettingPropertyFloatingInteger("战斗影响力倍数", 0.1f, 5f, HintText = "本机值为1.0。 您从战斗中获得的影响力乘以该值。"), SettingPropertyGroup("战斗奖励")]
        public float BattleInfluenceMultiplier { get; set; } = 1f;

        [SettingPropertyBool("应用到AI", Order = 1, RequireRestart = false, HintText = "应用一样的倍数到AI部队身上"), SettingPropertyGroup("战斗奖励")]
        public bool BattleRewardApplyToAI { get; set; } = true;

        #endregion

        #region Battle Tweaks - Hideout Tweaks

        [SettingPropertyBool("启用藏身处战斗行为", Order = 1, RequireRestart = false, HintText = "改变躲藏战中的游戏行为."), SettingPropertyGroup("藏身处战斗调整")]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("剿匪部队上限", 5, 90, HintText = "游戏默认值为9或10,藏身处部队限制,不能超过90,因为它会引起错误."), SettingPropertyGroup("藏身处战斗调整")]
        public int HideoutBattleTroopLimit { get; set; } = 90;

        [SettingPropertyBool("玩家死亡继续", Order = 1, RequireRestart = false, HintText = "游戏默认值为关闭.启用此选项后,如果死去,您将不会自动输掉藏身战斗.您的部队将冲锋,而上司决斗将被禁用."), SettingPropertyGroup("藏身处战斗调整")]
        public bool ContinueHideoutBattleOnPlayerDeath { get; set; } = false;

        [SettingPropertyBool("决斗失败继续", Order = 1, RequireRestart = false, HintText = "游戏默认值为关闭.如果启用,决斗失败,您将输掉战斗.如果禁用,则您的部队将继续战斗."), SettingPropertyGroup("藏身处战斗调整")]
        public bool ContinueHideoutBattleOnPlayerLoseDuel { get; set; } = true;

        #endregion

        #region Battle Tweaks - Siege Tweaks

        [SettingPropertyBool("启用攻城调整", Order = 1, RequireRestart = false, IsToggle = true, HintText = "启用攻城调整."), SettingPropertyGroup("攻城建设")]
        public bool SiegeTweaksEnabled { get; set; } = false;


        [SettingPropertyBool("攻城器建造调整", Order = 1, RequireRestart = false, HintText = "增加围攻工程进度调整"), SettingPropertyGroup("攻城建设")]
        public bool SiegeConstructionProgressPerDayMultiplierEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("每天攻城建造进度", 0.5f, 10.0f, HintText = "游戏默认值为1.0,对每日施工进度增加一个倍率,较小的数字将导致更长的围攻时间."), SettingPropertyGroup("攻城建设")]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 0.8f;


        [SettingPropertyBool("攻城伤亡调整", Order = 1, RequireRestart = false, HintText = "更改在战役地图上攻城阶段用于计算居民伤亡的值."), SettingPropertyGroup("攻城建设")]
        public bool SiegeCasualtiesTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("攻城附加伤害", 1, 3, HintText = "游戏默认值为2.0,更改在战役地图围攻阶段用于计算附带伤亡人数的值."), SettingPropertyGroup("攻城建设")]
        public int SiegeCollateralDamageCasualties { get; set; } = 1;

        [SettingPropertyInteger("攻城人员伤亡", 3, 7, HintText = "游戏默认值为5.0,更改在战役地图围攻阶段用于计算附带伤亡人数的值"), SettingPropertyGroup("攻城建设")]
        public int SiegeDestructionCasualties { get; set; } = 4;

        #endregion

        #region Battle Tweaks - Troop Battle Experience Tweaks

        [SettingPropertyBool("启用部队经验调整", Order = 1, RequireRestart = false, HintText = "军队在战斗中获得的经验值倍率(注：军队,而不是英雄)."), SettingPropertyGroup("部队经验")]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("部队经验修改", 1f, 6f, HintText = "本机值为1.0.增加所有部队在战斗中获得的经验(注意:只有部队,没有英雄).不适用于模拟战斗)."), SettingPropertyGroup("部队经验")]
        public float TroopBattleExperienceMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("启用模拟经验", Order = 1, RequireRestart = false, HintText = "提供从模拟战斗中获得经验的乘数.这适用于战役地图上的所有战斗(包括NPC战斗)."), SettingPropertyGroup("部队经验")]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("模拟战斗经验", 0.5f, 8f, HintText = "本机值为1.0.提供从模拟战斗中获得经验的乘数.这适用于战役地图上的所有模拟战斗."), SettingPropertyGroup("部队经验")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 1.0f;

        #endregion

        #region Battle Tweaks - Weapon Cut Through Tweaks

        [SettingPropertyBool("双手武器穿透", Order = 1, RequireRestart = false, HintText = "允许所有双手武器类型切断并击中多人."), SettingPropertyGroup("无双调整")]
        public bool TwoHandedWeaponsSliceThroughEnabled { get; set; } = false;

        [SettingPropertyBool("单手武器穿透", Order = 1, RequireRestart = false, HintText = "允许所有单手武器类型切断并击中多人."), SettingPropertyGroup("无双调整")]
        public bool SingleHandedWeaponsSliceThroughEnabled { get; set; } = false;

        #endregion

        #region Character Tweaks - Age Tweaks

        [SettingPropertyBool("启用年龄调整", RequireRestart = false, HintText = "允许调整角色年龄行为."), SettingPropertyGroup("年龄调整")]
        public bool AgeTweaksEnabled { get; set; } = false;

        [SettingPropertyInteger("婴儿时期", 0, 125, HintText = "默认是3.", Order = 0), SettingPropertyGroup("年龄调整")]
        public int BecomeInfantAge { get; set; } = 3;

        [SettingPropertyInteger("儿童时期", 0, 125, HintText = "默认是6.", Order = 1), SettingPropertyGroup("年龄调整")]
        public int BecomeChildAge { get; set; } = 6;

        [SettingPropertyInteger("青少时期", 0, 125, HintText = "默认是14.", Order = 2), SettingPropertyGroup("年龄调整")]
        public int BecomeTeenagerAge { get; set; } = 14;

        [SettingPropertyInteger("成人时期", 0, 100, HintText = "默认是18.", Order = 3), SettingPropertyGroup("年龄调整")]
        public int HeroComesOfAge { get; set; } = 18;

        [SettingPropertyInteger("老年时期", 0, 125, HintText = "默认是47.", Order = 4), SettingPropertyGroup("年龄调整")]
        public int BecomeOldAge { get; set; } = 47;

        [SettingPropertyInteger("最大年龄", 0, 125, HintText = "默认是125.", Order = 5), SettingPropertyGroup("年龄调整")]
        public int MaxAge { get; set; } = 125;

        #endregion

        #region Character Tweaks - Attribute Focus Point Tweaks

        [SettingPropertyBool("启用属性点调整", Order = 1, RequireRestart = false, HintText = "更改用于计算英雄升级获得多少属性点和专精点的值。"), SettingPropertyGroup("属性专精调整")]
        public bool AttributeFocusPointTweakEnabled { get; set; } = false;


        [SettingPropertyInteger("升几级获得属性", 1, 5, HintText = "本机值为4。你需要获得多少等级才能获得属性点."), SettingPropertyGroup("属性专精调整")]
        public int AttributePointRequiredLevel { get; set; } = 4;


        [SettingPropertyInteger("每级获得专精点", 1, 5, HintText = "本机值为1。这是每个级别获得的焦点点数."), SettingPropertyGroup("属性专精调整")]
        public int FocusPointsPerLevel { get; set; } = 1;

        #endregion

        #region Character Tweaks - Hero Skill Multiplier Tweaks

        [SettingPropertyBool("启用英雄技能经验倍数", Order = 1, RequireRestart = false, HintText = "对技能获得的经验量应用乘数.只影响玩家."), SettingPropertyGroup("技能经验")]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyBool("启用同伴技能经验倍数", Order = 1, RequireRestart = false, HintText = "将乘数乘以所获得的技能经验。 仅影响玩家."), SettingPropertyGroup("技能经验")]
        public bool CompanionSkillExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("英雄技能经验倍数", 1f, 5f, HintText = "对技能获得的经验量应用乘数.只影响玩家."), SettingPropertyGroup("技能经验")]
        public float HeroSkillExperienceMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("同伴技能经验倍数", 1f, 20f, RequireRestart = false, HintText = "将乘数乘以所获得的技能经验。 仅影响同伴."), SettingPropertyGroup("技能经验")]
        public float CompanionSkillExperienceMultiplier { get; set; } = 1f;

        #endregion

        #region Character Tweaks - Pregnancy Tweaks

        [SettingPropertyBool("禁止孕产死胎", Order = 1, RequireRestart = false, HintText = "禁止孕妇生产时发生难产和死胎."), SettingPropertyGroup("生育调整")]
        public bool NoStillbirthsTweakEnabled { get; set; } = false;

        [SettingPropertyBool("禁止孕妇死亡", Order = 1, RequireRestart = false, HintText = "使母亲在分娩时死亡的机会丧失."), SettingPropertyGroup("生育调整")]
        public bool NoMaternalMortalityTweakEnabled { get; set; } = false;

        [SettingPropertyBool("启用妊娠天数调整", Order = 1, RequireRestart = false, HintText = "调整怀孕持续的时间."), SettingPropertyGroup("生育调整/妊娠天数")]
        public bool PregnancyDurationTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("妊娠持续天数", 1, 96, HintText = "默认是36天."), SettingPropertyGroup("生育调整/妊娠天数")]
        public int PregnancyDuration { get; set; } = 36;

        [SettingPropertyBool("启用性别调整", Order = 1, RequireRestart = false, HintText = "允许调整出生性别比例."), SettingPropertyGroup("生育调整/生女娃概率")]
        public bool FemaleOffspringProbabilityTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("女孩出生比例", -1.0f, 1.0f, HintText = "本机值为0.51。设置为-1将禁用女性生育."), SettingPropertyGroup("生育调整/生女娃概率")]
        public float FemaleOffspringProbability { get; set; } = 0.51f;

        [SettingPropertyBool("启用双胞胎", Order = 1, RequireRestart = false, HintText = "允许调整生双胞胎的机会，人工受精啦！"), SettingPropertyGroup("生育调整/双胞胎概率")]
        public bool TwinsProbabilityTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("双胞胎的概率", -1.0f, 1.0f, HintText = "本机值为0.03。决定生双胞胎的机会"), SettingPropertyGroup("生育调整/双胞胎概率")]
        public float TwinsProbability { get; set; } = 0.03f;

        [SettingPropertyBool("启用怀孕几率调整", Order = 1, RequireRestart = false, HintText = "启用此功能将完全覆盖每日妊娠检查。 以下所有设置将被应用！"), SettingPropertyGroup("生育调整/怀孕几率调整")]
        public bool DailyChancePregnancyTweakEnabled { get; set; } = false;

        [SettingPropertyBool("玩家可以生育", Order = 1, RequireRestart = false, HintText = "默认: true. 如果设置为 false, 玩家将不能用有孩子"), SettingPropertyGroup("生育调整/怀孕几率调整")]
        public bool PlayerCharacterFertileEnabled { get; set; } = true;

        [SettingPropertyInteger("最小怀孕年龄", 0, 999, HintText = "英雄怀孕的最小年龄。本地：18.", RequireRestart = false), SettingPropertyGroup("生育调整/怀孕几率调整")]
        public int MinPregnancyAge { get; set; } = 18;

        [SettingPropertyInteger("最大怀孕年龄", 0, 999, HintText = "英雄怀孕的最大年龄。本地：45.", RequireRestart = false), SettingPropertyGroup("生育调整/怀孕几率调整")]
        public int MaxPregnancyAge { get; set; } = 45;

        [SettingPropertyBool("启用氏族生育奖励", Order = 1, RequireRestart = false, HintText = "为您的氏族成员增加奖励以使其怀孕。"), SettingPropertyGroup("生育调整/怀孕几率调整/氏族生育奖励")]
        public bool ClanFertilityBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("氏族生育奖励", 1f, 5f, RequireRestart = false, HintText = "为您的氏族成员增加％的怀孕几率. 1 = 无奖励, 2 = 2倍几率, 5 = 5倍几率. 提示: 由于基础怀孕的计算，可能在〜6-8个孩子之后做的不多."), SettingPropertyGroup("生育调整/怀孕几率调整/氏族生育奖励")]
        public float ClanFertilityBonus { get; set; } = 1.1f;

        [SettingPropertyBool("启用孩子上限调整", Order = 1, RequireRestart = false, HintText = "允许设置可以获取孩子的最大数目"), SettingPropertyGroup("生育调整/怀孕几率调整/孩子上限")]
        public bool MaxChildrenTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("最大孩子数量", 0, 999, HintText = "任何人可以拥有的最大孩子数。默认值：5", RequireRestart = false), SettingPropertyGroup("生育调整/怀孕几率调整/孩子上限")]
        public int MaxChildren { get; set; } = 5;

        #endregion

        #region Clan Tweaks - Companion Limit Tweak

        [SettingPropertyBool("启用同伴上限调整", Order = 1, RequireRestart = false, HintText = "设置基础同伴限制和每个部落等级获得的奖励"), SettingPropertyGroup("同伴数量")]
        public bool CompanionLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("基础同伴上限", 1, 50, HintText = "游戏默认值为3,设置基础同伴数量限制."), SettingPropertyGroup("同伴数量")]
        public int CompanionBaseLimit { get; set; } = 3;

        [SettingPropertyInteger("氏族等级奖励", 0, 10, HintText = "默认值为1。将奖励设置为每个氏族等级的同伴限制。这个值乘以你的氏族等级.请注意，领导能力“我们誓言”的特权也会使此数字增加1。"), SettingPropertyGroup("同伴数量")]
        public int CompanionLimitBonusPerClanTier { get; set; } = 3;

        [SettingPropertyBool("启用无限流浪者补丁", Order = 1, RequireRestart = false, HintText = "取消可产生的最大潜在同伴数量的上限。 本地人将流浪者的数量限制为〜25。 这将消除该限制。 注意：由于要在生成新游戏时设置上限，因此需要新的战役才能生效。"), SettingPropertyGroup("同伴数量")]
        public bool UnlimitedWanderersPatch { get; set; } = false;


        #endregion

        #region Clan Tweaks - Clan Parties Tweaks

        [SettingPropertyBool("启用氏族部队调整", Order = 1, RequireRestart = false, HintText = "更改氏族部队的基本人数基础数量以及升级加成"), SettingPropertyGroup("氏族调整/氏族部队")]
        public bool ClanPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("基础氏族部队上限", 1, 10, HintText = "游戏默认值为1,基础部队数量."), SettingPropertyGroup("氏族调整/氏族部队")]
        public int BaseClanPartiesLimit { get; set; } = 2;

        [SettingPropertyFloatingInteger("氏族等级奖励", 0.0f, 3f, HintText = "每个氏族等级,奖励部队数量"), SettingPropertyGroup("氏族调整/氏族部队")]
        public float ClanPartiesBonusPerClanTier { get; set; } = 0.5f;

        [SettingPropertyBool("启用AI家族部队调整", Order = 1, RequireRestart = false, IsToggle = true, HintText = "更改AI 领主可以参加的部队人数."), SettingPropertyGroup("氏族调整/氏族部队/AI领主部队调整")]
        public bool AIClanPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyBool("也调整小家族势力", Order = 1, RequireRestart = false, HintText = "更改AI 小势力领主可以参加的部队的基本人数。 [根据氏族等级，原版是1-4。]"), SettingPropertyGroup("氏族调整/氏族部队/AI领主部队调整")]
        public bool AIMinorClanPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("增加AI家族部队上限", 1, 10, Order = 1, RequireRestart = false, HintText = "这将增加AI Lords可以带领军队的领主基本人数。 [第3层及以下的本地为1，在T4处为2，在T5及更高处为3。]除非包括以下选项，否则不包括小派系."), SettingPropertyGroup("氏族调整/氏族部队/AI领主部队调整")]
        public int BaseAIClanPartiesLimit { get; set; } = 0;

        [SettingPropertyBool("调整自定义生成部队", Order = 2, RequireRestart = false, IsToggle = true, HintText = "更改自定义生成领主可以使用的方的基本数目。 [根据氏族等级，当地人是1-4。]"), SettingPropertyGroup("氏族调整/氏族部队/自定义生成部队调整")]
        public bool AICustomSpawnPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("增加自定义生成部队的上限", 1, 10, Order = 1, RequireRestart = false, HintText = "这将增加“定制领主”可以组建军队的领主基本人数。 [第3层及以下的本地为1，在T4处为2，在T5及更高处为3。]除非包括以下选项，否则不包括小派系."), SettingPropertyGroup("氏族调整/氏族部队/自定义生成部队调整")]
        public int BaseAICustomSpawnPartiesLimit { get; set; } = 0;

        #endregion

        #region Crafting Tweaks - Crafting Stamina Tweaks

        [SettingPropertyBool("锻造耐力调整", Order = 1, RequireRestart = false, HintText = "影响制作耐力的调整."), SettingPropertyGroup("锻造调整")]
        public bool CraftingStaminaTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("锻造最大耐力", 100, 1000, HintText = "游戏默认值为400,设置最大耐力值."), SettingPropertyGroup("锻造调整")]
        public int MaxCraftingStamina { get; set; } = 400;

        [SettingPropertyInteger("小时耐力回复", 0, 100, HintText = "每小时回复点数,游戏默认5."), SettingPropertyGroup("锻造调整")]
        public int CraftingStaminaGainAmount { get; set; } = 10;

        [SettingPropertyBool("无视锻造耐力", Order = 1, RequireRestart = false, HintText = "游戏默认值为关闭,您仍然会失去锻造耐力,但是当您达到零时,您仍然可以制作."), SettingPropertyGroup("锻造调整")]
        public bool IgnoreCraftingStamina { get; set; } = false;

        [SettingPropertyFloatingInteger("野外耐力回复", 0f, 1f, HintText = "游戏默认值为关闭,您仍然会失去锻造耐力,但是当您达到零时,您仍然可以制作."), SettingPropertyGroup("锻造调整")]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 1f;

        #endregion

        #region Crafting Tweaks - Smelting Tweaks

        [SettingPropertyBool("隐藏锁定武器", Order = 1, RequireRestart = false, HintText = "游戏默认false. 启用用来防止你锁定的武器出现在熔炼列表."), SettingPropertyGroup("熔炼调整")]
        public bool PreventSmeltingLockedItems { get; set; } = true;


        [SettingPropertyBool("熔炼解锁部件", Order = 1, RequireRestart = false, HintText = "游戏默认关闭. 启用则可以从所熔炼的武器中解锁这个武器的部件."), SettingPropertyGroup("熔炼调整")]
        public bool AutoLearnSmeltedParts { get; set; } = true;

        #endregion

        #region Kingdom Tweaks - Lord Bartering Tweaks

        [SettingPropertyBool("交易调整", Order = 1, RequireRestart = false, HintText = "启用影响物物交换的调整（婚姻，加入您的阵营等）.)"), SettingPropertyGroup("交易调整")]
        public bool BarterablesTweaksEnabled { get; set; } = true;

        [SettingPropertyInteger("派系加入的交易调整", 1, 200, Order = 0, RequireRestart = false, HintText = "调整摇摆派系加入您的王国的费用百分比。 原始值为100％（不变）。 50 =降低50％的成本。 150 =成本增加50％，依此类推。"), SettingPropertyGroup("交易调整")]
        public int BarterablesJoinKingdomAsClanAdjustment { get; set; } = 100;

        [SettingPropertyBool("使用派系加入交易的替代公式", Order = 1, RequireRestart = false, HintText = "应用替代公式来计算摇摆派系加入您的王国的成本，重点更多放在关系上。 [与主的关系越高，以物易物越便宜]。"), SettingPropertyGroup("交易调整")]
        public bool BarterablesJoinKingdomAsClanAltFormulaEnabled { get; set; } = false;


        #endregion

        #region Party Tweaks - Army Tweaks

        [SettingPropertyBool("调整部队生成规模", Order = 0, RequireRestart = true, HintText = "调整领主战败重生后生成的部队规模."), SettingPropertyGroup("部队调整/部队调整")]
        public bool Strategy_ModifyRespawnParty { get; set; }

        [SettingPropertyInteger("AI领主部队重生规模", 0, 200, "0", Order = 1, RequireRestart = false, HintText = "调整AI领主战败重生后生成的部队规模."), SettingPropertyGroup("部队调整/部队调整")]
        public int Strategy_ModifyRespawnParty_AILordPartySizeOnRespawn { get; set; } = 3;

        [SettingPropertyInteger("玩家部队重生规模", 0, 200, "0", Order = 2, RequireRestart = false, HintText = "调整玩家氏族领主战败重生后生成的部队规模."), SettingPropertyGroup("部队调整/部队调整")]
        public int Strategy_ModifyRespawnParty_PlayerPartySizeOnRespawn { get; set; }

        #endregion

        #region Party Tweaks - Caravan Tweaks

        [SettingPropertyBool("启用玩家商队部队规模调整", Order = 1, RequireRestart = false, HintText = "将配置的值应用于您的商队规模"), SettingPropertyGroup("玩家商队部队规模调整")]
        public bool PlayerCaravanPartySizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("主角商队规模", 30, 100, HintText = "默认30"), SettingPropertyGroup("玩家商队部队规模调整")]
        public int PlayerCaravanPartySize { get; set; } = 30;

        #endregion

        #region Party Tweaks - Party Size Tweaks

        [SettingPropertyBool("启用部队规模奖励", Order = 1, RequireRestart = false, HintText = "根据你的统御和管理技能等级，增加可带领的部队人数."), SettingPropertyGroup("部队规模奖励")]
        public bool PartySizeTweakEnabled { get; set; } = true;

        [SettingPropertyBool("启用统御奖励", Order = 1, RequireRestart = false, HintText = "将相当于您的统御技能的设定百分比的奖励应用于您的团队规模."), SettingPropertyGroup("部队规模奖励")]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("统御等级奖励", 0f, 1f, HintText = "将相当于您的领导技能的设定百分比的奖金应用于您的政党规模."), SettingPropertyGroup("部队规模奖励")]
        public float LeadershipPartySizeBonus { get; set; } = 0.3f;

        [SettingPropertyBool("启用管理奖励", Order = 1, RequireRestart = false, HintText = "将相当于您的管理技能的设定百分比的奖励应用于您的团队规模."), SettingPropertyGroup("部队规模奖励")]
        public bool StewardPartySizeBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("管理等级奖励", 0f, 1f, HintText = "将相当于您的管理技能的设定百分比的奖励应用于您的团队规模."), SettingPropertyGroup("部队规模奖励")]
        public float StewardPartySizeBonus { get; set; } = 0.3f;

        #endregion

        #region Party Tweaks - Party Wage Tweaks

        [SettingPropertyBool("启用工资调整", Order = 1, RequireRestart = false, HintText = "允许您降低/增加各个群体的工资。[实验性]"), SettingPropertyGroup("工资调整")]
        public bool PartyWageTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("部队工资调整", .05f, 5f, HintText = "将部队的工资调整为当地价值的百分比。 最低5％（.05），最高500％（5.00）[本地100％（1.00）]", Order = 0, RequireRestart = false), SettingPropertyGroup("工资调整")]
        public float PartyWagePercent { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("驻军工资调整", .05f, 5f, HintText = "将驻军工资调整为当地价值的％。 最低5％（.05），最高500％（5.00）[本地100％（1.00）]", Order = 2, RequireRestart = false), SettingPropertyGroup("工资调整")]
        public float GarrisonWagePercent { get; set; } = 1.0f;

        [SettingPropertyBool("应用工资调整到你的势力", Order = 1, RequireRestart = false, HintText = "工资调整也适用于您的氏族/派系"), SettingPropertyGroup("工资调整")]
        public bool ApplyWageTweakToFaction { get; set; } = false;

        #endregion

        #region Party Tweaks - Troop Daily Experience Tweak

        [SettingPropertyBool("部队获得经验", Order = 1, RequireRestart = false, HintText = "根据领主的统御技能，每天给予部队一定的经验。默认情况下仅适用于玩家。."), SettingPropertyGroup("每日部队经验")]
        public bool DailyTroopExperienceTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("统御奖励", 0.01f, 5f, HintText = "根据领主的统御技能，每天给予部队一定的经验。默认情况下仅适用于玩家。"), SettingPropertyGroup("每日部队经验")]
        public float LeadershipPercentageForDailyExperienceGain { get; set; } = 0.5f;

        [SettingPropertyBool("应用于玩家的氏族", Order = 1, RequireRestart = false, HintText = "将每日获得的部队经验也应用于玩家的氏族成员."), SettingPropertyGroup("每日部队经验")]
        public bool DailyTroopExperienceApplyToPlayerClanMembers { get; set; } = false;

        [SettingPropertyBool("应用于所有AI领主", Order = 1, RequireRestart = false, HintText = "将每日部队经验值应用于所有NPC领主."), SettingPropertyGroup("每日部队经验")]
        public bool DailyTroopExperienceApplyToAllNPC { get; set; } = false;

        [SettingPropertyBool("经验消息显示", Order = 1, RequireRestart = false, HintText = "显示一条消息，显示获得的经验量."), SettingPropertyGroup("每日部队经验")]
        public bool DisplayMessageDailyExperienceGain { get; set; } = false;

        [SettingPropertyInteger("需要统御等级", 1, 200, HintText = "开始给部队提供经验所需的统御技能等级."), SettingPropertyGroup("每日部队经验")]
        public int DailyTroopExperienceRequiredLeadershipLevel { get; set; } = 30;

        #endregion

        #region Prisoner Tweaks

        [SettingPropertyBool("启用牢狱调整", Order = 1, RequireRestart = false, HintText = "增加贵族试图逃脱监禁前的最短时间."), SettingPropertyGroup("监禁调整")]
        public bool PrisonerImprisonmentTweakEnabled { get; set; } = false;

        [SettingPropertyBool("仅限玩家俘虏", Order = 1, RequireRestart = false, HintText = "这个调整是否只适用于玩家关押的囚犯."), SettingPropertyGroup("监禁调整")]
        public bool PrisonerImprisonmentPlayerOnly { get; set; } = true;

        [SettingPropertyInteger("囚禁最少天数", 0, 180, HintText = "领主在试图逃跑前被囚禁的最少天数."), SettingPropertyGroup("监禁调整")]
        public int MinimumDaysOfImprisonment { get; set; } = 10;

        [SettingPropertyBool("启用失踪囚犯英雄修复", Order = 2, HintText = "将尝试检测并释放可能被打扰且不会重生的囚犯英雄。 将在“最低监禁天数”设置后3天触发。"), SettingPropertyGroup("监禁调整")]
        public bool EnableMissingHeroFix { get; set; } = true;

        [SettingPropertyBool("启用囚犯人数奖励", Order = 1, RequireRestart = false, HintText = "使您的队伍的最大囚犯人数固定增加％."), SettingPropertyGroup("监禁调整/囚犯数量")]
        public bool PrisonerSizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("囚犯人数奖励", 0, 500, Order = 0, RequireRestart = false, HintText = "为您队伍的最大监狱人数增加固定的％奖励. 10% = 10, +50% = 50, etc. [默认 0]"), SettingPropertyGroup("监禁调整/囚犯数量")]
        public float PrisonerSizeTweakPercent { get; set; } = 0;

        [SettingPropertyBool("启用囚犯确认调整", Order = 1, RequireRestart = false, IsToggle = true, HintText = "修改基本游戏的合格率，加快/降低招募囚犯的速率"), SettingPropertyGroup("监禁调整/囚犯确认调整")]
        public bool PrisonerConformityTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("囚犯确认奖励", 0f, 10f, Order = 1, RequireRestart = false, HintText = "向每小时产生的合格性增加固定的％奖励。 10％= .1，+ 50％= .5，依此类推。[本地为0]"), SettingPropertyGroup("监禁调整/囚犯确认调整")]
        public float PrisonerConformityTweakBonus { get; set; } = 0;

        [SettingPropertyBool("将战俘确认调整应用于氏族", Order = 1, RequireRestart = false, HintText = "也适用于所有氏族部队的囚犯整合调整."), SettingPropertyGroup("监禁调整/囚犯确认调整")]
        public bool PrisonerConformityTweaksApplyToClan { get; set; } = false;

        [SettingPropertyBool("将囚犯确认调整应用于AI", Order = 1, RequireRestart = false, HintText = "将囚犯整合调整应用于所有方，包括AI领主主."), SettingPropertyGroup("监禁调整/囚犯确认调整")]
        public bool PrisonerConformityTweaksApplyToAi { get; set; } = false;



        #endregion

        #region Settlement Tweaks - Disable Troop Donations

        [SettingPropertyBool("禁止部队捐赠", Order = 1, RequireRestart = false, IsToggle = true, HintText = "禁止您的氏族（以及可选的王国）派遣部队向自己的住所捐款."), SettingPropertyGroup("定居点建筑调整/禁止部队捐赠")]
        public bool DisableTroopDonationPatchEnabled { get; set; } = false;

        [SettingPropertyBool("禁止部队捐赠 - 应用到王国", Order = 1, RequireRestart = false, HintText = "将禁用部队捐赠扩展到整个王国的拥有的定居点."), SettingPropertyGroup("定居点建筑调整/禁止部队捐赠")]
        public bool DisableTroopDonationFactionWideEnabled { get; set; } = false;

        #endregion

        #region Settlement Tweaks - Settlement Buildings Tweaks - Castle Buildings Tweaks

        [SettingPropertyBool("启用城堡训练场调整", Order = 1, RequireRestart = false, HintText = "改变培训场为每个级别提供的经验"), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡训练场调整")]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城堡训练场1级经验", 1, 150, HintText = "游戏默认值为1.更改培训级别在1级提供的经验"), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡训练场调整")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 30;

        [SettingPropertyInteger("城堡训练场2级经验", 2, 200, HintText = "游戏默认值为2.更改培训级别在2级提供的经验."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡训练场调整")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 70;

        [SettingPropertyInteger("城堡训练场3级经验", 3, 250, HintText = "游戏默认值为3.更改第3级培训领域提供的经验."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡训练场调整")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 150;


        [SettingPropertyBool("启用城堡粮仓调整", Order = 1, RequireRestart = false, HintText = "更改城堡粮仓每级提供的食物存储量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡粮仓调整")]
        public bool CastleGranaryBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城堡1级粮仓存储", 10, 90, HintText = "游戏默认值为10,更改城堡粮仓在1级提供的食物存储量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡粮仓调整")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 30;

        [SettingPropertyInteger("城堡2级粮仓存储", 20, 180, HintText = "游戏默认值为20,更改城堡粮仓在1级提供的食物存储量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡粮仓调整")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 45;

        [SettingPropertyInteger("城堡3级粮仓存储", 30, 270, HintText = "游戏默认值为30,更改城堡粮仓在1级提供的食物存储量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡粮仓调整")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 60;


        [SettingPropertyBool("启用城堡花园调整", Order = 1, RequireRestart = false, HintText = "改变城堡花园每级生产的食物数量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡花园调整")]
        public bool CastleGardensBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城堡1级花园食物产出", 1, 10, HintText = "游戏默认值为1,在2级城堡花园生产的食物量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡花园调整")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 3;

        [SettingPropertyInteger("城堡2级花园食物产出", 2, 20, HintText = "游戏默认值为2,在2级城堡花园生产的食物量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡花园调整")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 6;

        [SettingPropertyInteger("城堡3级花园食物产出", 3, 30, HintText = "游戏默认值为3,在2级城堡花园生产的食物量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡花园调整")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 9;


        [SettingPropertyBool("启用城堡民兵营调整", Order = 1, RequireRestart = false, HintText = "城堡民兵营民兵生产加成"), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡民兵营调整")]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城堡1级民兵营", 1, 10, HintText = "默认1,1级民兵营生产民兵数量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡民兵营调整")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 2;

        [SettingPropertyInteger("城堡2级民兵营", 1, 14, HintText = "默认2,1级民兵营生产民兵数量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡民兵营调整")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 4;

        [SettingPropertyInteger("城堡3级民兵营", 1, 16, HintText = "默认4,1级民兵营生产民兵数量."), SettingPropertyGroup("定居点建筑调整/城堡建筑调整/城堡民兵营调整")]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 8;

        #endregion

        #region Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks

        [SettingPropertyBool("启用城镇训练场奖励", Order = 1, RequireRestart = false, HintText = "改变培训场为每个级别提供的经验."), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇训练场调整")]
        public bool TownTrainingFieldsBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城镇训练场1级经验", 1, 150, HintText = "游戏默认值为1.更改培训级别在1级提供的经验."), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇训练场调整")]
        public int TownTrainingFieldsXpAmountLevel1 { get; set; } = 30;

        [SettingPropertyInteger("城镇训练场2级经验", 2, 200, HintText = "游戏默认值为2.更改培训级别在1级提供的经验."), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇训练场调整")]
        public int TownTrainingFieldsXpAmountLevel2 { get; set; } = 70;

        [SettingPropertyInteger("城镇训练场3级经验", 3, 300, HintText = "游戏默认值为3.更改培训级别在1级提供的经验."), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇训练场调整")]
        public int TownTrainingFieldsXpAmountLevel3 { get; set; } = 150;


        [SettingPropertyBool("启用城镇粮仓调整", Order = 1, RequireRestart = false, HintText = "更改城镇粮仓每级提供的食物存储量."), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇粮仓调整")]
        public bool TownGranaryBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城镇1级粮仓存储", 10, 900, HintText = "游戏默认值为200,更改城堡粮仓在1级提供的食物存储量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇粮仓调整")]
        public int TownGranaryStorageAmountLevel1 { get; set; } = 400;

        [SettingPropertyInteger("城镇2级粮仓存储", 20, 1800, HintText = "游戏默认值为400,更改城堡粮仓在1级提供的食物存储量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇粮仓调整")]
        public int TownGranaryStorageAmountLevel2 { get; set; } = 600;

        [SettingPropertyInteger("城镇3级粮仓存储", 30, 2700, HintText = "游戏默认值为600,更改城堡粮仓在1级提供的食物存储量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇粮仓调整")]
        public int TownGranaryStorageAmountLevel3 { get; set; } = 900;


        [SettingPropertyBool("启用城镇果园调整", Order = 1, RequireRestart = false, HintText = "改变城镇果园每级的食物产量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇果园调整")]
        public bool TownOrchardsBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城镇果园生产1级", 10, 100, HintText = "本机值为10。改变城镇果园1级时生产的食物量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇果园调整")]
        public int TownOrchardsFoodProductionAmountLevel1 { get; set; } = 45;

        [SettingPropertyInteger("城镇果园生产2级", 20, 200, HintText = "本机值为20。改变城镇果园1级时生产的食物量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇果园调整")]
        public int TownOrchardsFoodProductionAmountLevel2 { get; set; } = 60;

        [SettingPropertyInteger("城镇果园生产3级", 30, 300, HintText = "本机值为30。改变城镇果园1级时生产的食物量"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇果园调整")]
        public int TownOrchardsFoodProductionAmountLevel3 { get; set; } = 75;


        [SettingPropertyBool("启用城镇民兵营调整", Order = 1, RequireRestart = false, HintText = "改变城镇民兵营房每级提供的民兵产量."), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇民兵营调整")]
        public bool TownMilitiaBarracksBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("城镇民兵生产1级", 1, 15, HintText = "本机值为1。改变城镇民兵营房1级时提供的民兵生产量。"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇民兵营调整")]
        public int TownMilitiaBarracksAmountLevel1 { get; set; } = 2;

        [SettingPropertyInteger("城镇民兵生产2级", 1, 20, HintText = "本机值为2。改变城镇民兵营房1级时提供的民兵生产量。"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇民兵营调整")]
        public int TownMilitiaBarracksAmountLevel2 { get; set; } = 4;

        [SettingPropertyInteger("城镇民兵生产3级", 1, 30, HintText = "本机值为3。改变城镇民兵营房1级时提供的民兵生产量。"), SettingPropertyGroup("定居点建筑调整/城镇建筑调整/城镇民兵营调整")]
        public int TownMilitiaBarracksAmountLevel3 { get; set; } = 5;

        #endregion

        #region Settlement Tweaks - Settlement Food Bonus

        [SettingPropertyBool("启用定居点食物奖励", Order = 1, RequireRestart = false, HintText = "城镇、城堡的食品生产提供加成."), SettingPropertyGroup("定居点食物奖励")]
        public bool SettlementFoodBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("城堡粮食加成", 0f, 10f, HintText = "游戏默认值为0,为城堡的粮食生产加成."), SettingPropertyGroup("定居点食物奖励")]
        public float CastleFoodBonus { get; set; } = 2f;

        [SettingPropertyFloatingInteger("城镇粮食加成", 0f, 10f, HintText = "游戏默认值为0,为城镇的粮食生产加成."), SettingPropertyGroup("定居点食物奖励")]
        public float TownFoodBonus { get; set; } = 4f;

        [SettingPropertyBool("人口粮食消耗", Order = 1, RequireRestart = false, HintText = "允许你调整繁荣导致人口对粮食过度消耗所受到的影响."), SettingPropertyGroup("定居点食物奖励")]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("粮食损失除数", 50f, 400f, HintText = "本地值是50.用殖民地的繁荣程度除以这个值来计算损失.增加这个值将减少食物的损失."), SettingPropertyGroup("定居点食物奖励")]
        public float SettlementProsperityFoodMalusDivisor { get; set; } = 100;

        #endregion

        #region Settlement Tweaks - Settlement Militia Bonus Tweaks

        [SettingPropertyBool("启用民兵奖励", Order = 1, RequireRestart = false, HintText = "给城镇和城堡里的民兵增加人数"), SettingPropertyGroup("定居点民兵奖励")]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("城堡民兵增长", 0f, 5f, HintText = "本机值为0。给城堡里的民兵增加数量."), SettingPropertyGroup("定居点民兵奖励")]
        public float CastleMilitiaBonus { get; set; } = 1.25f;

        [SettingPropertyFloatingInteger("城镇民兵增长", 0f, 5f, HintText = "本机值为0。给城镇里的民兵增加数量."), SettingPropertyGroup("定居点民兵奖励")]
        public float TownMilitiaBonus { get; set; } = 2.5f;


        [SettingPropertyBool("启用贵族兵培育", Order = 1, RequireRestart = false, HintText = "增加在城镇和城堡民兵升级为正规军的几率"), SettingPropertyGroup("定居点民兵奖励")]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("贵族步兵培育", 0f, 1f, HintText = "游戏默认值为0,为城镇和城堡中的民兵是近战部队产生的概率."), SettingPropertyGroup("定居点民兵奖励")]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.15f;

        [SettingPropertyFloatingInteger("贵族射手培育", 0f, 1f, HintText = "游戏默认值为0,为城镇和城堡中的民兵是远程部队产生的概率."), SettingPropertyGroup("定居点民兵奖励")]
        public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;

        #endregion

        #region Settlement Tweaks - Tournament Tweaks

        [SettingPropertyBool("启用比赛声望调整", Order = 1, RequireRestart = false, HintText = "设置赢得竞技场比赛时获得声望."), SettingPropertyGroup("比赛调整/声望奖励调整")]
        public bool TournamentRenownIncreaseEnabled { get; set; } = true;

        [SettingPropertyInteger("获得声望数量", 1, 20, HintText = "游戏默认值为3,增加赢得竞技场时获得的声望."), SettingPropertyGroup("比赛调整/声望奖励调整")]
        public int TournamentRenownAmount { get; set; } = 8;

        [SettingPropertyBool("启用奖金调整", Order = 1, RequireRestart = false, HintText = "当您赢得竞技场时,将增加奖金."), SettingPropertyGroup("比赛调整/金钱奖励调整")]
        public bool TournamentGoldRewardEnabled { get; set; } = true;

        [SettingPropertyInteger("获得奖金额度", 150, 1000, HintText = "游戏默认值为0.当您赢得竞技场时,会在奖励中添加固定奖金."), SettingPropertyGroup("比赛调整/金钱奖励调整")]
        public int TournamentGoldRewardAmount { get; set; } = 500;

        [SettingPropertyBool("启用投注调整", Order = 1, RequireRestart = false, HintText = "设置您可以在竞技场中每轮下注的最大奖金."), SettingPropertyGroup("比赛调整/最大赌注调整")]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("比赛最大赌注数量", 150, 2000, HintText = "游戏默认值为150.设置您可以在竞技场中每轮下注的最大奖金."), SettingPropertyGroup("比赛调整/最大赌注调整")]
        public int TournamentMaxBetAmount { get; set; } = 500;


        [SettingPropertyBool("启用比赛经验覆盖", Order = 1, RequireRestart = false, HintText = "调整比赛中获得经验的本地乘数值."), SettingPropertyGroup("比赛调整/英雄经验调整")]
        public bool TournamentHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("比赛经验乘数", 0.25f, 1f, HintText = "本机值为0.25.设置乘数以应用于英雄人物在比赛中获得的经验."), SettingPropertyGroup("比赛调整/英雄经验调整")]
        public float TournamentHeroExperienceMultiplier { get; set; } = 0.25f;

        [SettingPropertyBool("启用竞技经验倍数", Order = 1, RequireRestart = false, HintText = "在英雄角色的竞技场战斗中，覆盖本地倍增值以获得经验增益."), SettingPropertyGroup("比赛调整/英雄经验倍数")]
        public bool ArenaHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("竞技经验乘数", 0.07f, 1f, HintText = "本机值为0.06,为英雄角色在竞技场战斗中获得经验而重写本地乘数."), SettingPropertyGroup("比赛调整/英雄经验倍数")]
        public float ArenaHeroExperienceMultiplier { get; set; } = 0.07f;


        [SettingPropertyBool("启用赔率调整", Order = 1, RequireRestart = false, HintText = "允许您设置锦标赛中的最小投注赔率."), SettingPropertyGroup("比赛调整/投注赔率")]
        public bool MinimumBettingOddsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("最低投注赔率", 1.1f, 5f, HintText = "本地：1.1。锦标赛赌注的最低赔率."), SettingPropertyGroup("比赛调整/投注赔率")]
        public float MinimumBettingOdds { get; set; } = 2f;

        #endregion

        #region Settlement Tweaks - Workshop Tweaks

        [SettingPropertyBool("启用工坊调整", Order = 0, RequireRestart = false, HintText = "基础最大数量,以及每个家族等级获得的数量增加."), SettingPropertyGroup("工坊调整")]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("基础工坊数量", 0, 20, HintText = "游戏默认值为1,基础工坊数量."), SettingPropertyGroup("工坊调整")]
        public int BaseWorkshopCount { get; set; } = 2;

        [SettingPropertyInteger("氏族等级奖励", 0, 5, HintText = "基础最大数量,以及每个家族等级获得的数量增加."), SettingPropertyGroup("工坊调整")]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;

        [SettingPropertyBool("启用成本调整", Order = 1, RequireRestart = false, HintText = "基础最大成本,以及每个家族等级获得的数量增加."), SettingPropertyGroup("工坊调整/成本")]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("工坊购买价格", 0, 15000, HintText = "游戏默认值为10,000,工坊购买成本."), SettingPropertyGroup("工坊调整/成本")]
        public int WorkshopBaseCost { get; set; } = 10000;

        #endregion

    }
}
