using System.Collections.Generic;
using Gameplay;
using LetsLeap.Game.Skins;
using UnityEngine;

namespace LetsLeap.Game
{
    public sealed class SkinsAchievementManager : MonoSingleton<SkinsAchievementManager>
    {
        [SerializeField] private SkinsConfig _skinsConfig;

        private List<SkinAchievement> _astronautSkinAchievement;
        private VendettaSkinAchievement _vendettaSkinAchievement;
        private HatSkinAchievement _hatSkinAchievement;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _vendettaSkinAchievement = new VendettaSkinAchievement(_skinsConfig);
            _hatSkinAchievement = new HatSkinAchievement(_skinsConfig);
            
            _astronautSkinAchievement = new List<SkinAchievement>()
            {
                new AstronautSkinAchievement(_skinsConfig),
                new LeaperSkinAchievement(_skinsConfig),
                _vendettaSkinAchievement,
                _hatSkinAchievement,
            };
        }

        private void Update()
        {
            foreach (var skinAchievement in _astronautSkinAchievement)
            {
                skinAchievement.Tick();
            }
        }

        public void OnPlayerDeath()
        {
            _vendettaSkinAchievement.OnPlayerDeath();
            _hatSkinAchievement.OnPlayerDeath();
        }
    }
}