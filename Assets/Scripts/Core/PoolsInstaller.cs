using Zenject;
using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    private PoolableObjectFactory _poolableObjectFactory;

    [SerializeField] private BulletPresentation _bullet;

    public override void InstallBindings()
    {
        Container.Bind<ScoreRewardModel>().AsSingle();

        Container.Bind<PoolableObjectFactory>().AsSingle();

        Container.Bind<BulletPresentation>().FromInstance(_bullet).AsSingle();

        Container.Bind<ObjectPool<BulletPresentation>>().AsSingle();

        Container.Bind<ObjectPool<AsteroidPresentation>>().AsSingle();

        Container.Bind<ObjectPool<FragmentPresentation>>().AsSingle();

        Container.Bind<ObjectPool<UFOPresentation>>().AsSingle();
    }
}
