using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class PoolService : MonoSingleton<PoolService>
    {
        
        [SerializeField] private PoolObjectsConfig _poolObjectsConfig;
        
        private readonly Dictionary<PoolObjectId, Stack<PoolObject>> _poolObjectsByIds = new();
        
        private void Start()
        {
            _poolObjectsConfig.Init();
        }

        public TPoolObject Spawn<TPoolObject>(PoolObjectId poolObjectId, Transform parent) where TPoolObject : PoolObject
        {
            if (!_poolObjectsConfig.PoolObjectsByIds.TryGetValue(poolObjectId, out var poolObjectPrefab))
            {
                Debug.LogError($"Missing prefab for  pool object id: {poolObjectId}");
                return null;
            }
            
            if (TryGetPoolObjectFromPoolCache(poolObjectId, out var cachedPoolObject))
            {
                cachedPoolObject.Reinitialize();
                return (TPoolObject) cachedPoolObject;
            }
            
            var poolObject = Instantiate(poolObjectPrefab, parent);
            poolObject.PoolObjectId = poolObjectId;
            poolObject.Reinitialize();
            
            return (TPoolObject) poolObject;
        }

        public void Despawn(PoolObject poolObject)
        {
            poolObject.Deactivate();
            AddPoolObjectToPoolCache(poolObject.PoolObjectId, poolObject);
        }

        private bool TryGetPoolObjectFromPoolCache(PoolObjectId resourceId, out PoolObject poolObject)
        {
            poolObject = default;
            return _poolObjectsByIds.TryGetValue(resourceId, out var poolObjects) && poolObjects.TryPop(out poolObject);
        }

        private void AddPoolObjectToPoolCache(PoolObjectId resourceId, PoolObject poolObject)
        {
            if (_poolObjectsByIds.TryGetValue(resourceId, out var poolObjects))
            {
                poolObjects.Push(poolObject);
                return;
            }

            var poolObjectsStack = new Stack<PoolObject>();
            poolObjectsStack.Push(poolObject);
            
            _poolObjectsByIds.Add(resourceId, poolObjectsStack);
        }
    }
}