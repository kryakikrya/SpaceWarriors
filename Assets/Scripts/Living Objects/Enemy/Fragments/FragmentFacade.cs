using UnityEngine;


public class FragmentFacade : LivingFacade, INeedStartMove
{
    [SerializeField] private AsteroidMovement _movement;

    private FragmentSettings _settings;

    private void Awake()
    {
        _health = new PoolableObjectHealth(_maxHealth);
    }

    public void InitializeInfo(FragmentSettings settings)
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

    public override async void Death()
    {
        base.Death();

        FragmentVisual visual = new FragmentVisual();
        await visual.FireTask(transform, _settings.FireTime);

        if (_health.CurrentHealth > 0)
        {
            _health.Death();
        }
    }

    public override void DisableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.DisableInvulnerability(gameObject, _physicalLayers.DefaultLayer, _physicalLayers.InvulnerabilityLayer, _physicalLayers.FragmentLayer, _physicalLayers.EnemyLayer));
    }
}
