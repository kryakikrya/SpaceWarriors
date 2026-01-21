using UnityEngine;

public abstract class PoolableObject : MonoBehaviour, IPoolableObject<IObjectSettings>
{
    public abstract void InitializeInfo(IObjectSettings settings);

    public abstract void Death();
}
