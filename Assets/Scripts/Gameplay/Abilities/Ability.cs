using LetsLeap.Game.Audio;
using UnityEngine;

namespace Gameplay
{
    public abstract class Ability : PoolObject
    {
        protected const int AbilityLifetime = 36;
        protected abstract string AbilityName { get; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 3)
            {
                return;
            }
            
            ApplyAbility(other.gameObject.GetComponent<Player>());
            ScoreCounter.Instance.OnAbilityApplied();
            AudioManager.Instance.PlayApplyAbilitySound();

            var playerPosition = GameManager.Instance.Player.transform.position + Vector3.down;
            var scoreHitDisplay = PoolService.Instance.Spawn<DisplayTextCanvas>(playerPosition, Quaternion.identity, null);
            scoreHitDisplay.SetText(AbilityName);
        }

        protected abstract void ApplyAbility(Player player);
    }
}