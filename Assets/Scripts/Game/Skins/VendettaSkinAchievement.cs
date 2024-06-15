using UnityEngine;

namespace LetsLeap.Game.Skins
{
    public sealed class VendettaSkinAchievement : SkinAchievement
    {
        private const int DeathCountToUnlock = 50;
        
        public VendettaSkinAchievement(SkinsConfig skinsConfig, int skinIndex) : base(skinsConfig, skinIndex)
        {
        }

        public void OnPlayerDeath()
        {
            if (_skinsConfig.SkinData[SkinIndex].IsUnlocked)
            {
                return;
            }

            if (PlayerPrefs.GetInt(Constants.PrefsKeys.DeathCountKey) >= DeathCountToUnlock)
            {
                _skinsConfig.SkinData[SkinIndex].IsUnlocked = true;
            }
        }

        public override string GetDescription()
        {
            return _skinsConfig.SkinData[SkinIndex].Description.
                Replace("X", PlayerPrefs.GetInt(Constants.PrefsKeys.DeathCountKey).ToString());
        }
    }
}