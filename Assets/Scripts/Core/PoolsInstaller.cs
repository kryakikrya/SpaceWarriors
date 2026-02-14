using Zenject;
using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    private PoolableObjectFactory _poolableObjectFactory;

    [SerializeField] private PoolableBullet _bullet;

    [SerializeField] private PoolableAsteroid _asteroid;

    public override void InstallBindings()
    {
        Container.Bind<PoolableObjectFactory>().AsSingle();

        Container.Bind<PoolableBullet>().FromInstance(_bullet).AsSingle();

        Container.Bind<ObjectPool<PoolableBullet>>().AsSingle();

        Container.Bind<PoolableAsteroid>().FromInstance(_asteroid).AsSingle();

        Container.Bind<ObjectPool<PoolableAsteroid>>().AsSingle();
    }
}
