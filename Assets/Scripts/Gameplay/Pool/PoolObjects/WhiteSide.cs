using Gameplay.Vfx;
using UnityEngine;

namespace Gameplay
{
    public class WhiteSide : CometSide
    {
        protected override void OnPlayerCollided(Player player)
        {
            if (player.gameObject.GetComponent<GhostEffect>())
            {
                return;
            }
            
            ScoreCounter.Instance.AddScore(_comet.Points);
            PoolService.Instance.Despawn(_comet);
            
            var explosionEffect = PoolService.Instance.Spawn<ExplosionEffect>(
                transform.position, 
                Quaternion.identity, 
                null);

            explosionEffect.transform.localScale = _comet.transform.localScale;
        }

        protected override void OnBorderCollided()
        {
            PoolService.Instance.Despawn(_comet);
        }
    }
}