public class UFOPresentation : PoolableObject
{
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
        base.Death();

        gameObject.SetActive(false);
    }
}
