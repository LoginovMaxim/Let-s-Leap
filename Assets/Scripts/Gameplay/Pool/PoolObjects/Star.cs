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

            var player = other.transform.gameObject.GetComponent<Player>();
            if (player.gameObject.GetComponent<StarEffect>())
            {
                player.Leap();
                player.PlayLeapSound();
                return;
            }
            
            GameManager.Instance.GameOver();
        }
    }
}