using UnityEngine;

public class BulletMovement
{
    public void StartMovement(LivingObjectPhysics physics, Vector2 direction, float speed)
    {
        physics.ZeroVelocity();
        physics.AddForce(direction, speed);
    }
}
