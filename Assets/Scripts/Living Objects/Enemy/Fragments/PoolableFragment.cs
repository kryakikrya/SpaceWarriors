using UnityEngine;
using Zenject;

public class PoolableFragment : PoolableObject
{
    [Inject] private ObjectPool<PoolableFragment> _fragmentPool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is FragmentSettings)
        {
            GetComponent<FragmentFacade>().InitializeInfo((FragmentSettings)settings);
        }
    }

    public override void Death()
    {
        _fragmentPool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
