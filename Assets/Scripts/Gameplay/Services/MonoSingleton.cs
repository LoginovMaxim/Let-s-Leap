using UnityEngine;

namespace Gameplay
{
    public abstract class MonoSingleton<T> : MonoBehaviour  where T : Component
    {
        public static T Instance { get; private set; }

        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this as T; 
            } 
        }
    }
}