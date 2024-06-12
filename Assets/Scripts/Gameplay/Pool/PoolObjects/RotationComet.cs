using UnityEngine;

namespace Gameplay
{
    public class RotationComet : Comet
    {
        [SerializeField] private float _speedRotation;
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Rotate();
        }

        private void Rotate()
        {
            _rigidbody.rotation += _speedRotation * Time.fixedDeltaTime;
        }
    }
}