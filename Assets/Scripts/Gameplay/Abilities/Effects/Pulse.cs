using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public sealed class Pulse : MonoBehaviour
    {
        private const float PulseSize = 36;
        
        private void Start()
        {
            transform.DOScale(PulseSize * Vector3.one, 0.5f).SetEase(Ease.InExpo).OnComplete(() => Destroy(gameObject));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!TryGetCometTransform(other.gameObject, out var cometTransform))
            {
                return;
            }

            var cometPosition = cometTransform.position;
            var direction = (cometPosition - transform.position).normalized;
            var force = PulseSize / 2 / Vector3.Distance(cometPosition, transform.position);

            cometTransform.DOMove(cometPosition + direction * force, 0.5f).SetEase(Ease.OutExpo);
        }

        private bool TryGetCometTransform(GameObject gameObject, out Transform cometTransform)
        {
            cometTransform = null;
            if (gameObject.CompareTag("WhiteSide") || gameObject.CompareTag("BlackSide"))
            {
                cometTransform = gameObject.GetComponentInParent<Comet>().transform;
                return true;
            }

            return false;
        }
    }
}