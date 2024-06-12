using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class CircleCometStaticOrbitSpawner : Spawner
    {
        [SerializeField] private int _count;

        private List<Comet> _gameplayPoolObjects = new List<Comet>();
        private bool _wasSpawned; 
        
        public override void UnPause()
        {
            base.UnPause();

            if (_wasSpawned)
            {
                return;
            }
            
            var spawnDistance = Random.Range(_spawnCircleRange.x, _spawnCircleRange.y);
            
            var center = transform.position;
            var angle = Random.Range(0f, 360f);
            var angleStep = 360f / _count;
            
            for (var i = 0; i < _count; i++)
            {
                var position = new Vector3
                {
                    x = center.x + spawnDistance * Mathf.Sin(angle * Mathf.Deg2Rad),
                    y = center.y + spawnDistance * Mathf.Cos(angle * Mathf.Deg2Rad),
                    z = center.z,
                };
                
                position = Quaternion.AngleAxis(90, Vector3.forward) * position;
                angle += angleStep;
            
                var gameplayPoolObject = PoolService.Instance.Spawn(
                    _prefab,
                    position, 
                    Quaternion.identity, 
                    transform);
                
                _gameplayPoolObjects.Add(gameplayPoolObject);

                gameplayPoolObject.Init(
                    GetLinearSpeed(), 
                    GetOrbitSpeed(), 
                    GetRotation(position), 
                    GetScale());
            }

            _wasSpawned = true;
        }

        public override void Pause()
        {
            base.Pause();

            foreach (var circleComet in _gameplayPoolObjects)
            {
                PoolService.Instance.Despawn(circleComet);
            }
            
            _gameplayPoolObjects.Clear();
            
            _wasSpawned = false;
        }

        protected override void Spawn()
        {
        }
    }
}