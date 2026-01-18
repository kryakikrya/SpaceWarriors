using UnityEngine;

public class BulletFacade : LivingFacade, IPoolableObject<BulletSettings>
{
    private BulletMovement movement;

    private void Start()
    {
        movement = new BulletMovement();
    }

    public void InitializeInfo(BulletSettings settings)
    {
        movement.StartMovement(_physics, transform.rotation.eulerAngles, settings.Speed);
    }
}
