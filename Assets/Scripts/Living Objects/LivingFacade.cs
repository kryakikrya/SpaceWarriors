using UnityEngine;
using Zenject;

public class LivingFacade : MonoBehaviour
{
    [SerializeField] protected int _maxHealth = 3;

    protected Invulnerability _invulnerability;

    protected LivingObjectPhysics _physics;

    protected PhysicalLayers _physicalLayers;

    protected Health _health;

    protected DamageApplier _damageApplier;

    public Health Health => _health;

    [Inject]
    private void Construct(Invulnerability invulnerability, PhysicalLayers layers)
    {
        _invulnerability = invulnerability;
        _physicalLayers = layers;

        _health = new PoolableObjectHealth(_maxHealth);
    }

    private void Awake()
    {
        _physics = GetComponent<LivingObjectPhysics>();

        _damageApplier = new DamageApplier(1);

        DisableInvulnerability();
    }

    private void OnEnable()
    {
        if (_physics == null)
        {
            _physics = GetComponent<LivingObjectPhysics>();

            _damageApplier = new DamageApplier(1);

            DisableInvulnerability();
        }

        _physics.Colliding += DamageEnemy;
    }

    private void OnDisable()
    {
        _physics.Colliding -= DamageEnemy;
    }

    private void DamageEnemy(LivingObjectPhysics physics)
    {
        _damageApplier.ApplyDamage(physics.gameObject.GetComponent<LivingFacade>().Health);

        _damageApplier.ApplyDamage(Health);
    }

    public void EnableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.EnableInvulnerability(gameObject, _physicalLayers.InvulnerabilityLayer));
    }

    public virtual void DisableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.DisableInvulnerability(gameObject, _physicalLayers.DefaultLayer, _physicalLayers.InvulnerabilityLayer));
    }

    public virtual void Death()
    {
        Debug.Log($"{name} was killed");
    }
}
