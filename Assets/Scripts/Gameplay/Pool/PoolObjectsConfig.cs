using System;
using System.Collections.Generic;
using Gameplay.Extensions;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PoolObjectsConfig", menuName = "Let's Leap/PoolObjectsConfig")]
    public sealed class PoolObjectsConfig : ScriptableObject
    {
        [field: SerializeField] public List<PoolObjectData> PoolObjectData { get; private set; }

        public IReadOnlyDictionary<string, PoolObject> PoolObjectsByIds => _poolObjectsByIds;
        private Dictionary<string, PoolObject> _poolObjectsByIds;

        private void OnValidate()
        {
            foreach (var poolObjectData in PoolObjectData)
            {
                poolObjectData.PoolObjectId = poolObjectData.PoolObject.GetType().ToString().GetShortTypeName();
            }
        }

        public void Init()
        {
            _poolObjectsByIds = new Dictionary<string, PoolObject>();

            foreach (var poolObjectData in PoolObjectData)
            {
                _poolObjectsByIds.Add(poolObjectData.PoolObjectId, poolObjectData.PoolObject);
            }
        }
    }

    [Serializable]
    public class PoolObjectData
    {
        public string PoolObjectId;
        public PoolObject PoolObject;
    }
}