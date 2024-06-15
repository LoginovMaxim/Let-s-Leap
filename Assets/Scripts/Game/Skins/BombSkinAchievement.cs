namespace LetsLeap.Game.Skins
{
    public sealed class BombSkinAchievement : SkinAchievement
    {
        private const int DestroyedCometsAmountToUnlock = 50;
        
        public BombSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
        {
        }

        public void OnPlayerDeath()
        {
            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (Statistics.Instance.DestroyedCometsAmount >= DestroyedCometsAmountToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", Statistics.Instance.DestroyedCometsAmount.ToString());
        }
    }
}