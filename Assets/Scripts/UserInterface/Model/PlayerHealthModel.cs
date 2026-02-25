using UnityEngine;
public class PlayerHealthModel : Model
{
    public ReactiveProperty<int> Health { get; } = new ReactiveProperty<int>();

    public ReactiveProperty<int> CurrentHealth { get; private set; } = new ReactiveProperty<int>();

    public PlayerHealthModel(int maxHealth)
    {
        Health.Value = maxHealth;
        CurrentHealth.Value = Health.Value;
    }

    public void ChangeHealth(int health)
    {
        Debug.Log($"ChangeHealth {health}");

        CurrentHealth.Value = Mathf.Clamp(health, 0, Health.Value);
    }
}
