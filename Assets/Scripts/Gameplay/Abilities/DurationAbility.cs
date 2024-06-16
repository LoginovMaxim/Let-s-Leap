using UnityEngine;

namespace Gameplay
{
    public abstract class DurationAbility : Ability
    {
        [SerializeField] protected float _duration;

        private bool _isActive = false;
        private float _timeToDisable;

        public override void Reinitialize(Vector3 position)
        {
            base.Reinitialize(position);
            _timeToDisable = Time.time + AbilityLifetime;
            _isActive = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }
            
            if (_timeToDisable > Time.time)
            {
                return;
            }
            
            PoolService.Instance.Despawn(this);
        }
    }
}