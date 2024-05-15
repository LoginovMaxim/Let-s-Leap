using UnityEngine;

namespace Gameplay
{
    public sealed class Circle : PoolObject
    {
        public int Points;
        
        private void Start()
        {
            PoolObjectId = PoolObjectId.Circle;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.layer != 3)
            {
                return;
            }
            
            PointsCounter.Instance.AddPoints(Points);
        }
    }
}