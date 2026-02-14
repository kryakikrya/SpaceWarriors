using System.Collections;
using UnityEngine;
using Zenject;

public class LivingFacade : MonoBehaviour
{
    private const int CheckBoundsTime = 5;

    [SerializeField] private float Offset = 0.1f;

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
    }

    private void Awake()
    {
        _physics = GetComponent<LivingObjectPhysics>();

        _damageApplier = new DamageApplier(1);

        DisableInvulnerability();

        StartCoroutine(CheckBoundsWracks());
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

    private IEnumerator CheckBoundsWracks()
    {
        while (true)
        {
            yield return new WaitForSeconds(CheckBoundsTime);

            if (Physics2D.OverlapCircle(transform.position, (transform.localScale.x * 2) + Offset, _physicalLayers.WrappingLayer) == false)
            {
                Death();
            }
        }
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
