using UnityEngine;

namespace Gameplay
{
    public sealed class Rotator : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void Update()
        {
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
        }
    }
}