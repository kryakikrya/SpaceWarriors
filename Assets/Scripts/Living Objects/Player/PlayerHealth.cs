using Zenject;

public class PlayerHealth : Health
{
    private SignalBus _signalBus;

    private Invulnerability _invulnerability;

    private float _invulnerabilityTime;

    private PlayerHealthModel _healthModel;

    public PlayerHealthModel Model => _healthModel;

    public PlayerHealth(PlayerParametersSettings settings, Invulnerability invulnerability, SignalBus signalBus) : base(settings)
    {
        _invulnerability = invulnerability;

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
