using UnityEngine;

public class BulletMovement
{
    public void StartMovement(LivingObjectPhysics physics, Vector2 direction, float speed)
    {
        Debug.Log($"Force was added, {direction.x}, {direction.y}, {speed}");
        physics.AddForce(direction, speed);
    }
}
