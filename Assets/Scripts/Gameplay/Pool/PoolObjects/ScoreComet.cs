using Gameplay.Vfx;
using UnityEngine;

namespace Gameplay
{
    public sealed class ScoreComet : Comet
    {
        private LayerMask _playerMask;
        private LayerMask _borderMask;

        private void Start()
        {
            _playerMask = LayerMask.GetMask("Player");
            _borderMask = LayerMask.GetMask("Border");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 8)
            {
                PoolService.Instance.Despawn(this);
                return;
            }
            
            if (other.gameObject.layer != 3)
            {
                return;
            }
            
            ScoreCounter.Instance.AddScore(_points);
            other.gameObject.GetComponent<Player>().PlayLeapSound();
            
            PoolService.Instance.Spawn<ExplosionEffect>(
                transform.position, 
                Quaternion.identity, 
                null);
        }
    }
}