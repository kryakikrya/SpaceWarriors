using System.Collections.Generic;
using Zenject;

public class ObjectPool<T> where T : PoolableObject
{
    private ObjectSettingsProvider _provider;

    public List<T> Objects { get; private set; } = new List<T>();

    [Inject]
    private void Construct(ObjectSettingsProvider provider)
    {
        _provider = provider;
    }

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
