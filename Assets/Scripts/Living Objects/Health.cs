using System;
using UnityEngine;
public abstract class Health
{
    public Action OnObjectDeath;

    private int _health = 1;

    public void InitializeHealth(int health)
    {
        _health = health;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Take Damage {damage}");

        _health -= damage;

        if ( _health <= 0)
        {
            Debug.Log("Death");
            Death();
        }
    }

    public abstract void Death();
}
