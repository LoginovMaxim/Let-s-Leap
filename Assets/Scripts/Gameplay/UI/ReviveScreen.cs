using LetsLeap.Game.Audio;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public sealed class ReviveScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void UpdateScreenView()
        {
            _scoreText.text = ScoreCounter.Instance.Score.ToString();
        }
        
        public void OnReviveButtonPressed()
        {
            GameManager.Instance.OnReviveRewardWatched();
            AudioManager.Instance.PlayUiClickSound();
        }
        
        public void OnCloseButtonPressed()
        {
            GameScreensManager.Instance.ShowGameOverScreen();
            AudioManager.Instance.PlayUiClickSound();
        }
    }
}