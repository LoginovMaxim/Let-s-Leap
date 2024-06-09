using UnityEngine;

namespace Gameplay
{
    public sealed class PointsCounter : MonoSingleton<PointsCounter>
    {
        [SerializeField] private Star _star;
        
        private int _pointsAmount;
        private int _pointsMultiplier;

        public int PointsAmount => _pointsAmount;
        public int PointsMultiplier => _pointsMultiplier;
        
        public void AddPoints(int pointsAmount)
        {
            _pointsAmount += pointsAmount * _pointsMultiplier;
            Hud.Instance.SetPointsAmount(_pointsAmount);
        }

        public void IncreaseMultiplier()
        {
            _pointsMultiplier++;
            _star.SetMultiplierValue(_pointsMultiplier);
        }

        public void ResetPointsAmount()
        {
            _pointsAmount = 0;
            _pointsMultiplier = 1;
        }
    }
}