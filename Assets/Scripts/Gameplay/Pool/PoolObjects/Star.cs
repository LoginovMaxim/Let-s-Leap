using TMPro;
using UnityEngine;

namespace Gameplay
{
    public sealed class Star : BlackSide
    {
        [SerializeField] private TextMeshProUGUI _multiplierText;

        public void SetMultiplierValue(int value)
        {
            _multiplierText.text = $"x{value}";
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.layer != 3)
            {
                return;
            }
            
            GameManager.Instance.GameOver();
        }
    }
}