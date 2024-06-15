using System.Collections.Generic;
using Gameplay;
using LetsLeap.Game.Skins;
using UnityEngine;

namespace LetsLeap.Game
{
    public sealed class SkinsAchievementManager : MonoSingleton<SkinsAchievementManager>
    {
        [SerializeField] private SkinsConfig _skinsConfig;

        private List<SkinAchievement> _skinAchievements;
        private Dictionary<int, SkinAchievement> _skinAchievementsByIndexes;
        
        private VendettaSkinAchievement _vendettaSkinAchievement;
        private HatSkinAchievement _hatSkinAchievement;
        private WatermelonSkinAchievement _watermelonSkinAchievement;
        private BombSkinAchievement _bombSkinAchievement;
        private RobotSkinAchievement _robotSkinAchievement;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _vendettaSkinAchievement = new VendettaSkinAchievement(_skinsConfig, 3);
            _hatSkinAchievement = new HatSkinAchievement(_skinsConfig, 4);
            _watermelonSkinAchievement = new WatermelonSkinAchievement(_skinsConfig, 5);
            _bombSkinAchievement = new BombSkinAchievement(_skinsConfig, 6);
            _robotSkinAchievement = new RobotSkinAchievement(_skinsConfig, 7);
            
            _skinAchievements = new List<SkinAchievement>()
            {
                new DefaultSkinAchievement(_skinsConfig, 0),
                new AstronautSkinAchievement(_skinsConfig, 1),
                new LeaperSkinAchievement(_skinsConfig, 2),
                _vendettaSkinAchievement,
                _hatSkinAchievement,
                _watermelonSkinAchievement,
                _bombSkinAchievement,
                _robotSkinAchievement,
            };

            _skinAchievementsByIndexes = new Dictionary<int, SkinAchievement>();
            foreach (var skinAchievement in _skinAchievements)
            {
                _skinAchievementsByIndexes.Add(skinAchievement.SkinIndex, skinAchievement);
            }
        }

        private void Update()
        {
            foreach (var skinAchievement in _skinAchievements)
            {
                skinAchievement.Tick();
            }
        }

        public void OnPlayerDeath()
        {
            _vendettaSkinAchievement.OnPlayerDeath();
            _hatSkinAchievement.OnPlayerDeath();
            _watermelonSkinAchievement.OnPlayerDeath();
            _bombSkinAchievement.OnPlayerDeath();
            _robotSkinAchievement.OnPlayerDeath();
        }

        public string GetSkinDescriptionByIndex(int skinIndex)
        {
            return _skinAchievementsByIndexes[skinIndex].GetDescription();
        }
    }
}