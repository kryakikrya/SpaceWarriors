using Zenject;
using UnityEngine;

public class PlayerFacade : LivingFacade
{
    [SerializeField] private PlayerInputs _inputs;

    [SerializeField] private GameObject _firePos;

    [SerializeField] private float _invulnerabilityTime;

    [SerializeField] private GameObject _invulnerabilityEffect;

    private PlayerShooter _shooter;

    private SignalBus _signalBus;

    private InvulnerabilityVisual _invulnerabilityVisual;

    [Inject]
    public void Construct(PoolableBullet bullet, ObjectPool<PoolableBullet> pool, PoolableObjectFactory factory, Invulnerability invulnerability, SignalBus bus)
    {
        _shooter = new PlayerShooter();

        _shooter.Initialize(pool, bullet);

        _invulnerability = invulnerability;

        _signalBus = bus;
    }

    private void Start()
    {
        _inputs.Shooting += Shoot;

        InitializeHealth();
    }

    public void Shoot()
    {
        _shooter.Shoot(_firePos.transform, transform.rotation.eulerAngles);
    }

    private void InitializeHealth()
    {
        _health = new PlayerHealth(_maxHealth, _invulnerability, _invulnerabilityTime, this, _signalBus);
    }

    protected override void Enable()
    {
        base.Enable();

        InitializeHealth();

        _invulnerabilityVisual = new InvulnerabilityVisual(_invulnerabilityEffect, Health as PlayerHealth);

        _invulnerabilityVisual.Subscribe();
    }

    protected override void Disable()
    {
        base.Disable();

        _invulnerabilityVisual.Unsubscribe();
    }
}
