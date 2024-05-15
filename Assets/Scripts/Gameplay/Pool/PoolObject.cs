using UnityEngine;

namespace Gameplay
{
    public abstract class PoolObject : MonoBehaviour
    {
        public PoolObjectId PoolObjectId { get; set; }
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