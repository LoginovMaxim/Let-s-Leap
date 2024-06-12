namespace Gameplay
{
    public sealed class GhostAbility : Ability
    {
        protected override void ApplyAbility(Player player)
        {
            if (player.gameObject.TryGetComponent<GhostEffect>(out var playerGhost))
            {
                playerGhost.ApplyStarAbility(player, _duration);
                return;
            }

            playerGhost = player.gameObject.AddComponent<GhostEffect>();
            playerGhost.ApplyStarAbility(player, _duration);
        }
    }
}