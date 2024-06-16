using Gameplay.Vfx;
using UnityEngine;

namespace Gameplay
{
    public sealed class Bullet : Comet
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!TryGetCometTransform(other.gameObject, out var comet))
            {
                return;
            }
            
            PoolService.Instance.Despawn(comet);
            
            var explosionEffect = PoolService.Instance.Spawn<ExplosionEffect>(
                transform.position, 
                Quaternion.identity, 
                null);

            explosionEffect.transform.localScale = comet.transform.localScale;
        }

        private bool TryGetCometTransform(GameObject gameObject, out Comet comet)
        {
            comet = null;
            if (gameObject.CompareTag("WhiteSide") || gameObject.CompareTag("BlackSide"))
            {
                comet = gameObject.GetComponentInParent<Comet>();
                return true;
            }

            return false;
        }
    }
}