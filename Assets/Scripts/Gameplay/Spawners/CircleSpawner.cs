using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class CircleSpawner : Spawner
    {
        protected override void Spawn()
        {
            var spawnDistance = Random.Range(_spawnRangeIn, _spawnRangeOut);
            var randomDirectionX = Random.Range(-1f, 1f);
            var randomDirectionY = Random.Range(-1f, 1f);
            var randomSpawnPosition = new Vector3(randomDirectionX, randomDirectionY, 0f) * spawnDistance;
            
            var circle = PoolService.Instance.Spawn<Circle>(
                PoolObjectId.Circle, 
                randomSpawnPosition, 
                Quaternion.identity, 
                _spawnParent);
            
            var vectorToTarget = -circle.transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            
            circle.SetRotation(angle + 90);
            circle.SetSpeed(Random.Range(_gameplayConfig.CircleSpeedRange.x, _gameplayConfig.CircleSpeedRange.y));
        }
    }
}