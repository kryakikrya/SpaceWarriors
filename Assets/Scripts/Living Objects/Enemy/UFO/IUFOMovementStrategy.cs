using UnityEngine;

public interface IUFOMovementStrategy
{
    public void Move(GameObject player, LivingObjectPhysics physics, float speed);
}
