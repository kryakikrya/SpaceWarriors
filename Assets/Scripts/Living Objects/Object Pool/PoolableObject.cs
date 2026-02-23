using UnityEngine;
public abstract class PoolableObject : MonoBehaviour, IPoolableObject<IObjectSettings>
{
    [SerializeField] private LivingFacade _facade;

    protected Health _health;

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
