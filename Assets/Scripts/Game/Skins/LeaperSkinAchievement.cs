using Gameplay;

namespace LetsLeap.Game.Skins
{
    public sealed class LeaperSkinAchievement : SkinAchievement
    {
        private const int ScoreMultiplyToUnlock = 10;
        private const int SkinIndex = 2;
        
        public LeaperSkinAchievement(SkinsConfig skinsConfig) : base(skinsConfig)
        {
        }

        public override void Tick()
        {
            base.Tick();

            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (ScoreCounter.Instance == null)
            {
                return;
            }

            if (ScoreCounter.Instance.ScoreMultiplier > ScoreMultiplyToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }
    }
}