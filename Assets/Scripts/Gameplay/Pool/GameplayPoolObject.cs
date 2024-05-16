using UnityEngine;

namespace Gameplay
{
    public abstract class GameplayPoolObject : PoolObject
    {
        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected int _points;

        private float _speed;

        public int Points => _points;
        
        public void SetRotation(float rotation)
        {
            _rigidbody.rotation = rotation;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        private void FixedUpdate()
        {
            if (_speed == 0)
            {
                return;
            }

            var position = _rigidbody.position;
            var targetPosition = position - (Vector2)transform.up;
            _rigidbody.position = Vector2.Lerp(position, targetPosition, _speed * Time.fixedDeltaTime);
        }
    }
}