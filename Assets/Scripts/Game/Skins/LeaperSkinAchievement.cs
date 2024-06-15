using Gameplay;

namespace LetsLeap.Game.Skins
{
    public sealed class LeaperSkinAchievement : SkinAchievement
    {
        private const int ScoreMultiplyToUnlock = 10;
        
        public LeaperSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
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

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", Statistics.Instance.Multiply.ToString());
        }
    }
}