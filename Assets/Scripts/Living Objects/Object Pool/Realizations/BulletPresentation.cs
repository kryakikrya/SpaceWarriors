using UnityEngine;

[RequireComponent (typeof(BulletFacade))]
public class BulletPresentation : PoolableObject
{
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
        base.Death();

        gameObject.SetActive(false);
    }
}
