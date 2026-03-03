using Zenject;

public class PlayerHealth : Health
{
    private SignalBus _signalBus;

    private float _invulnerabilityTime;

    private PlayerHealthModel _healthModel;

    public PlayerHealthModel Model => _healthModel;

    public PlayerHealth(PlayerParametersSettings settings, SignalBus signalBus) : base(settings)
    {
        _health = settings.Health;
        _maxHealth = settings.Health;

        _invulnerabilityTime = settings.InvulnerabilityTime;
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
