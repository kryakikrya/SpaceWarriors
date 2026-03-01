using UnityEngine;
public abstract class PoolableObject : MonoBehaviour
{
    [SerializeField] private PoolableObjectFacade _facade;

    [SerializeField] private PoolableObjectType _type;

    protected PoolableObjectHealth _health;

    public PoolableObjectType Type => _type;

    private void OnDestroy()
    {
        _health.OnObjectDeath -= Death;
    }

    public virtual void InitializeInfo(IObjectSettings settings)
    {
        _health = new PoolableObjectHealth(settings);

        _facade.InitializeHealth(_health);

        _health.OnObjectDeath += Death;
    }

    public abstract void Death();
}
