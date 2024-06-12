using UnityEngine;

namespace Gameplay.Abilities
{
    public abstract class AbilityEffect : MonoBehaviour
    {
        protected Player _player;
        private GameObject _abilityView;
        private float _timeToDisable;
        private bool _wasApplied;

        private void Update()
        {
            if (!_wasApplied)
            {
                return;
            }
            
            if (_timeToDisable > Time.time)
            {
                return;
            }

            DisableAbility();
        }

        public virtual void ApplyStarAbility(Player player, float abilityDuration)
        {
            _player = player;
            
            if (_timeToDisable == 0)
            {
                _timeToDisable = Time.time + abilityDuration;
                _wasApplied = true;
                return;
            }

            _timeToDisable += abilityDuration;
            
            _wasApplied = true;
        }

        public void SetAbilityView(GameObject abilityView)
        {
            _abilityView = abilityView;
        }

        protected virtual void DisableAbility()
        {
            if (_abilityView != null)
            {
                Destroy(_abilityView);
            }
            
            Destroy(this);
        }
    }
}