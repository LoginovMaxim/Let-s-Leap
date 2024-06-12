using UnityEngine;

namespace Gameplay.Abilities
{
    public sealed class StarAbility : Ability
    {
        [SerializeField] private GameObject _playerStarView;
        
        protected override void ApplyAbility(Player player)
        {
            if (player.gameObject.TryGetComponent<StarEffect>(out var playerStar))
            {
                playerStar.ApplyStarAbility(player, _duration);
                return;
            }

            playerStar = player.gameObject.AddComponent<StarEffect>();
            
            var playerTransform = player.transform;
            _playerStarView = Instantiate(_playerStarView, 
                playerTransform.position, 
                playerTransform.rotation, 
                playerTransform);
            
            playerStar.ApplyStarAbility(player, _duration);
            playerStar.SetAbilityView(_playerStarView);
        }
    }
}