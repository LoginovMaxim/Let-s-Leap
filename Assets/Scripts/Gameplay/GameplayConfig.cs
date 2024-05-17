using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Let's Leap/GameplayConfig")]
    public sealed class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public float WaveDuration { get; private set; }
        [field: SerializeField] public Vector2 CircleSpeedRange { get; private set; }
    }
}