using UnityEngine;

public abstract class PoolableObjectFactory : MonoBehaviour, IFactory<PoolableObject>
{
    public virtual void Create<TSettings>(PoolableObject poolableObject, TSettings settings)  where TSettings : IObjectSettings
    {
        Instantiate(poolableObject);
        poolableObject.InitializeInfo(settings);
    }
}
