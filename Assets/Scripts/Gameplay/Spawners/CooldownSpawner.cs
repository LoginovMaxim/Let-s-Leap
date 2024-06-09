using UnityEngine;

namespace Gameplay
{
    public class CooldownSpawner : Spawner
    {
        [Header("Cooldown Spawner")]
        [SerializeField] protected Vector2 _spawnPeriodRange;
        [SerializeField] protected Vector2Int _spawnCountRange;
        
        private float _spawnTime;

        public override void UnPause()
        {
            base.UnPause();
            _spawnTime = Time.time + GetSpawnPeriod();
        }

        private void Update()
        {
            if (_isPause)
            {
                return;
            }
            
            if (_spawnTime > Time.time)
            {
                return;
            }

            Spawn();
            
            _spawnTime = Time.time + GetSpawnPeriod();
        }
        
        protected override void Spawn()
        {
            var count = Random.Range(_spawnCountRange.x, _spawnCountRange.y);
            for (var i = 0; i < count; i++)
            {
                var spawnDistance = Random.Range(_spawnCircleRange.x, _spawnCircleRange.y);
                var randomDirectionX = Random.Range(-1f, 1f);
                var randomDirectionY = Random.Range(-1f, 1f);
                var randomSpawnPosition = new Vector3(randomDirectionX, randomDirectionY, 0f).normalized * spawnDistance;
            
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

        private float GetSpawnPeriod()
        {
            return Random.Range(_spawnPeriodRange.x, _spawnPeriodRange.y);
        }
    }
}