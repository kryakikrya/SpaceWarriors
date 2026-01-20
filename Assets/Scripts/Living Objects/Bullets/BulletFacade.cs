using UnityEngine;

[RequireComponent (typeof(BulletPhysics))]
public class BulletFacade : LivingFacade
{
    private const float Offset = -90;

    private BulletMovement movement;

    private BulletSettings _settings;

    public void InitializeInfo(BulletSettings settings)
    {
        _settings = settings;
    }

    private void Start()
    {
        movement = new BulletMovement();

        float z = (transform.eulerAngles.z + Offset) * Mathf.Deg2Rad;

        Vector2 angle = new Vector2
        {
            x = Mathf.Cos(z),
            y = Mathf.Sin(z)
        };

        movement.StartMovement(_physics, angle, _settings.Speed);
    }
}
