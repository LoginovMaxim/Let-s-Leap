namespace LetsLeap.Game.Skins
{
    public sealed class HatSkinAchievement : SkinAchievement
    {
        private const int ScoreToUnlock = 1000;
        
        public HatSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
        {
        }

        public void OnPlayerDeath()
        {
            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (Statistics.Instance.Record >= ScoreToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", Statistics.Instance.Record.ToString());
        }
    }
}