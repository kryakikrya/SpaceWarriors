public class PoolableObjectHealth : Health
{
    public PoolableObjectHealth(IObjectSettings settins) : base(settins)
    {

    }

    public override void Death()
    {
        OnObjectDeath?.Invoke();
    }
}
