using UnityEngine;

namespace Gameplay
{
    public abstract class Ability : PoolObject
    {
        protected const int AbilityLifetime = 36;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 3)
            {
                return;
            }
            
            ApplyAbility(other.gameObject.GetComponent<Player>());
            ScoreCounter.Instance.OnAbilityApplied();
        }

        protected abstract void ApplyAbility(Player player);
    }
}