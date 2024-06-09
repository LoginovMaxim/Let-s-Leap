using Gameplay.Vfx;
using UnityEngine;

namespace Gameplay
{
    public class WhiteSide : CometSide
    {
        protected override void OnPlayerCollided()
        {
            PointsCounter.Instance.AddPoints(_comet.Points);
            PoolService.Instance.Despawn(_comet);
            
            PoolService.Instance.Spawn<ExplosionEffect>(
                transform.position, 
                Quaternion.identity, 
                null);
        }

        protected override void OnBorderCollided()
        {
            PoolService.Instance.Despawn(_comet);
        }
    }
}