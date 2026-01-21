using Zenject;
using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    private PoolableObjectFactory _poolableObjectFactory;

    [SerializeField] private PoolableBullet _bullet;


    public override void InstallBindings()
    {
        Container.Bind<PoolableObjectFactory>().AsSingle();

        Container.Bind<PoolableBullet>().FromInstance(_bullet).AsSingle();

        Container.Bind<ObjectPool<PoolableBullet>>().AsSingle();
    }
}
