namespace LetsLeap.Game.Skins
{
    public sealed class RobotSkinAchievement : SkinAchievement
    {
        private const int StageToUnlock = 10;
        
        public RobotSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
        {
        }

        public void OnPlayerDeath()
        {
            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (Statistics.Instance.Stage >= StageToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", Statistics.Instance.Stage.ToString());
        }
    }
}