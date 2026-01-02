using System.Collections.Generic;

public interface IObjectPool<T> where T : IPoolableObject
{
    List<T> AvailableObjects { get; }

    List<T> UnavailableObjects { get; }

    void MakeObjectUnavailable(T obj);

    T GetAvailableObject();
}
