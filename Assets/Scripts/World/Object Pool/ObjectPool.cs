using System.Collections.Generic;

public class ObjectPool<T> : IObjectPool<T> where T : IPoolableObject<IObjectSettings>
{
    public List<T> AvailableObjects { get; private set; }

    public List<T> UnavailableObjects { get; private set; }

    public void MakeObjectUnavailable(T obj)
    {
        AvailableObjects.Remove(obj);
        UnavailableObjects.Add(obj);
    }

    public T GetAvailableObject()
    {
        T objectToReturn = AvailableObjects[0];

        AvailableObjects.RemoveAt(0);

        UnavailableObjects.Add(objectToReturn);

        return objectToReturn;
    }
}
