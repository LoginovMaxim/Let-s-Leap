using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    [Serializable]
    public sealed class CircleSpawner : Spawner
    {
        protected override void Spawn()
        {
            var spawnDistance = Random.Range(_spawnCircleRange.x, _spawnCircleRange.y);
            var randomDirectionX = Random.Range(-1f, 1f);
            var randomDirectionY = Random.Range(-1f, 1f);
            var randomSpawnPosition = new Vector3(randomDirectionX, randomDirectionY, 0f) * spawnDistance;
            
            var gameplayPoolObject = PoolService.Instance.Spawn(
                _prefab,
                randomSpawnPosition, 
                Quaternion.identity, 
                transform);

            gameplayPoolObject.Init(
                GetLinearSpeed(), 
                GetOrbitSpeed(), 
                GetRotation(randomSpawnPosition), 
                GetScale());
        }
    }
}