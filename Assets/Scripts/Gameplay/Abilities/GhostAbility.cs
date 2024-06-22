namespace Gameplay
{
    public sealed class GhostAbility : DurationAbility
    {
        protected override string AbilityName => "бесплотный";
        
        protected override void ApplyAbility(Player player)
        {
            if (player.gameObject.TryGetComponent<GhostEffect>(out var playerGhost))
            {
                playerGhost.ApplyStarAbility(_duration, () => player.SetAlpha(1f));
                player.SetAlpha(0.1f);
                return;
            }

            playerGhost = player.gameObject.AddComponent<GhostEffect>();
            playerGhost.ApplyStarAbility(_duration, () => player.SetAlpha(1f));
            player.SetAlpha(0.1f);
        }
    }
}