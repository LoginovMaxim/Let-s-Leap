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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}