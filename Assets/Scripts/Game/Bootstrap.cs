using UnityEngine;
using UnityEngine.SceneManagement;

namespace LetsLeap.Game
{
    public sealed class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("Meta");
        }
    }
}