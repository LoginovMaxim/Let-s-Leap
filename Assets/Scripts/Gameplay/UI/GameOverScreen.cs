using LetsLeap.Game;
using LetsLeap.Game.Audio;
using LetsLeap.Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public sealed class GameOverScreen : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject StatsPanel;
        [SerializeField] private GameObject RecordStatsPanel;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreRecordText;
        [SerializeField] private TextMeshProUGUI _newScoreRecordText;

        public void UpdateScreenView()
        {
            if (ScoreCounter.Instance.Score < Statistics.Instance.Record)
            {
                StatsPanel.gameObject.SetActive(true);
                RecordStatsPanel.gameObject.SetActive(false);
                
                _scoreText.text = ScoreCounter.Instance.Score.ToString();
                _scoreRecordText.text = Statistics.Instance.Record.ToString();
            }
            else
            {
                StatsPanel.gameObject.SetActive(false);
                RecordStatsPanel.gameObject.SetActive(true);
                
                _newScoreRecordText.text = Statistics.Instance.Record.ToString();
            }
        }
        
        public void OnReplayButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AudioManager.Instance.PlayUiClickSound();
        }

        public void OnMenuButtonPressed()
        {
            SceneManager.LoadScene(1);
            AudioManager.Instance.PlayUiClickSound();
        }

        public void OnSettingsButtonPressed()
        {
            Settings.Instance.gameObject.SetActive(true);
            AudioManager.Instance.PlayUiClickSound();
        }
    }
}