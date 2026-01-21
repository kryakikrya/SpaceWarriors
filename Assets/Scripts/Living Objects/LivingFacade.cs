using UnityEngine;
using Zenject;

public class LivingFacade<T> : MonoBehaviour where T : Health
{
    [SerializeField] private float Offset = 0.1f;

    [SerializeField] protected int _maxHealth = 3;

    protected Invulnerability _invulnerability;

    protected LivingObjectPhysics _physics;

    protected PhysicalLayers _physicalLayers;

    protected T _health;

    protected DamageApplier _damageApplier;

    public T Health => _health;

    [Inject]
    private void Construct(Invulnerability invulnerability, PhysicalLayers layers)
    {
        _invulnerability = invulnerability;
        _physicalLayers = layers;
    }

    private void Awake()
    {
        _physics = GetComponent<LivingObjectPhysics>();

        _damageApplier = new DamageApplier(1);

        DisableInvulnerability();
    }

    private void OnEnable()
    {
        _physics.Colliding += DamageEnemy;
    }

    private void OnDisable()
    {
        _physics.Colliding -= DamageEnemy;
    }

    private void DamageEnemy(LivingObjectPhysics physics)
    {
        _damageApplier.ApplyDamage(physics.gameObject.GetComponent<LivingFacade<T>>().Health);
    }

    public void CheckBoundsWracks()
    {
        if (Physics2D.OverlapCircle(transform.position, transform.localScale.x + Offset, _physicalLayers.WrappingLayer) == false)
        {
            Death();
        }
    }

    private void FixedUpdate()
    {
        CheckBoundsWracks();
    }

    public void EnableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.EnableInvulnerability(gameObject, _physicalLayers.InvulnerabilityLayer));
    }

    public void DisableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.DisableInvulnerability(gameObject, _physicalLayers.DefaultLayer, _physicalLayers.InvulnerabilityLayer));
    }

    public virtual void Death()
    {
        Debug.Log($"{name} was killed");
    }
}
