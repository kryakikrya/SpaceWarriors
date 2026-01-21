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

    public virtual PoolableObject Create(PoolableObject poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction)
    {
        GameObject obj = _container.InstantiatePrefab(poolableObject.gameObject, spawnPoint, Quaternion.Euler(direction), null);

        var newObject = obj.GetComponent<PoolableObject>();

        newObject.InitializeInfo(GetSettings(jsonName));

        return newObject;
    }

    public virtual IObjectSettings GetSettings(string jsonName)
    {
        return JsonUtility.FromJson<BulletSettings>(File.ReadAllText($"{Application.streamingAssetsPath}/{jsonName}"));
    }
}
