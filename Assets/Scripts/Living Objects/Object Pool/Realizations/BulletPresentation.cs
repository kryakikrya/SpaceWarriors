using UnityEngine;
using Zenject;

[RequireComponent (typeof(BulletFacade))]
public class BulletPresentation : PoolableObject
{
    [Inject] private ObjectPool<BulletPresentation> _pool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is BulletSettings)
        {
            base.InitializeInfo(settings);
            GetComponent<BulletFacade>().InitializeInfo((BulletSettings)settings);
        }
    }

    public override void Death()
    {
        _pool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
