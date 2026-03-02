using UnityEngine;

public class UFOStrategyDefault : IUFOMovementStrategy
{
    public void Move(GameObject player, LivingFacade facade, float speed)
    {
        Vector3 direction = player.transform.position - facade.gameObject.transform.position;

        facade.Physics.AddForce(direction, speed);
    }
}
