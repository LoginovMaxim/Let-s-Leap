namespace Gameplay
{
    public sealed class StarAbility : DurationAbility
    {
        protected override string AbilityName => "неуязвимый";

        protected override void ApplyAbility(Player player)
        {
            if (player.gameObject.TryGetComponent<StarEffect>(out var playerStar))
            {
                playerStar.ApplyStarAbility(_duration, () => player.SwitchStarViewVisible(false));
                player.SwitchStarViewVisible(true);
                return;
            }

            playerStar = player.gameObject.AddComponent<StarEffect>();
            playerStar.ApplyStarAbility(_duration, () => player.SwitchStarViewVisible(false));
            player.SwitchStarViewVisible(true);
        }
    }
}