using Zenject;
using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    [SerializeField] private BulletPresentation _bullet;

    public override void InstallBindings()
    {
        Container.Bind<PoolableObjectFactory<AsteroidPresentation>>().AsSingle();

        Container.Bind<PoolableObjectFactory<BulletPresentation>>().AsSingle();

        Container.Bind<PoolableObjectFactory<FragmentPresentation>>().AsSingle();

        Container.Bind<PoolableObjectFactory<UFOPresentation>>().AsSingle();

        Container.Bind<BulletPresentation>().FromInstance(_bullet).AsSingle();
    }
}
