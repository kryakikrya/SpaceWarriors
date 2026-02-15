using UnityEngine;

public class PlayerShooter
{
    private const string JsonName = "BulletConfig.json";

    private ObjectPool<PoolableBullet> _pool;
    private PoolableBullet _bullet;

    public void Initialize(ObjectPool<PoolableBullet> pool, PoolableBullet bullet)
    {
        _pool = pool;
        _bullet = bullet;
    }

    public void Shoot(Transform firePosition, Vector3 playerRotation)
    {
        _pool.GetAvailableObject<BulletSettings>(_bullet, JsonName, firePosition.position, playerRotation);
    }
}
