public class DamageApplier
{
    private int _damage;

    public DamageApplier(int damage)
    {
        _damage = damage;
    }

    public void ApplyDamage(Health health)
    {
        health.TakeDamage(_damage);
    }
}
