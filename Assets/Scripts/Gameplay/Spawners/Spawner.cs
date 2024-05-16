using UnityEngine;

namespace Gameplay
{
    public abstract class Spawner : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField] protected GameplayConfig _gameplayConfig;
        [SerializeField] protected Transform _spawnParent;
        [SerializeField] protected float _spawnPeriod;
        [SerializeField] protected float _spawnCount;
        [SerializeField] protected float _spawnRangeIn;
        [SerializeField] protected float _spawnRangeOut;

        private float _spawnTime;

        private void Start()
        {
            _spawnTime = Time.time + _spawnPeriod;
        }

        private void Update()
        {
            if (_spawnTime > Time.time)
            {
                return;
            }

            for (var i = 0; i < _spawnCount; i++)
            {
                Spawn();
            }
            
            _spawnTime = Time.time + _spawnPeriod;
        }

        protected abstract void Spawn();
    }
}