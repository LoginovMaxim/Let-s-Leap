using UnityEngine;

namespace Gameplay
{
    public abstract class CometSide : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 3)
            {
                return;
            }

            OnPlayerCollided();
        }

        protected abstract void OnPlayerCollided();
    }
}