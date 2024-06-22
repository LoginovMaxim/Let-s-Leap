using System;
using UnityEngine;

namespace Gameplay
{
    public abstract class AbilityEffect : MonoBehaviour
    {
        private Action _disableAction;
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

        private void DisableAbility()
        {
            _disableAction?.Invoke();
            Destroy(this);
        }

        public void ApplyStarAbility(float abilityDuration, Action disableAction)
        {
            _disableAction = disableAction;
            
            if (_timeToDisable == 0)
            {
                _timeToDisable = Time.time + abilityDuration;
                _wasApplied = true;
                return;
            }

            _timeToDisable += abilityDuration;
            
            _wasApplied = true;
        }
    }
}