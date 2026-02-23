using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T> where T : PoolableObject
{
    protected PoolableObjectFactory _factory;

    public List<T> AvailableObjects { get; private set; } = new List<T>();

    public List<T> UnavailableObjects { get; private set; } = new List<T>();

    public ObjectPool (PoolableObjectFactory factory)
    {
                _factory = factory;
    }

    public void MakeObjectUnavailable(T obj)
    {
        AvailableObjects.Add(obj);
        UnavailableObjects.Remove(obj);
    }

    public T GetAvailableObject<Settings>(T poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction) where Settings : IObjectSettings
    {
        PoolableObject objectToReturn;

        if (AvailableObjects.Count == 0)
        {
            objectToReturn = _factory.Create<Settings>(poolableObject, jsonName, spawnPoint, direction);
        }
        else
        {
            objectToReturn = AvailableObjects[0];

            AvailableObjects.RemoveAt(0);

            objectToReturn.transform.position = spawnPoint;
            objectToReturn.transform.rotation = Quaternion.Euler(direction);

            objectToReturn.gameObject.SetActive(true);

            objectToReturn.InitializeInfo(_factory.GetSettings<Settings>(jsonName));
        }

        UnavailableObjects.Add((T)objectToReturn);

        return (T)objectToReturn;
    }
}
