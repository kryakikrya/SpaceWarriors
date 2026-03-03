using UnityEngine;

public class BulletPhysics : LivingObjectPhysics
{
    private Vector2 _permanentVelocity;

    public override void AddForce(Vector2 direction, float speed)
    {
        _permanentVelocity = direction * speed;
    }

    public override void Perform()
    {
        _velocity = _permanentVelocity;
        Move();
    }
}