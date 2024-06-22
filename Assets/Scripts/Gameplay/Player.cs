using DG.Tweening;
using LetsLeap.Game;
using LetsLeap.Game.Audio;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {
        private const float LeapPitchDelta = 0.1f;
        
        [Header("Skin")] 
        [SerializeField] private SkinsConfig _skinsConfig;
        [SerializeField] private SpriteRenderer _skinRenderer;
        [SerializeField] private Transform _skinTransform;
        
        [Header("Rotations")] 
        [SerializeField] private float _leapUpRotation;
        [SerializeField] private float _leapDownRotation;
        [SerializeField] private float _speedLeapRotation;
        
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
        
        [Header("Ability Views")]
        [SerializeField] private GameObject _starView;
        
        private Rigidbody2D _rigidbody;
        private LayerMask _whiteMask;

        private Vector2 _attractionVector;
        private Vector2 _lateralVector;
        private Vector2 _leapVector;
        private float _leapDelta = 1;
        private float _targetLeapRotation;
        private int _positiveLeapsCount;
        private bool _isTouching;
        private float _leapPitch;
        
        public float MaxLeapHeight { get; private set; }
        
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

                _skinRenderer.sprite = skinData.Icon;
            }
        }

        private void Update()
        {
            _isTouching = Input.GetMouseButton(0);
            UpdateStretch();
            UpdateLeapRotation();
            CheckLeapHeight();
        }

        private void FixedUpdate()
        {
            UpdateAttractionForce();
            UpdateLateralForce();
            UpdateTargetPosition();
            UpdateLookRotation();
        }

        public void SetAlpha(float alpha)
        {
            _skinRenderer.DOFade(alpha, 0.2f);
        }

        public void SwitchStarViewVisible(bool isVisible)
        {
            _starView.SetActive(isVisible);
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
                _targetLeapRotation = _leapDownRotation;
                _leapPitch = 1f;
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

        private void UpdateLeapRotation()
        {
            var skinRotation = _skinTransform.localRotation.eulerAngles;
            
            if (skinRotation.z > 180)
            {
                skinRotation.z -= 360;
            }
            
            skinRotation.z = Mathf.Lerp(skinRotation.z, _targetLeapRotation, _speedLeapRotation * Time.deltaTime);
            _skinTransform.localRotation = Quaternion.Euler(skinRotation);
        }

        private void CheckLeapHeight()
        {
            if (transform.position.sqrMagnitude > Statistics.Instance.Leap * Statistics.Instance.Leap)
            {
                Statistics.Instance.Leap = Mathf.RoundToInt(Mathf.Sqrt(transform.position.sqrMagnitude));
            }
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
                ScoreCounter.Instance.IncreaseScoreMultiplier();
            }
            
            if (other.gameObject.layer != 6)
            {
                return;
            }

            var isPoolObject = other.gameObject.TryGetComponent<PoolObject>(out var poolObject);

            var isScoreComet = other.gameObject.TryGetComponent<ScoreComet>(out _);
            var isAbility = other.gameObject.TryGetComponent<Ability>(out _);
            
            if (GetComponent<GhostEffect>())
            {
                if (isScoreComet || isAbility)
                {
                    if (isPoolObject)
                    {
                        PoolService.Instance.Despawn(poolObject);
                    }
                    
                    _leapDelta = 0;
                    _positiveLeapsCount++;
                }
                
                return;
            }
            
            if (isPoolObject)
            {
                PoolService.Instance.Despawn(poolObject);
            }
            
            _leapDelta = 0;
            _targetLeapRotation = _leapUpRotation;
            _positiveLeapsCount++;

            _leapPitch += LeapPitchDelta;
        }

        public void PlayLeapSound()
        {
            AudioManager.Instance.PlayLeapSound(_leapPitch);
        }

        public void Leap()
        {
            _leapDelta = 0;
            _targetLeapRotation = _leapUpRotation;
            _positiveLeapsCount++;
        }
    }
}