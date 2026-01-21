using UnityEngine;
public abstract class Health
{
    private int _health = 1;

    public void InitializeHealth(int health)
    {
        _health = health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if ( _health <= 0)
        {
            Death();
        }

        Debug.Log(_health);
    }

    public abstract void Death();
}
