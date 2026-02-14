public class BulletHealth : Health
{
    public override void Death()
    {
        OnObjectDeath?.Invoke();
    }
}
