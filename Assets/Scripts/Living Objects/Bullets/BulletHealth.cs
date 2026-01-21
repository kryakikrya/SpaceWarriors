using UnityEngine;
public class BulletHealth : Health
{
    public override void Death()
    {
        Debug.Log("Убит!");
    }
}
