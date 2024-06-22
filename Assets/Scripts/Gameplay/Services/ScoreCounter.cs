using LetsLeap.Game.Audio;
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
            var resultScore = score * _scoreMultiplier;
            _score += resultScore;
            Hud.Instance.SetPointsAmount(_score);

            var playerPosition = GameManager.Instance.Player.transform.position;
            var scoreHitDisplay = PoolService.Instance.Spawn<DisplayTextCanvas>(playerPosition, Quaternion.identity, null);
            scoreHitDisplay.SetText($"+{resultScore}");
        }

        public void IncreaseScoreMultiplier()
        {
            _scoreMultiplier++;
            _star.SetMultiplierValue(_scoreMultiplier);

            if (ScoreMultiplier <= 1)
            {
                return;
            }
            
            AudioManager.Instance.PlayCrossLineSound();

            var playerPosition = GameManager.Instance.Player.transform.position + 0.25f * Vector3.up;
            var scoreHitDisplay = PoolService.Instance.Spawn<DisplayTextCanvas>(playerPosition, Quaternion.identity, null);
            scoreHitDisplay.SetText($"множитель х{ScoreMultiplier}");
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