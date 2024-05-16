using UnityEngine.SceneManagement;

namespace Gameplay
{
    public sealed class GameManager : MonoSingleton<GameManager>
    {
        public void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}