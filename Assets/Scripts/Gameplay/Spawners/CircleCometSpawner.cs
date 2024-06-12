using UnityEngine;

namespace Gameplay
{
    public sealed class CircleCometSpawner : Spawner
    {
        protected override void Spawn()
        {
            Vector3 randomSpawnPosition;
            while (true)
            {
                var spawnDistance = Random.Range(_spawnCircleRange.x, _spawnCircleRange.y);
                var randomDirectionX = Random.Range(-1f, 1f);
                var randomDirectionY = Random.Range(-1f, 1f);
                randomSpawnPosition = new Vector3(randomDirectionX, randomDirectionY, 0f).normalized * spawnDistance;

                if ((GameManager.Instance.Player.transform.position - randomSpawnPosition).sqrMagnitude > 4f)
                {
                    break;
                }
            }
            
            var comet = (Comet) PoolService.Instance.Spawn(
                _prefab,
                randomSpawnPosition, 
                Quaternion.identity, 
                transform);

            comet.Init(
                GetLinearSpeed(), 
                GetOrbitSpeed(), 
                GetRotation(randomSpawnPosition), 
                GetScale());
        }
    }
}