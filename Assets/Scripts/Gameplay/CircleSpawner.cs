using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class CircleSpawner : MonoBehaviour
    {
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private float _spawnPeriod;
        [SerializeField] private float _spawnCount;

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
                SpawnCircle();
            }
            
            _spawnTime = Time.time + _spawnPeriod;
        }

        private void SpawnCircle()
        {
            var randomSpawnPositionX = Random.Range(-5.5f, 5.5f);
            var randomSpawnPositionY = Random.Range(-5.5f, 5.5f);
            var randomSpawnPosition = new Vector3(randomSpawnPositionX, randomSpawnPositionY, 0f);
            
            var circle = PoolService.Instance.Spawn<Circle>(
                PoolObjectId.Circle, 
                randomSpawnPosition, 
                Quaternion.identity, 
                transform);
            
            var vectorToTarget = -circle.transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            
            circle.SetRotation(angle + 90);
            circle.SetSpeed(Random.Range(_gameplayConfig.CircleSpeedRange.x, _gameplayConfig.CircleSpeedRange.y));
        }
    }
}