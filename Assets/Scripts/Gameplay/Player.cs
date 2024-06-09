using LetsLeap.Game;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {
        [Header("Skin")] 
        [SerializeField] private SkinsConfig _skinsConfig;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Header("Common")]
        [SerializeField] private float _sensitive;
        [SerializeField] private AnimationCurve _stretchCurve;
        
        [Header("Attraction")]
        [SerializeField] private float _attractionForce;
        
        [Header("Lateral")]
        [SerializeField] private float _lateralSensitive;
        [SerializeField] private float _lateralForce;
        
        [Header("Leap")]
        [SerializeField] private float _leapForce;
        
        private Rigidbody2D _rigidbody;
        private LayerMask _whiteMask;

        private Vector2 _attractionVector;
        private Vector2 _lateralVector;
        private Vector2 _leapVector;
        private float _leapDelta = 1;
        private int _positiveLeapsCount;
        private bool _isTouching;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _whiteMask = LayerMask.GetMask("White");
            
            foreach (var skinData in _skinsConfig.SkinData)
            {
                if (!skinData.IsSelected)
                {
                    continue;
                }

                _spriteRenderer.sprite = skinData.Icon;
            }
        }

        private void Update()
        {
            _isTouching = Input.GetMouseButton(0);
            UpdateStretch();
        }

        private void FixedUpdate()
        {
            UpdateAttractionForce();
            UpdateLateralForce();
            UpdateTargetPosition();
            UpdateLookRotation();
        }

        private void UpdateAttractionForce()
        {
            var direction = new Vector2(transform.position.x, transform.position.y).normalized;
            _leapVector = direction * _leapForce * (0.8f + (_positiveLeapsCount / 5f));

            var sqrMagnitude = transform.position.sqrMagnitude / 300f;
            var distanceForce = Mathf.Clamp(sqrMagnitude, 1f, 5f);
            var attractionVector = -direction * distanceForce * _attractionForce;
            
            _leapDelta += _attractionForce * Time.fixedDeltaTime;

            if (_leapDelta >= 1f)
            {
                _positiveLeapsCount = 0;
            }
            
            _attractionVector = Vector2.Lerp(_leapVector, attractionVector, Mathf.Clamp01(_leapDelta));
            Debug.DrawLine(transform.position, _attractionVector, Color.yellow);
        }

        private void UpdateLateralForce()
        {
            if (!_isTouching)
            {
                _lateralVector = Vector2.Lerp(_lateralVector, Vector2.zero, _lateralSensitive * Time.fixedDeltaTime);
                Debug.DrawLine(transform.position, Vector2.zero, Color.blue);
                return;
            }

            var direction = new Vector2(transform.right.x, transform.right.y);
            _lateralVector = Vector2.Lerp(_lateralVector, direction * _lateralForce, _lateralSensitive * Time.fixedDeltaTime);
            Debug.DrawLine(transform.position, _lateralVector, Color.blue);
        }

        private void UpdateTargetPosition()
        {
            Debug.DrawLine(transform.position, _attractionVector + _lateralVector, Color.green);
            _rigidbody.position = Vector2.Lerp(_rigidbody.position, _rigidbody.position + _attractionVector + _lateralVector,
                _sensitive * Time.fixedDeltaTime);
        }

        private void UpdateLookRotation()
        {
            var vectorToTarget = -transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = angle + 90;
        }

        private void UpdateStretch()
        {
            var localScale = transform.localScale;
            var targetScaleY = _stretchCurve.Evaluate(Mathf.Clamp01(_leapDelta));
            localScale.x = Mathf.Lerp(localScale.x, 1f / targetScaleY, 10f * Time.deltaTime);
            localScale.y = Mathf.Lerp(localScale.y, targetScaleY, 10f * Time.deltaTime);
            transform.localScale = localScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 9)
            {
                PointsCounter.Instance.IncreaseMultiplier();
            }
            
            if (other.gameObject.layer != 6)
            {
                return;
            }

            if (other.gameObject.TryGetComponent<PoolObject>(out var poolObject))
            {
                PoolService.Instance.Despawn(poolObject);
            }
            
            _leapDelta = 0;
            _positiveLeapsCount++;
        }
    }
}