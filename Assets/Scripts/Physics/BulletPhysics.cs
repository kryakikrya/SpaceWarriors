using UnityEngine;

public class BulletPhysics : LivingObjectPhysics
{
    public override void Hit(RaycastHit2D hit)
    {
        if (hit.rigidbody.gameObject.TryGetComponent(out LivingObjectPhysics physics))
        {
            //take damage
        }
    }
}

