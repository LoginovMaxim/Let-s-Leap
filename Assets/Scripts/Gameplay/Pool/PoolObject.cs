using UnityEngine;

namespace Gameplay
{
    public abstract class PoolObject : MonoBehaviour
    {
        public abstract PoolObjectId PoolObjectId { get; }
        public bool IsActive => gameObject.activeSelf;
        
        public virtual void Reinitialize()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}