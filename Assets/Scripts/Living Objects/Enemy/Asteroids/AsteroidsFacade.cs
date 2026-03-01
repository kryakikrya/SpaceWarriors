using UnityEngine;

public class AsteroidsFacade : PoolableObjectFacade, INeedStartMove
{
    [SerializeField] private AsteroidMovement _movement;

    private AsteroidSettings _settings;

    public void InitializeInfo(AsteroidSettings settings)
    {
        _health.HealToMax();

        _settings = settings;

        transform.localScale = Vector3.one * Random.Range(_settings.MinSize, _settings.MaxSize);

        StartMove();
    }

    public void StartMove()
    {
        _movement.SetSpeed(_settings.Speed);
    }
}
