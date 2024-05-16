using UnityEngine;

namespace Gameplay
{
    public sealed class CircleCometSpawner : Spawner
    {
        protected override void Spawn()
        {
            var spawnDistance = Random.Range(_spawnRangeIn, _spawnRangeOut);
            var randomDirectionX = Random.Range(-1f, 1f);
            var randomDirectionY = Random.Range(-1f, 1f);
            var randomSpawnPosition = new Vector3(randomDirectionX, randomDirectionY, 0f).normalized * spawnDistance;
            
            var circleComet = PoolService.Instance.Spawn<CircleComet>(
                PoolObjectId.CircleComet, 
                randomSpawnPosition, 
                Quaternion.identity, 
                transform);
            
            var vectorToTarget = -circleComet.transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            
            circleComet.SetRotation(angle + 90);
            circleComet.SetSpeed(0f);
        }
    }
}