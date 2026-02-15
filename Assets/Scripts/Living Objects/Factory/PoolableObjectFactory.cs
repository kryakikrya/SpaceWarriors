using System.IO;
using UnityEngine;
using Zenject;

public class PoolableObjectFactory : IFactory<PoolableObject>
{
    private DiContainer _container;

    public PoolableObjectFactory(DiContainer container)
    {
        _container = container;
    }

    public virtual PoolableObject Create<Type>(PoolableObject poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction) where Type : IObjectSettings
    {
        GameObject obj = _container.InstantiatePrefab(poolableObject.gameObject, spawnPoint, Quaternion.Euler(direction), null);

        var newObject = obj.GetComponent<PoolableObject>();

        newObject.InitializeInfo(GetSettings<Type>(jsonName));

        return newObject;
    }

    public virtual T GetSettings<T>(string jsonName) where T : IObjectSettings
    {
        return JsonUtility.FromJson<T>(File.ReadAllText($"{Application.streamingAssetsPath}/{jsonName}"));
    }
}
