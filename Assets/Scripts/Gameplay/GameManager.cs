using LetsLeap.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public sealed class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private Player _player;

        public Player Player => _player;
        
        public void GameOver()
        {
            UpdateStatistics();
            
            PlayerPrefs.SetInt(Constants.PrefsKeys.DeathCountKey, PlayerPrefs.GetInt(Constants.PrefsKeys.DeathCountKey) + 1);
            SkinsAchievementManager.Instance.OnPlayerDeath();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        }
    }
}