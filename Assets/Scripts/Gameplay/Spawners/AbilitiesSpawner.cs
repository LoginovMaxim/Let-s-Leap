using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class AbilitiesSpawner : MonoBehaviour
    {
        [SerializeField] private List<PoolObject> _abilities;
        [SerializeField] private Vector2 _spawnCircleRange;
        [SerializeField] private Vector2 _spawnPeriodRange;
        [SerializeField] private Vector2Int _spawnCountRange;
        
        private float _spawnTime;
        private float _decreasedSpawnTime;

        private void Start()
        {
            _spawnTime = Time.time + GetSpawnPeriod();
        }

        private void Update()
        {
            if (_spawnTime > Time.time)
            {
                return;
            }

            Spawn();
            
            _spawnTime = Time.time + GetSpawnPeriod();
        }

        public void OnNextWaveStarted()
        {
            _decreasedSpawnTime += 0.5f;
        }
        
        private void Spawn()
        {
            var count = Random.Range(_spawnCountRange.x, _spawnCountRange.y);
            for (var i = 0; i < count; i++)
            {
                var spawnDistance = Random.Range(_spawnCircleRange.x, _spawnCircleRange.y);
                var randomDirectionX = Random.Range(-1f, 1f);
                var randomDirectionY = Random.Range(-1f, 1f);
                var randomSpawnPosition = new Vector3(randomDirectionX, randomDirectionY, 0f).normalized * spawnDistance;

                var ability = _abilities[Random.Range(0, _abilities.Count)];
                
                PoolService.Instance.Spawn(
                    ability,
                    randomSpawnPosition, 
                    Quaternion.identity, 
                    transform);
            }
        }

        private float GetSpawnPeriod()
        {
            return Random.Range(_spawnPeriodRange.x, _spawnPeriodRange.y) - _decreasedSpawnTime;
        }
    }
}