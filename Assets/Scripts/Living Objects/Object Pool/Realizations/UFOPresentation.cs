using Zenject;
using UnityEngine;

public class UFOPresentation : PoolableObject
{
    [Inject] private ObjectPool<UFOPresentation> _UFOPool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is UFOSettings)
        {
            base.InitializeInfo(settings);
            GetComponent<UFOMovement>().InitializeInfo((UFOSettings)settings);
        }
    }

    public override void Death()
    {
        _UFOPool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
