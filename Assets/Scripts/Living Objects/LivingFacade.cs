using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LivingFacade : MonoBehaviour
{
    [SerializeField] private List<string> _layersToIgnore = new List<string>();

    [SerializeField] private string _myLayer;

    [SerializeField] protected PhysicsSO _physicsConfig;

    protected Invulnerability _invulnerability;

    protected LivingObjectPhysics _physics;

    protected PhysicalLayers _physicalLayers;

    protected Health _health;

    protected DamageApplier _damageApplier;

    protected PhysicsResolver _resolver;

    public LivingObjectPhysics Physics => _physics;

    public Health Health => _health;

    [Inject]
    private void Construct(Invulnerability invulnerability, PhysicalLayers layers, PhysicsResolver resolver)
    {
        _invulnerability = invulnerability;
        _physicalLayers = layers;

        _resolver = resolver;
    }

    private void Awake()
    {
        if (_physics == null)
        {
            InitializePhysics();

            _resolver.AddPhysics(Physics);

            _damageApplier = new DamageApplier(1);

            DisableInvulnerability();
        }
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

    private void OnEnable()
    {
        Enable();
    }

    public virtual void InitializePhysics()
    {
        _physics = new LivingObjectPhysics();
        _physics.Initialize(GetComponent<Rigidbody2D>(), _physicsConfig);
    }

    protected virtual void Enable()
    {
        if (_physics == null)
        {
            InitializePhysics();

            _resolver.AddPhysics(Physics);

            _damageApplier = new DamageApplier(1);

            DisableInvulnerability();
        }

        _physics.Colliding += DamageEnemy;
    }

    protected virtual void Disable()
    {
        _physics.Colliding -= DamageEnemy;
    }

    private void OnDisable()
    {
        Disable();
    }

    private void DamageEnemy(RaycastHit2D hit)
    {
        _damageApplier.ApplyDamage(hit.rigidbody.gameObject.GetComponent<LivingFacade>().Health);

        Physics.Bounce(hit, hit.rigidbody.GetComponent<LivingFacade>().Physics);

        _damageApplier.ApplyDamage(Health);
    }
}
