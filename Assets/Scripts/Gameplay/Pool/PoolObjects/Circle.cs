using DG.Tweening;
using Gameplay.Vfx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class Circle : GameplayPoolObject
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
            
            PointsCounter.Instance.AddPoints(_points);
            
            PoolService.Instance.Spawn<ExplosionEffect>(
                transform.position, 
                Quaternion.identity, 
                null);
        }
    }
}