using Zenject;

public class FragmentPresentation : PoolableObject
{
    [Inject] private ObjectPool<FragmentPresentation> _fragmentPool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is FragmentSettings)
        {
            base.InitializeInfo(settings);
            GetComponent<FragmentFacade>().InitializeInfo((FragmentSettings)settings);
        }
    }

    public override void Death()
    {
        _fragmentPool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
