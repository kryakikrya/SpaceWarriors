using UnityEngine;
public abstract class PoolableObject : MonoBehaviour, IPoolableObject<IObjectSettings>
{
    [SerializeField] private LivingFacade _facade;

    [SerializeField] private PoolableObjectType _type;

    protected Health _health;

    public PoolableObjectType Type => _type;

    private void OnEnable()
    {
        _health = _facade.Health;

        _health.OnObjectDeath += Death;
    }

    private void OnDisable()
    {
        _health.OnObjectDeath -= Death;
    }

    public abstract void InitializeInfo(IObjectSettings settings);

    public abstract void Death();
}
