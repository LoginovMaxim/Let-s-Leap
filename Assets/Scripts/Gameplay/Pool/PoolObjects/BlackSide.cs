using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class BlackSide : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}