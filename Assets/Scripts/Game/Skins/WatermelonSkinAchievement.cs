using Gameplay;

namespace LetsLeap.Game.Skins
{
    public sealed class WatermelonSkinAchievement : SkinAchievement
    {
        private const int AppliedAbilitiesAmountToUnlock = 25;
        
        public WatermelonSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
        {
        }

        public void OnPlayerDeath()
        {
            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (Statistics.Instance.AppliedAbilitiesAmount >= AppliedAbilitiesAmountToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", Statistics.Instance.AppliedAbilitiesAmount.ToString());
        }
    }
}