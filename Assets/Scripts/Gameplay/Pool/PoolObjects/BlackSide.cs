using Gameplay.Vfx;
using LetsLeap.Game.Audio;
using UnityEngine;

namespace Gameplay
{
    public class BlackSide : CometSide
    {
        protected override void OnPlayerCollided(Player player)
        {
            if (player.gameObject.GetComponent<StarEffect>())
            {
                PoolService.Instance.Despawn(_comet);
            
                var explosionEffect = PoolService.Instance.Spawn<ExplosionEffect>(
                    transform.position, 
                    Quaternion.identity, 
                    null);

                explosionEffect.transform.localScale = _comet.transform.localScale;
                
                ScoreCounter.Instance.AddScore(_comet.Points);
                ScoreCounter.Instance.OnCometDestroyed();
                
                //player.Leap();
                AudioManager.Instance.PlayDestroyCometSound();
                
                return;
            }
            
            if (player.gameObject.GetComponent<GhostEffect>())
            {
                return;
            }
            
            GameManager.Instance.GameOver();
        }

        protected override void OnBorderCollided()
        {
            PoolService.Instance.Despawn(_comet);
        }
    }
}