using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Orbit : MonoBehaviour
    {
        [SerializeField] private bool _isClockwise;
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var direction = _isClockwise ? -1 : 1;
            _rigidbody.rotation += direction * _speed * Time.fixedDeltaTime;
        }
    }
}