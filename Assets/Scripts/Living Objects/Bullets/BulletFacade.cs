using UnityEngine;
using Zenject;

public class BulletFacade : PoolableObjectFacade, INeedStartMove
{
    private const float Offset = -90;

    private BulletMovement movement;

    private BulletSettings _settings;

    private BulletPresentation _poolMember;

    [Inject]
    private void Construct(Invulnerability invulnerability, PhysicalLayers layers)
    {
        _invulnerability = invulnerability;
        _physicalLayers = layers;
    }

    public void InitializeInfo(BulletSettings settings)
    {
        _health.HealToMax();

        _settings = settings;

        if (movement == null)
        {
            FindComponents();
        }

        StartMove();
    }

    public override void InitializePhysics()
    {
        _physics = new BulletPhysics();
        _physics.Initialize(GetComponent<Rigidbody2D>(), _physicsConfig);
    }

    private void FindComponents()
    {
        movement = new BulletMovement();

        _poolMember = GetComponent<BulletPresentation>();
    }

    public void StartMove()
    {
        float z = (transform.eulerAngles.z + Offset) * Mathf.Deg2Rad;

        Vector2 angle = new Vector2
        {
            x = Mathf.Cos(z),
            y = Mathf.Sin(z)
        };

        movement.StartMovement(_physics, angle, _settings.Speed);
    }

    public override void Death()
    {
        base.Death();

        _poolMember.Death();
    }
}
