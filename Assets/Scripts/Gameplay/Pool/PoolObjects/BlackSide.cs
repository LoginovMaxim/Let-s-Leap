namespace Gameplay
{
    public class BlackSide : CometSide
    {
        protected override void OnPlayerCollided()
        {
            GameManager.Instance.GameOver();
        }

        protected override void OnBorderCollided()
        {
            PoolService.Instance.Despawn(_comet);
        }
    }
}