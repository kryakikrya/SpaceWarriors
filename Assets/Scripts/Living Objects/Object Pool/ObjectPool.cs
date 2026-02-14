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
        AvailableObjects.Add(obj);
        UnavailableObjects.Remove(obj);

        Debug.Log($"Теперь в пуле доступных {AvailableObjects.Count} элементов. А в пуле недоступных {UnavailableObjects.Count}");
    }

    public T GetAvailableObject(T poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction)
    {
        PoolableObject objectToReturn;

        if (AvailableObjects.Count == 0)
        {
            objectToReturn = _factory.Create(poolableObject, jsonName, spawnPoint, direction);

            Debug.Log($"Создан новый элемент пула {objectToReturn.name}");
        }
        else
        {
            objectToReturn = AvailableObjects[0];

            AvailableObjects.RemoveAt(0);

            objectToReturn.transform.position = spawnPoint;
            objectToReturn.transform.rotation = Quaternion.Euler(direction);

            Debug.Log($"Изменен существующий доступный элемент пула {objectToReturn.name}");
        }

        UnavailableObjects.Add((T)objectToReturn);

        Debug.Log($"Теперь в пуле доступных {AvailableObjects.Count} элементов. А в пуле недоступных {UnavailableObjects.Count}");

        return (T)objectToReturn;
    }
}
