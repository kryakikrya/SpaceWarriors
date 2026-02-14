public class AsteroidsHealth : Health
{
    public override void Death()
    {
        OnObjectDeath?.Invoke();
    }
}
