using UnityEngine;

namespace LetsLeap.Game.Skins
{
    public sealed class VendettaSkinAchievement : SkinAchievement
    {
        private const int DeathCountToUnlock = 50;
        private const int SkinIndex = 3;
        
        public VendettaSkinAchievement(SkinsConfig skinsConfig) : base(skinsConfig)
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
    }
}