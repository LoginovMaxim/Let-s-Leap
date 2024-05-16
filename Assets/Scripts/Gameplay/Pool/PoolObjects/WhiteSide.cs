namespace Gameplay
{
    public class WhiteSide : CometSide
    {
        private Comet _comet;

        private void Start()
        {
            _comet = GetComponentInParent<Comet>();
        }
        
        protected override void OnPlayerCollided()
        {
            PointsCounter.Instance.AddPoints(_comet.Points);
            PoolService.Instance.Despawn(_comet);
        }
    }
}