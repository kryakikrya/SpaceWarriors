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

    public virtual void Create(PoolableObject poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction)
    {
        GameObject obj = _container.InstantiatePrefab(poolableObject.gameObject, spawnPoint, Quaternion.Euler(direction), null);

        obj.GetComponent<PoolableObject>().InitializeInfo(GetSettings(jsonName));
    }

    public virtual IObjectSettings GetSettings(string jsonName)
    {
        return JsonUtility.FromJson<BulletSettings>(File.ReadAllText($"{Application.streamingAssetsPath}/{jsonName}"));
    }
}
