namespace LetsLeap.Game.Skins
{
    public sealed class DefaultSkinAchievement : SkinAchievement
    {
        public DefaultSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
        {
        }

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description;
        }
    }
}