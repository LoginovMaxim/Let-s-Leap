using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PoolObjectsConfig", menuName = "Let's Leap/PoolObjectsConfig")]
    public sealed class PoolObjectsConfig : ScriptableObject
    {
        [field: SerializeField] public List<PoolObjectData> PoolObjectData { get; private set; }

        public IReadOnlyDictionary<PoolObjectId, PoolObject> PoolObjectsByIds => _poolObjectsByIds;
        private Dictionary<PoolObjectId, PoolObject> _poolObjectsByIds;

        public void Init()
        {
            _poolObjectsByIds = new Dictionary<PoolObjectId, PoolObject>();

            foreach (var poolObjectData in PoolObjectData)
            {
                _poolObjectsByIds.Add(poolObjectData.PoolObjectId, poolObjectData.PoolObject);
            }
        }
    }

    [Serializable]
    public struct PoolObjectData
    {
        public PoolObjectId PoolObjectId;
        public PoolObject PoolObject;
    }
}