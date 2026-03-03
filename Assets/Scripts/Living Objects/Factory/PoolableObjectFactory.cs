using UnityEngine;
using Zenject;

public class PoolableObjectFactory<T> where T : PoolableObject, new()
{
    private DiContainer _container;

    private ObjectSettingsProvider _settingsProvider;

    private ObjectPool<T> _pool;

    public ObjectPool<T> Pool => _pool;

    public PoolableObjectFactory(DiContainer container, ObjectSettingsProvider provider)
    {
        _container = container;

        _pool = new ObjectPool<T>();
    }

    public virtual PoolableObject Create(PoolableObject poolableObject, Vector3 spawnPoint = default, Vector3 direction = default)
    {
        if (_pool.Get(out PoolableObject newObject))
        {
            Debug.Log("Взят из пула");
        }
        else
        {
            GameObject obj = _container.InstantiatePrefab(poolableObject.gameObject, spawnPoint, Quaternion.Euler(direction), null);
            newObject = obj.GetComponent<PoolableObject>();

            _pool.AddObject((T)newObject);

            Debug.Log("Создан новый");
        }

        return newObject;
    }
}
