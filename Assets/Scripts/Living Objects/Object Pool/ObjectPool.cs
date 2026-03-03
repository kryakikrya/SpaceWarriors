using System.Collections.Generic;

public class ObjectPool<T> where T : PoolableObject
{
    public List<T> Objects { get; private set; } = new List<T>();

    public bool Get(out PoolableObject objectToReturn)
    {
        objectToReturn = UpdateLists();

        if (objectToReturn != null)
        {
            objectToReturn.gameObject.SetActive(true);

            return true;
        }

        return false;
    }

    public void AddObject(T obj)
    {
        Objects.Add(obj);
    }

    private PoolableObject UpdateLists()
    {
        foreach (var obj in Objects)
        {
            if (obj.gameObject.activeSelf == false)
            {
                return obj;
            }
        }

        return null;
    }
}
