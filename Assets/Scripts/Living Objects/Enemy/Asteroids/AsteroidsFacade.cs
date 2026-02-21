using UnityEngine;

public class AsteroidsFacade : LivingFacade, INeedStartMove
{
    [SerializeField] private AsteroidMovement _movement;

    private AsteroidSettings _settings;

    private void Awake()
    {
        _health = new PoolableObjectHealth(_maxHealth);
    }

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

    public override void DisableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.DisableInvulnerability(gameObject, _physicalLayers.EnemyLayer, _physicalLayers.InvulnerabilityLayer));
    }
}
