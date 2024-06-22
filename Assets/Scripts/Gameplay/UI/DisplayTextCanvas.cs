using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public sealed class DisplayTextCanvas : PoolObject
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public override void Reinitialize(Vector3 position)
        {
            base.Reinitialize(position);
            _text.alpha = 1f;
            _text.transform.DOPunchScale(0.5f * Vector3.one, 0.5f, 2);
            _text.DOFade(0f, 1.5f).OnComplete(() => PoolService.Instance.Despawn(this));
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}