using UnityEngine;

namespace Gameplay
{
    public sealed class PlayerLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _starTransform;
        
        private void Update()
        {
            _lineRenderer.positionCount = 2;
            
            _lineRenderer.SetPosition(0, _playerTransform.position);
            _lineRenderer.SetPosition(1, _starTransform.position);
        }
    }
}