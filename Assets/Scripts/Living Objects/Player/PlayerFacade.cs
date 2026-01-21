using Zenject;
using UnityEngine;

public class PlayerFacade : LivingFacade
{
    [SerializeField] private PlayerInputs _inputs;

    [SerializeField] private GameObject _firePos;

    private PlayerShooter _shooter;

    [Inject]
    public void Construct(PoolableBullet bullet, ObjectPool<PoolableBullet> pool, PoolableObjectFactory factory)
    {
        _shooter = new PlayerShooter();
        _shooter.Initialize(pool, bullet);

        pool.InitializeFactory(factory);
    }

    private void Awake()
    {
        _health = new PlayerHealth();
    }

    private void Start()
    {
        _inputs.Shooting += Shoot;
    }

    public void Shoot()
    {
        _shooter.Shoot(_firePos.transform, transform.rotation.eulerAngles);
    }
}
