using Gameplay.Extensions;
using UnityEngine;

namespace Gameplay
{
    public abstract class PoolObject : MonoBehaviour
    {
        public string PoolObjectId => GetType().ToString().GetShortTypeName();
        public bool IsActive => gameObject.activeSelf;
        
        public virtual void Reinitialize(Vector3 position)
        {
            transform.position = position;
            transform.localScale = Vector3.one;
            
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}