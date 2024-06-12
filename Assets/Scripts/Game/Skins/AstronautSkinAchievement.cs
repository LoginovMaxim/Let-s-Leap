namespace LetsLeap.Game.Skins
{
    public sealed class AstronautSkinAchievement : SkinAchievement
    {
        private const int LeapHeightToUnlock = 25;
        private const int SkinIndex = 1;
        
        public AstronautSkinAchievement(SkinsConfig skinsConfig) : base(skinsConfig)
        {
        }

        public override void Tick()
        {
            base.Tick();

            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (Statistics.Instance.Leap > LeapHeightToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }
    }
}