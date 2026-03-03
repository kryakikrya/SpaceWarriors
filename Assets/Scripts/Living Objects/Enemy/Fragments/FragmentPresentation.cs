public class FragmentPresentation : PoolableObject
{
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
        gameObject.SetActive(false);
    }
}
