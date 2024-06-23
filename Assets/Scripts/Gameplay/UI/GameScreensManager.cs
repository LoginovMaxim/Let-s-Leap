using UnityEngine;

namespace Gameplay
{
    public sealed class GameScreensManager : MonoSingleton<GameScreensManager>
    {
        [SerializeField] private Hud _hud;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private ReviveScreen _reviveScreen;

        public void ShowHudScreen()
        {
            _hud.gameObject.SetActive(true);
            _gameOverScreen.gameObject.SetActive(false);
            _reviveScreen.gameObject.SetActive(false);
        }

        public void ShowGameOverScreen()
        {
            _hud.gameObject.SetActive(false);
            _gameOverScreen.gameObject.SetActive(true);
            _reviveScreen.gameObject.SetActive(false);
        }

        public void ShowReviveScreen()
        {
            _hud.gameObject.SetActive(false);
            _gameOverScreen.gameObject.SetActive(false);
            _reviveScreen.gameObject.SetActive(true);
        }

        public void UpdateScreens()
        {
            _gameOverScreen.UpdateScreenView();
            _reviveScreen.UpdateScreenView();
        }
    }
}