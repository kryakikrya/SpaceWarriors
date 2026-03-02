using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LivingFacade : MonoBehaviour
{
    [SerializeField] private List<string> _layersToIgnore = new List<string>();

    [SerializeField] private string _myLayer;

    protected Invulnerability _invulnerability;

    protected LivingObjectPhysics _physics;

    protected PhysicalLayers _physicalLayers;

    protected Health _health;

    protected DamageApplier _damageApplier;

    public LivingObjectPhysics Physics => _physics;

    public Health Health => _health;

    [Inject]
    private void Construct(Invulnerability invulnerability, PhysicalLayers layers)
    {
        _invulnerability = invulnerability;
        _physicalLayers = layers;
    }

    private void Awake()
    {
        _damageApplier = new DamageApplier(1);

        DisableInvulnerability();
    }

    private void OnEnable()
    {
        Enable();
    }

    protected virtual void Enable()
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
        Disable();
    }

    protected virtual void Disable()
    {
        _physics.Colliding -= DamageEnemy;
    }

    private void DamageEnemy(RaycastHit2D hit)
    {
        _damageApplier.ApplyDamage(hit.rigidbody.gameObject.GetComponent<LivingFacade>().Health);

        Physics.Bounce(hit, hit.rigidbody.GetComponent<LivingFacade>().Physics);

        _damageApplier.ApplyDamage(Health);
    }

    public void EnableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.EnableInvulnerability(gameObject, _physicalLayers.InvulnerabilityLayer));
    }

    public void DisableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.DisableInvulnerability(gameObject, _myLayer, _layersToIgnore));
    }

    public virtual void Death()
    {
        Debug.Log($"{name} was killed!");
    }
}
