public class PoolableFacade<T> : LivingFacade<T> where T : Health
{
    protected PoolableObject _poolableObject;

    public override void Death()
    {
        
    }
}
