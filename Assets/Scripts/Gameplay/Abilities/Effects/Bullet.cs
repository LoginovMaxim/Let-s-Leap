using System;
using Gameplay.Vfx;
using LetsLeap.Game.Audio;
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
            PoolService.Instance.Despawn(this);
            AudioManager.Instance.PlayDestroyCometSound();
            
            var explosionEffect = PoolService.Instance.Spawn<ExplosionEffect>(
                transform.position, 
                Quaternion.identity, 
                null);

            explosionEffect.transform.localScale = comet.transform.localScale;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != 8)
            {
                return;
            }
            
            PoolService.Instance.Despawn(this);
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