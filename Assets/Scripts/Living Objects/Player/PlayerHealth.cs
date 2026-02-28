using Zenject;

public class PlayerHealth : Health
{
    private SignalBus _signalBus;

    private const int MenuSceneID = 0;

    private Invulnerability _invulnerability;

    private float _invulnerabilityTime;

    private PlayerHealthModel _healthModel;

    public PlayerHealthModel Model => _healthModel;

    public PlayerHealth(int health, Invulnerability invulnerability, float invulnerabilityTime, SignalBus signalBus) : base(health)
    {
        _invulnerability = invulnerability;
        _invulnerabilityTime = invulnerabilityTime;
        _signalBus = signalBus;

        _healthModel = new PlayerHealthModel(_maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        _healthModel.ChangeHealth(_health);

        _signalBus.Fire(new PlayerDamagedSignal { InvulnerabilityTime = _invulnerabilityTime });
    }

    public override void Death()
    {
        OnObjectDeath?.Invoke();
    }
}
