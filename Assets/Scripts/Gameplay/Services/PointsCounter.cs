namespace Gameplay
{
    public sealed class PointsCounter : MonoSingleton<PointsCounter>
    {
        private int _pointsAmount;
        private int _pointsMultiplier = 1;

        public int PointsAmount => _pointsAmount;
        public int PointsMultiplier => _pointsMultiplier;
        
        public void AddPoints(int pointsAmount)
        {
            _pointsAmount += pointsAmount * _pointsMultiplier;
            Hud.Instance.SetPointsAmount(_pointsAmount);
        }

        public void ResetPointsAmount()
        {
            _pointsAmount = 0;
            _pointsMultiplier = 1;
        }
    }
}