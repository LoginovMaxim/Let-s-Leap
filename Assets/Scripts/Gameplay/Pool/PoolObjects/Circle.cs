using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class Circle : PoolObject
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int _points;

        private float _speed;
        
        private void Start()
        {
            PoolObjectId = PoolObjectId.Circle;
        }

        public override void Reinitialize()
        {
            base.Reinitialize();

            transform.localScale = Vector3.zero;
            
            var randomScale = Random.Range(0.66f, 1.33f);
            transform.DOScale(randomScale * Vector3.one, 2f).SetEase(Ease.InSine);
        }

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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.layer != 3)
            {
                return;
            }
            
            PointsCounter.Instance.AddPoints(_points);
        }
    }
}