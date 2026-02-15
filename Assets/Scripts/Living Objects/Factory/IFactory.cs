using UnityEngine;

public interface IFactory<T> where T : PoolableObject
{
    PoolableObject Create<Type>(T poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction) where Type : IObjectSettings;

    Type GetSettings<Type>(string jsonName) where Type : IObjectSettings; 
}
