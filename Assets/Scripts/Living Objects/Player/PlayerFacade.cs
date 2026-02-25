using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerFacade : LivingFacade
{
    [SerializeField] private PlayerInputs _inputs;

    [SerializeField] private GameObject _firePos;

    [SerializeField] private float _invulnerabilityTime;

    [SerializeField] private GameObject _invulnerabilityEffect;

    [SerializeField] private PlayerLaser _laser;

    [SerializeField] private PlayerShipRotator _rotator;

    private PlayerShooter _shooter;

    private SignalBus _signalBus;

    private InvulnerabilityVisual _invulnerabilityVisual;

    [Inject]
    public void Construct(PoolableBullet bullet, ObjectPool<PoolableBullet> pool, PoolableObjectFactory factory, Invulnerability invulnerability, SignalBus bus, PlayerSettings settings)
    {
        _shooter = new PlayerShooter();

        _shooter.Initialize(pool, bullet);

        _invulnerability = invulnerability;

        _signalBus = bus;

        SetSettings(settings);
    }

    private void Start()
    {
        _inputs.Shooting += Shoot;

        if (_health == null)
        {
            InitializeHealth();
        }
    }

    private void SetSettings(PlayerSettings settings)
    {
        _inputs.SetSpeed(settings.Speed);
        _maxHealth = settings.Health;
        _laser.SetSettings(settings.MaxLasers, settings.LaserCD);
        _rotator.SetRotationSpeed(settings.RotationSpeed);
    }

    public void Shoot()
    {
        _shooter.Shoot(_firePos.transform, transform.rotation.eulerAngles);
    }

    private void InitializeHealth()
    {
        _health = new PlayerHealth(_maxHealth, _invulnerability, _invulnerabilityTime, _signalBus);

        _signalBus.Subscribe<PlayerDamagedSignal>(Invulnerability);
    }

    protected override void Enable()
    {
        base.Enable();

        InitializeHealth();

        _invulnerabilityVisual = new InvulnerabilityVisual(_invulnerabilityEffect, Health as PlayerHealth, _signalBus);

        _invulnerabilityVisual.Subscribe();
    }

    protected override void Disable()
    {
        base.Disable();

        _signalBus.Unsubscribe<PlayerDamagedSignal>(Invulnerability);

        _invulnerabilityVisual.Unsubscribe();
    }

    private async void Invulnerability(PlayerDamagedSignal signal)
    {
        EnableInvulnerability();

        await InvulnerabilityCD(signal.InvulnerabilityTime);

        DisableInvulnerability();
    }

    private async UniTask InvulnerabilityCD(float time)
    {
        await UniTask.WaitForSeconds(time);
    }
}
