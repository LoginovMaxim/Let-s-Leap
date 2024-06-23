using LetsLeap.Game;
using LetsLeap.Game.Audio;
using UnityEngine;

namespace Gameplay
{
    public sealed class GameManager : MonoSingleton<GameManager>
    {
        private const int ScoreToOfferRevive = 100;
        private const int ReviveStarDuration = 5;
        
        [SerializeField] private Player _player;
        [SerializeField] private float _slowMoSpeed;

        public bool IsGamePaused { get; private set; }
        public bool WasRevived { get; private set; }

        public Player Player => _player;

        private void Start()
        {
            Time.timeScale = 1f;
        }

        public void GameOver()
        {
            _player.gameObject.SetActive(false);
            IsGamePaused = true;
            
            UpdateStatistics();
            
            PlayerPrefs.SetInt(Constants.PrefsKeys.DeathCountKey, PlayerPrefs.GetInt(Constants.PrefsKeys.DeathCountKey) + 1);
            SkinsAchievementManager.Instance.OnPlayerDeath();
            AudioManager.Instance.PlayGameOverSound();

            GameScreensManager.Instance.UpdateScreens();
            
            if (ScoreCounter.Instance.Score < ScoreToOfferRevive)
            {
                GameScreensManager.Instance.ShowGameOverScreen();
                return;
            }

            if (WasRevived)
            {
                GameScreensManager.Instance.ShowGameOverScreen();
                return;
            }
            
            GameScreensManager.Instance.ShowReviveScreen();
        }

        public void OnReviveRewardWatched()
        {
            IsGamePaused = false;
            WasRevived = true;
            
            GameScreensManager.Instance.ShowHudScreen();
            
            if (_player.gameObject.TryGetComponent<StarEffect>(out var playerStar))
            {
                playerStar.ApplyStarAbility(ReviveStarDuration, () => _player.SwitchStarViewVisible(false));
                _player.SwitchStarViewVisible(true);
                return;
            }

            playerStar = _player.gameObject.AddComponent<StarEffect>();
            playerStar.ApplyStarAbility(ReviveStarDuration, () => _player.SwitchStarViewVisible(false));
            _player.SwitchStarViewVisible(true);
            
            _player.Leap();
            _player.gameObject.SetActive(true);
            
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (!IsGamePaused)
            {
                return;
            }
            
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, _slowMoSpeed * Time.deltaTime);
        }

        private void UpdateStatistics()
        {
            if (ScoreCounter.Instance.Score > Statistics.Instance.Record)
            {
                Statistics.Instance.Record = ScoreCounter.Instance.Score;
            }
            
            if (ScoreCounter.Instance.ScoreMultiplier > Statistics.Instance.Multiply)
            {
                Statistics.Instance.Multiply = ScoreCounter.Instance.ScoreMultiplier;
            }

            if (WaveManager.Instance.CurrentWaveNumber + 1 > Statistics.Instance.Stage)
            {
                Statistics.Instance.Stage = WaveManager.Instance.CurrentWaveNumber + 1;
            }

            Statistics.Instance.AppliedAbilitiesAmount += ScoreCounter.Instance.AppliedAbilitiesAmount;
            Statistics.Instance.DestroyedCometsAmount += ScoreCounter.Instance.DestroyedCometsAmount;
        }
    }
}