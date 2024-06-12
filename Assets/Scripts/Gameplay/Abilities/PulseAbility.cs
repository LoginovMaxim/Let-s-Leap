using UnityEngine;

namespace Gameplay
{
    public sealed class PulseAbility : Ability
    {
        [SerializeField] private Pulse _pulse;
        
        protected override void ApplyAbility(Player player)
        {
            Instantiate(_pulse, player.transform.position, Quaternion.identity);
        }
    }
}