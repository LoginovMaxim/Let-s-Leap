using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public sealed class Star : BlackSide
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}