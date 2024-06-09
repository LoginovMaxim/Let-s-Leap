using System.Collections.Generic;
using Gameplay.Extensions;
using UnityEngine;

namespace Gameplay
{
    public sealed class PoolService : MonoSingleton<PoolService>
    {
        [SerializeField] private PoolObjectsConfig _poolObjectsConfig;
        
        private readonly Dictionary<string, Stack<PoolObject>> _poolObjectsByIds = new();
        
        private void Start()
        {
            _poolObjectsConfig.Init();
        }

        public TPoolObject Spawn<TPoolObject>(
            Vector3 position, 
            Quaternion rotation, 
            Transform parent) 
            where TPoolObject : PoolObject
        {
            var poolObjectId = typeof(TPoolObject).ToString().GetShortTypeName();
            if (!_poolObjectsConfig.PoolObjectsByIds.TryGetValue(poolObjectId, out var poolObjectPrefab))
            {
                Debug.LogError($"Missing prefab for  pool object id: {poolObjectId}");
                return null;
            }
            
            if (TryGetPoolObjectFromPoolCache(poolObjectId, out var cachedPoolObject))
            {
                cachedPoolObject.Reinitialize(position);
                return (TPoolObject) cachedPoolObject;
            }
            
            var poolObject = Instantiate(poolObjectPrefab, position, rotation, parent);
            poolObject.Reinitialize(position);
            
            return (TPoolObject) poolObject;
        }

        public TPoolObject Spawn<TPoolObject>(
            TPoolObject poolObjectPrefab,
            Vector3 position, 
            Quaternion rotation, 
            Transform parent) 
            where TPoolObject : PoolObject
        {
            var poolObjectId = typeof(TPoolObject).ToString().GetShortTypeName();
            
            if (TryGetPoolObjectFromPoolCache(poolObjectId, out var cachedPoolObject))
            {
                cachedPoolObject.Reinitialize(position);
                return (TPoolObject) cachedPoolObject;
            }
            
            var poolObject = Instantiate(poolObjectPrefab, position, rotation, parent);
            poolObject.Reinitialize(position);
            
            return poolObject;
        }

        public void Despawn(PoolObject poolObject)
        {
            poolObject.Deactivate();
            AddPoolObjectToPoolCache(poolObject.PoolObjectId, poolObject);
        }

        private bool TryGetPoolObjectFromPoolCache(string poolObjectId, out PoolObject poolObject)
        {
            poolObject = default;
            return _poolObjectsByIds.TryGetValue(poolObjectId, out var poolObjects) && poolObjects.TryPop(out poolObject);
        }

        private void AddPoolObjectToPoolCache(string poolObjectId, PoolObject poolObject)
        {
            if (_poolObjectsByIds.TryGetValue(poolObjectId, out var poolObjects))
            {
                poolObjects.Push(poolObject);
                return;
            }

            var poolObjectsStack = new Stack<PoolObject>();
            poolObjectsStack.Push(poolObject);
            
            _poolObjectsByIds.Add(poolObjectId, poolObjectsStack);
        }
    }
}