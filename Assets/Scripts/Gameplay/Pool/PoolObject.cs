using UnityEngine;

namespace Gameplay
{
    public abstract class PoolObject : MonoBehaviour
    {
        public string PoolObjectId
        {
            get
            {
                var poolObjectId = gameObject.name;
                if (poolObjectId.Contains("(Clone)"))
                {
                    poolObjectId = poolObjectId.Replace("(Clone)", "");
                }

                return poolObjectId;
            }
        }
        public bool IsActive => gameObject.activeSelf;
        
        public virtual void Reinitialize(Vector3 position)
        {
            transform.position = position;
            transform.localScale = Vector3.one;
            transform.localScale = Vector3.one;
            
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}