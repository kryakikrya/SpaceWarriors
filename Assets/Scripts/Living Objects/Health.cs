using System;
public abstract class Health
{
    public Action OnObjectDeath;

    private int _health = 1;

    private int _maxHealth = 1;

    public int CurrentHealth => _health;

    public Health (int health)
    {
        _health = health;

        _maxHealth = health;
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
