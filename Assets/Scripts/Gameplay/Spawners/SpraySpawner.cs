using UnityEngine;

namespace Gameplay
{
    public class SpraySpawner : CooldownSpawner
    {
        protected override void Spawn()
        {
            var count = Random.Range(_spawnCountRange.x, _spawnCountRange.y);
            var spawnDistance = Random.Range(_spawnCircleRange.x, _spawnCircleRange.y);
            
            var center = transform.position;
            var angle = Random.Range(0f, 360f);
            var angleStep = 360f / count;

            var commonSpeed = GetLinearSpeed();
            var commonOrbitSpeed = GetOrbitSpeed();
            
            for (var i = 0; i < count; i++)
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

                gameplayPoolObject.Init(
                    commonSpeed, 
                    commonOrbitSpeed, 
                    GetRotation(position), 
                    GetScale());
            }
        }
    }
}