using UnityEngine;

namespace Gameplay
{
    public sealed class BulletsAbility : Ability
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletsAmount;
        [SerializeField] private float _bulletsSpeed;
        protected override string AbilityName => "выстрел";
        
        protected override void ApplyAbility(Player player)
        {
            var spawnDistance = 0.1f;
            
            var center = transform.position;
            var angle = Random.Range(0f, 360f);
            var angleStep = 360f / _bulletsAmount;

            for (var i = 0; i < _bulletsAmount; i++)
            {
                var position = new Vector3
                {
                    x = center.x + spawnDistance * Mathf.Sin(angle * Mathf.Deg2Rad),
                    y = center.y + spawnDistance * Mathf.Cos(angle * Mathf.Deg2Rad),
                    z = center.z,
                };

                //position = Quaternion.AngleAxis(90, Vector3.forward) * position;
                angle += angleStep;

                var bullet = (Comet) PoolService.Instance.Spawn(
                    _bulletPrefab,
                    position,
                    Quaternion.identity,
                    null);
                
                var rotation = Mathf.Atan2(position.y - center.y, position.x - center.x) * Mathf.Rad2Deg - 90;
                bullet.Init(_bulletsSpeed, 0f, rotation, 1f);
            }
        }
    }
}