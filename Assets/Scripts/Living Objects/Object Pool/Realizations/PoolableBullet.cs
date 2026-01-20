using UnityEngine;

[RequireComponent (typeof(BulletFacade))]
public class PoolableBullet : PoolableObject
{
    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is BulletSettings)
        {
            GetComponent<BulletFacade>().InitializeInfo((BulletSettings)settings);
        }
    }
}
