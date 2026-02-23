using Zenject;

public class PoolableUFO : PoolableObject
{
    [Inject] private ObjectPool<PoolableUFO> _UFOPool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is UFOSettings)
        {
            GetComponent<UFOMovement>().InitializeInfo((UFOSettings)settings);
        }
    }

    public override void Death()
    {
        _UFOPool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
