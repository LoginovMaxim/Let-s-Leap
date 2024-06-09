using UnityEngine;

namespace Gameplay
{
    public abstract class CometSide : MonoBehaviour
    {
        protected Comet _comet;
        private LayerMask _playerMask;
        private LayerMask _borderMask;

        private void Start()
        {
            _comet = GetComponentInParent<Comet>();
            _playerMask = LayerMask.GetMask("Player");
            _borderMask = LayerMask.GetMask("Border");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                OnPlayerCollided();
                return;
            }
            
            if (other.gameObject.layer == 8)
            {
                OnBorderCollided();
                return;
            }
        }

        protected abstract void OnPlayerCollided();
        protected abstract void OnBorderCollided();
    }
}