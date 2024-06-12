namespace LetsLeap.Game.Skins
{
    public abstract class SkinAchievement
    {
        protected SkinsConfig _skinsConfig;

        protected SkinAchievement(SkinsConfig skinsConfig)
        {
            _skinsConfig = skinsConfig;
        }

        public virtual void Tick()
        {
        }
    }
}