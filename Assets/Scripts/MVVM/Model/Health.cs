using System;

public abstract class Health
{
    public Action OnObjectDeath;

    protected int _health = 1;

    protected int _maxHealth = 1;

    public int CurrentHealth => _health;

    public Health(IObjectSettings settings)
    {
        _health = settings.Health;

        _maxHealth = settings.Health;
    }

    public void HealToMax()
    {
        _health = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;

        if ( _health <= 0)
        {
            Death();
        }
    }

    public abstract void Death();
}
