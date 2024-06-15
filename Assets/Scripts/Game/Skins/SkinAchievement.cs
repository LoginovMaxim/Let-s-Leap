namespace LetsLeap.Game.Skins
{
    public abstract class SkinAchievement
    {
        protected SkinsConfig _skinsConfig;
        protected int _skinIndex;

        public int SkinIndex => _skinIndex;

        protected SkinAchievement(SkinsConfig skinsConfig, int skinIndex)
        {
            _skinsConfig = skinsConfig;
            _skinIndex = skinIndex;
        }

        public virtual void Tick()
        {
        }

        public abstract string GetDescription();
    }
}