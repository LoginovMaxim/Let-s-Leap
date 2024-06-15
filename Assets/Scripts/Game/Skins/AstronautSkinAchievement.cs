namespace LetsLeap.Game.Skins
{
    public sealed class AstronautSkinAchievement : SkinAchievement
    {
        private const int LeapHeightToUnlock = 25;

        public AstronautSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
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

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", Statistics.Instance.Leap.ToString());
        }
    }
}