using UnityEngine;

namespace Gameplay
{
    public abstract class Spawner : MonoBehaviour
    {
        [Header("Spawner")]
        [SerializeField] protected Comet _prefab;
        [SerializeField] protected Vector2 _spawnCircleRange;
        [SerializeField] protected Vector2 _linearSpeedRange;
        [SerializeField] protected Vector2 _orbitSpeedRange;
        [SerializeField] protected Vector2 _scaleRange;

        private Rigidbody2D _parentRigidbody;
        protected bool _isPause = true;

        public virtual void Pause()
        {
            _isPause = true;
        }

        public virtual void UnPause()
        {
            _isPause = false;
        }

        protected float GetLinearSpeed()
        {
            return Random.Range(_linearSpeedRange.x, _linearSpeedRange.y);
        }

        protected float GetOrbitSpeed()
        {
            return Random.Range(_orbitSpeedRange.x, _orbitSpeedRange.y);
        }

        protected float GetRotation(Vector2 position)
        {
            return Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg - 90;
        }
        
        protected float GetScale()
        {
            return Random.Range(_scaleRange.x, _scaleRange.y);
        }

        protected abstract void Spawn();
    }
}