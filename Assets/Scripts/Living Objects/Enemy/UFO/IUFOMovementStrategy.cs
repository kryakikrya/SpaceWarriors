using UnityEngine;

public interface IUFOMovementStrategy
{
    public void Move(GameObject player, LivingFacade facade, float speed);
}
