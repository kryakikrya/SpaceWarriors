using UnityEngine;

[RequireComponent (typeof(BulletPhysics))]
public class BulletFacade : LivingFacade, INeedStartMove
{
    private const float Offset = -90;

    private BulletMovement movement;

    private BulletSettings _settings;

    private PoolableBullet _poolMember;

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

    private void FindComponents()
    {
        movement = new BulletMovement();

        _poolMember = GetComponent<PoolableBullet>();
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

    private void Awake()
    {
        _health = new PoolableObjectHealth(_maxHealth);
    }

    public override void Death()
    {
        base.Death();

        _poolMember.Death();
    }
}
