using Gameplay;

namespace LetsLeap.Game.Skins
{
    public sealed class HatSkinAchievement : SkinAchievement
    {
        private const int ScoreToUnlock = 1000;
        private const int SkinIndex = 4;
        
        public HatSkinAchievement(SkinsConfig skinsConfig) : base(skinsConfig)
        {
        }

        public void OnPlayerDeath()
        {
            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (ScoreCounter.Instance.Score >= ScoreToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }
    }
}