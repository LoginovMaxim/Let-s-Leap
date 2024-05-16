using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public sealed class CircleComet : Comet
    {
        public override PoolObjectId PoolObjectId => PoolObjectId.CircleComet;

        public override void Reinitialize()
        {
            base.Reinitialize();

            transform.localScale = Vector3.zero;
            
            var randomScale = Random.Range(0.66f, 1.33f);
            transform.DOScale(randomScale * Vector3.one, 1f).SetEase(Ease.InSine);
        }
    }
}