using UnityEngine;

namespace Gameplay
{
    public sealed class ScoreCounter : MonoSingleton<ScoreCounter>
    {
        [SerializeField] private Star _star;
        
        private int _score;
        private int _scoreMultiplier;
        private int _appliedAbilitiesAmount;
        private int _destroyedCometsAmount;

        public int Score => _score;
        public int ScoreMultiplier => _scoreMultiplier;
        public int AppliedAbilitiesAmount => _appliedAbilitiesAmount;
        public int DestroyedCometsAmount => _destroyedCometsAmount;
        
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
            _appliedAbilitiesAmount = 0;
        }

        public void OnAbilityApplied()
        {
            _appliedAbilitiesAmount++;
        }

        public void OnCometDestroyed()
        {
            _destroyedCometsAmount++;
        }
    }
}