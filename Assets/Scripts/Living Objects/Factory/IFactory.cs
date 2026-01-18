using UnityEngine;

public interface IFactory<T> where T : MonoBehaviour, IPoolableObject<IObjectSettings>
{
    void Create(T poolableObject, string jsonName, Vector3 spawnPoint, Vector3 direction);

    IObjectSettings GetSettings(string jsonName);
}
