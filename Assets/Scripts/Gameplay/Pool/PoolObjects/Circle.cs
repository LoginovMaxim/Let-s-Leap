using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class Circle : GameplayPoolObject
    {
        public override PoolObjectId PoolObjectId => PoolObjectId.Circle;

        public override void Reinitialize()
        {
            base.Reinitialize();

            transform.localScale = Vector3.one * 0.2f;
            
            var randomScale = Random.Range(0.6f, 1.2f);
            transform.DOScale(randomScale * Vector3.one, 2f).SetEase(Ease.InSine);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.layer != 3)
            {
                return;
            }
            
            PointsCounter.Instance.AddPoints(_points);
        }
    }
}