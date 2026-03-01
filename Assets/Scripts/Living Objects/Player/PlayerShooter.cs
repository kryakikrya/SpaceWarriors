using UnityEngine;

public class PlayerShooter
{
    private const string JsonName = "BulletConfig.json";

    private ObjectPool<BulletPresentation> _pool;
    private BulletPresentation _bullet;

    public void Initialize(ObjectPool<BulletPresentation> pool, BulletPresentation bullet)
    {
        _pool = pool;
        _bullet = bullet;
    }

    public void Shoot(Transform firePosition, Vector3 playerRotation)
    {
        _pool.GetAvailableObject<BulletSettings>(_bullet, JsonName, firePosition.position, playerRotation);
    }
}
