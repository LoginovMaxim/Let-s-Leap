using System;
using System.Collections.Generic;
using UnityEngine;

namespace LetsLeap.Game
{
    [CreateAssetMenu(fileName = "SkinsConfig", menuName = "Let's Leap/SkinsConfig")]
    public sealed class SkinsConfig : ScriptableObject
    {
        [field: SerializeField] public List<SkinData> SkinData { get; private set; }
    }

    [Serializable]
    public class SkinData
    {
        public Sprite Icon;
        public string Name;
        public string Description;
        public bool IsUnlocked;
        public bool IsSelected;
    }
}