using UnityEngine;

[RequireComponent (typeof(BulletPhysics))]
public class BulletFacade : LivingFacade
{
    private const float Offset = -90;

    private BulletMovement movement;

    private BulletSettings _settings;

    private PoolableBullet _poolMember;

    public void InitializeInfo(BulletSettings settings)
    {
        _settings = settings;
    }

    private void Start()
    {
        movement = new BulletMovement();

        _poolMember = GetComponent<PoolableBullet>();

        float z = (transform.eulerAngles.z + Offset) * Mathf.Deg2Rad;

        Vector2 angle = new Vector2
        {
            x = Mathf.Cos(z),
            y = Mathf.Sin(z)
        };

        print(_settings.Speed);

        movement.StartMovement(_physics, angle, _settings.Speed);
    }

    private void Awake()
    {
        _health = new BulletHealth();

        _health.InitializeHealth(_maxHealth);
    }

    public override void Death()
    {
        base.Death();

        _poolMember.Death();
    }
}
