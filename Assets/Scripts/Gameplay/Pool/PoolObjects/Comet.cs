using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class Comet : PoolObject
    {
        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected int _points;

        private bool _isPositiveSpeed;
        private float _speed;
        private float _orbitSpeed;

        public int Points => _points;

        public void Init(float speed, float orbitSpeed, float rotation, float scale)
        {
            _isPositiveSpeed = speed > 0;
            
            _speed = _isPositiveSpeed ? speed : -speed;
            _orbitSpeed = orbitSpeed;
            _rigidbody.rotation = rotation;

            var initialScale = scale / 3f;
            transform.localScale = Vector3.one * initialScale;
            transform.DOScale(scale * Vector3.one, 0.5f).SetEase(Ease.InSine);
        }

        protected virtual void FixedUpdate()
        {
            UpdateAttractionForce();
            UpdateOrbitForce();
        }

        private void UpdateAttractionForce()
        {
            if (_speed == 0)
            {
                return;
            }

            var position = _rigidbody.position;
            var targetPosition = _isPositiveSpeed ? position + (Vector2)transform.up : position - (Vector2)transform.up;
            _rigidbody.position = Vector2.Lerp(position, targetPosition, _speed * Time.fixedDeltaTime);
        }

        private void UpdateOrbitForce()
        {
            if (_orbitSpeed == 0)
            {
                return;
            }
            
            var vectorToTarget = -transform.position;
            var angleRotation = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            
            _rigidbody.rotation = angleRotation + 90;

            var angleDelta = Vector2.SignedAngle(Vector2.left, transform.position);
            angleDelta = angleDelta < 0 ? -angleDelta : 360 - angleDelta;
            angleDelta += _orbitSpeed * Time.fixedDeltaTime;
            
            if (angleDelta > 360)
            {
                angleDelta -= 360;
            }
            
            var distance = Vector2.Distance(Vector2.zero, transform.position);
            
            var position = new Vector2
            {
                x = distance * Mathf.Sin(angleDelta * Mathf.Deg2Rad),
                y = distance * Mathf.Cos(angleDelta * Mathf.Deg2Rad),
            };
            
            position = Quaternion.AngleAxis(90, Vector3.forward) * position;
            _rigidbody.position = position;
        }

        /*private void OnValidate()
        {
            var angleDelta = Vector2.SignedAngle(Vector2.left, transform.position);
            angleDelta = angleDelta < 0 ? -angleDelta : 360 - angleDelta;
            Debug.Log($"angleDelta: {angleDelta}");
        }*/
    }
}