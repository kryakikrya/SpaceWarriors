using Zenject;
using UnityEngine;

public class PoolsInstaller : MonoInstaller
{
    private PoolableObjectFactory _poolableObjectFactory;

    [SerializeField] private PoolableBullet _bullet;
    [SerializeField] private GameObject _spawn;


    public override void InstallBindings()
    {
        Container.Bind<PoolableObjectFactory>().AsSingle();
    }
}
