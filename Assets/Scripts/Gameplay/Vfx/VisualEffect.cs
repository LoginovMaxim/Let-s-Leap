
namespace Gameplay.Vfx
{
    public abstract class VisualEffect : PoolObject
    {
        private void OnParticleSystemStopped()
        {
            PoolService.Instance.Despawn(this);
        }
    }
}