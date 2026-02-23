using Zenject;
using UnityEngine;

public class PlayerFacade : LivingFacade
{
    [SerializeField] private PlayerInputs _inputs;

    [SerializeField] private GameObject _firePos;

    [SerializeField] private float _invulnerabilityTime;

    private PlayerShooter _shooter;

    private SignalBus _signalBus;

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

        _health = new PlayerHealth(_maxHealth, _invulnerability, _invulnerabilityTime, this, _signalBus);
    }

    public void Shoot()
    {
        _shooter.Shoot(_firePos.transform, transform.rotation.eulerAngles);
    }
}
