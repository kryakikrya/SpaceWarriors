using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool<T> where T : IPoolableObject<IObjectSettings>
{
    List<T> AvailableObjects { get; }

    List<T> UnavailableObjects { get; }

    void InitializeFactory(PoolableObjectFactory factory);

    void MakeObjectUnavailable(T obj);

    T GetAvailableObject<Settings>(T poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction) where Settings : IObjectSettings;
}
