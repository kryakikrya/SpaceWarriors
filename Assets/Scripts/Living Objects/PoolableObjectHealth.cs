public class PoolableObjectHealth : Health
{
    public PoolableObjectHealth(int health) : base(health)
    {
    }

    public override void Death()
    {
        OnObjectDeath?.Invoke();
    }
}
