using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T> where T : PoolableObject
{
    protected PoolableObjectFactory _factory;

    public List<T> AvailableObjects { get; private set; } = new List<T>();

    public List<T> UnavailableObjects { get; private set; } = new List<T>();

    public void InitializeFactory(PoolableObjectFactory factory)
    {
        _factory = factory;
    }

    public void MakeObjectUnavailable(T obj)
    {
        AvailableObjects.Remove(obj);
        UnavailableObjects.Add(obj);
    }

    public T GetAvailableObject(T poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction)
    {
        PoolableObject objectToReturn;

        if (AvailableObjects.Count == 0)
        {
            objectToReturn = _factory.Create(poolableObject, jsonName, spawnPoint, direction);

            Debug.Log($"Создан новый элемент пула {objectToReturn.name}. Теперь в пуле доступных {AvailableObjects.Count} элементов. А в пуле недоступных {UnavailableObjects.Count}");
        }
        else
        {
            objectToReturn = AvailableObjects[0];

            AvailableObjects.RemoveAt(0);

            Debug.Log($"Изменен существующий доступный элемент пула {objectToReturn.name}. Теперь в пуле доступных {AvailableObjects.Count} элементов. А в пуле недоступных {UnavailableObjects.Count}");
        }

        UnavailableObjects.Add((T)objectToReturn);

        return (T)objectToReturn;
    }
}
