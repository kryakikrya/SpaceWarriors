public class AsteroidsFacade : LivingFacade
{
    private AsteroidSettings _settings;

    private void Awake()
    {
        _health = new AsteroidsHealth();

        _health.InitializeHealth(_maxHealth);
    }

    public void InitializeInfo(AsteroidSettings settings)
    {
        _settings = settings;
    }
}
