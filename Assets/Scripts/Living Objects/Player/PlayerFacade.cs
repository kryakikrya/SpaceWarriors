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
    public void Construct(BulletPresentation bullet, ObjectPool<BulletPresentation> pool, PoolableObjectFactory factory, Invulnerability invulnerability, SignalBus bus, PlayerParametersSettings settings, PlayerHealth health)
    {
        _shooter = new PlayerShooter();

        _shooter.Initialize(pool, bullet);

        _invulnerability = invulnerability;

        _signalBus = bus;

        _health = health;

        SetSettings(settings);
    }

    private void SetSettings(PlayerParametersSettings settings)
    {
        _inputs.SetSpeed(settings.Speed);

        _laser.SetSettings(settings.MaxLasers, settings.LaserCD, settings.LaserTime, settings.LaserDamagePerRate, settings.DamageRate);
        _rotator.SetRotationSpeed(settings.RotationSpeed);
    }

    public override void InitializePhysics()
    {
        _physics = new PlayerObjectPhysics();
        _physics.Initialize(GetComponent<Rigidbody2D>(), _physicsConfig);
    }

    public void Shoot()
    {
        _shooter.Shoot(_firePos.transform, transform.rotation.eulerAngles);
    }

    protected override void Enable()
    {
        base.Enable();

        _inputs.Source.Shooting += Shoot;

        _signalBus.Subscribe<PlayerDamagedSignal>(Invulnerability);

        _invulnerabilityVisual = new InvulnerabilityVisual(_invulnerabilityEffect, _signalBus);

        _invulnerabilityVisual.Subscribe();
    }

    protected override void Disable()
    {
        base.Disable();

        _inputs.Source.Shooting -= Shoot;

        _signalBus.Unsubscribe<PlayerDamagedSignal>(Invulnerability);

        _invulnerabilityVisual.Unsubscribe();
    }

    private async void Invulnerability(PlayerDamagedSignal signal)
    {
        EnableInvulnerability();

        await InvulnerabilityCD(signal.InvulnerabilityTime);

        if (this != null)
        {
            DisableInvulnerability();
        }
    }

    private async UniTask InvulnerabilityCD(float time)
    {
        await UniTask.WaitForSeconds(time);
    }
}
