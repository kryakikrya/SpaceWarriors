using UnityEngine;

public class AsteroidsFacade : LivingFacade
{
    [SerializeField] private AsteroidMovement _movement;

    private AsteroidSettings _settings;

    private void Awake()
    {
        _health = new AsteroidsHealth(_maxHealth);
    }

    public void InitializeInfo(AsteroidSettings settings)
    {
        _settings = settings;

        transform.localScale = Vector3.one * Random.Range(_settings.MinSize, _settings.MaxSize);

        _movement.SetSpeed(Random.Range(_settings.MinSpeed, _settings.MaxSpeed));
    }
}
