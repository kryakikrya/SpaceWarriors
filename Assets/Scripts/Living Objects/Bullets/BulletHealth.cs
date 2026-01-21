public class BulletHealth : Health
{
    public BulletHealth(int health) : base(health)
    {
    }

    public override void Death()
    {
        // back to pool
    }
}
