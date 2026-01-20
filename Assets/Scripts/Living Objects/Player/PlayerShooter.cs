using UnityEngine;

public class PlayerShooter
{
    private const string JsonName = "BulletConfig.json";

    private PoolableObjectFactory _bulletFactory;
    private PoolableObject _bullet;

    public void Initialize(PoolableObjectFactory bulletFactory, PoolableObject bullet)
    {
        _bulletFactory = bulletFactory;
        _bullet = bullet;
    }

    public void Shoot(Transform firePosition, Vector3 playerRotation)
    {
        _bulletFactory.Create(_bullet, JsonName, firePosition.position, playerRotation);
    }
}
