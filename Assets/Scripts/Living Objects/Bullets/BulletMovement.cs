using UnityEngine;

public class BulletMovement
{
    public void StartMovement(LivingObjectPhysics physics, Vector2 direction, float speed)
    {
        physics.AddForce(direction, speed);
    }
}
