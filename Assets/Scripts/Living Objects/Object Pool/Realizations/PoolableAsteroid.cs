using UnityEngine;
using Zenject;

public class PoolableAsteroid : PoolableObject
{
    [Inject] private ObjectPool<PoolableAsteroid> _pool;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is AsteroidSettings)
        {
            GetComponent<AsteroidsFacade>().InitializeInfo((AsteroidSettings) settings);
        }
    }

    public override void Death()
    {
        Debug.Log("POOLABLE ASTEROID DEATH");

        _pool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
