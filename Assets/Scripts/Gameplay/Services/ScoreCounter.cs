using UnityEngine;

namespace Gameplay
{
    public sealed class ScoreCounter : MonoSingleton<ScoreCounter>
    {
        [SerializeField] private Star _star;
        
        private int _score;
        private int _scoreMultiplier;

        public int Score => _score;
        public int ScoreMultiplier => _scoreMultiplier;
        
        public void AddScore(int score)
        {
            _score += score * _scoreMultiplier;
            Hud.Instance.SetPointsAmount(_score);
        }

        public void IncreaseScoreMultiplier()
        {
            _scoreMultiplier++;
            _star.SetMultiplierValue(_scoreMultiplier);
        }

        public void ResetScore()
        {
            _score = 0;
            _scoreMultiplier = 1;
        }
    }
}