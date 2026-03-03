using UnityEngine;

public class PlayerShooter
{
    private const string JsonName = "BulletConfig.json";

    private BulletPresentation _bullet;
    private ObjectSettingsProvider _settingsProvider;
    private PoolableObjectFactory<BulletPresentation> _factory;

    public void Initialize(PoolableObjectFactory<BulletPresentation> factory, BulletPresentation bullet, ObjectSettingsProvider provider)
    {
        _factory = factory;
        _bullet = bullet;
        _settingsProvider = provider;
    }

    public void Shoot(Transform firePosition, Vector3 playerRotation)
    {
        PoolableObject bullet = _factory.Create(_bullet);

        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = Quaternion.Euler(playerRotation);

        bullet.InitializeInfo(_settingsProvider.Get<BulletPresentation>());
    }
}
