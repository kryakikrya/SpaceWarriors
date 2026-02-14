using UnityEngine;
using Zenject;

[RequireComponent (typeof(BulletFacade))]
public class PoolableBullet : PoolableObject
{
    [Inject] private ObjectPool<PoolableBullet> _pool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is BulletSettings)
        {
            GetComponent<BulletFacade>().InitializeInfo((BulletSettings)settings);
        }
    }

    public override void Death()
    {
        Debug.Log("POOLABLE BULLET DEATH");

        _pool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
