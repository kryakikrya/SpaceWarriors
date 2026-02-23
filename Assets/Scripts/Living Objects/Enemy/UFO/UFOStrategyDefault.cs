using UnityEngine;

public class UFOStrategyDefault : IUFOMovementStrategy
{
    public void Move(GameObject player, LivingObjectPhysics physics, float speed)
    {
        Vector3 direction = player.transform.position - physics.gameObject.transform.position;

        physics.AddForce(direction, speed);
    }
}
