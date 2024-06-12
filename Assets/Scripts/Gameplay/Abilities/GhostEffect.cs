namespace Gameplay.Abilities
{
    public sealed class GhostEffect : AbilityEffect
    {
        public override void ApplyStarAbility(Player player, float abilityDuration)
        {
            base.ApplyStarAbility(player, abilityDuration);
            _player.SetAlpha(0.1f);
        }

        protected override void DisableAbility()
        {
            _player.SetAlpha(1f);
            base.DisableAbility();
        }
    }
}