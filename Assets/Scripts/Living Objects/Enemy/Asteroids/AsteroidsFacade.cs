using UnityEngine;

public class AsteroidsFacade : LivingFacade, INeedStartMove
{
    [SerializeField] private AsteroidMovement _movement;

    private AsteroidSettings _settings;

    private void Awake()
    {
        _health = new AsteroidsHealth(_maxHealth);
    }

    public void InitializeInfo(AsteroidSettings settings)
    {
        Debug.Log(3);
        _settings = settings;

        StartMove();
    }

    public void StartMove()
    {
        transform.localScale = Vector3.one * Random.Range(_settings.MinSize, _settings.MaxSize);

        Debug.Log($"Scale is {transform.localScale}");

        _movement.SetSpeed(Random.Range(_settings.MinSpeed, _settings.MaxSpeed));
    }
}
