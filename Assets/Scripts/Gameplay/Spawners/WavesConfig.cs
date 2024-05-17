using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "WavesConfig", menuName = "Let's Leap/WavesConfig")]
    public sealed class WavesConfig : ScriptableObject
    {
        [field: SerializeField] public List<WaveData> WavesData { get; private set; } 
    }

    [Serializable]
    public struct WaveData
    {
        public Color BackgoundColor;
        public Color CameraBackgroundColor;
        public List<SpawnerId> SpawnerIds;
    }
}