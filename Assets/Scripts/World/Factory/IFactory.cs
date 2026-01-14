using UnityEngine;

public interface IFactory<T> where T : MonoBehaviour, IPoolableObject<IObjectSettings>
{
    void Create<TSettings>(T poolableObject, TSettings settings) where TSettings : IObjectSettings;
}
