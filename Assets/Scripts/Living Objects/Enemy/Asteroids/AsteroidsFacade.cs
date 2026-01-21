public class AsteroidsFacade : LivingFacade
{
    private void Awake()
    {
        _health = new AsteroidsHealth();

        _health.InitializeHealth(_maxHealth);
    }
}
