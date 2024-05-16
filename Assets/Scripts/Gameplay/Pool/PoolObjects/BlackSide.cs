namespace Gameplay
{
    public class BlackSide : CometSide
    {
        protected override void OnPlayerCollided()
        {
            GameManager.Instance.GameOver();
        }
    }
}