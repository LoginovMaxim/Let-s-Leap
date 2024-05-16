using UnityEngine;

namespace Gameplay
{
    public sealed class Star : BlackSide
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.layer != 3)
            {
                return;
            }
            
            GameManager.Instance.GameOver();
        }
    }
}