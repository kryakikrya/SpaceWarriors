using UnityEngine;

public class PlayerHealthViewModel : ViewModel
{
    private PlayerHealthModel _model; 

    public ReactiveProperty<int> MaxHealth = new ReactiveProperty<int>();
    public ReactiveProperty<int> CurrentHealth = new ReactiveProperty<int>();

    public ReactiveProperty<int> HeartsCount = new ReactiveProperty<int>();

    public PlayerHealthViewModel(PlayerHealthModel model) : base(model)
    {
        _model = model;

        Subscribe();
    }

    public override void Subscribe()
    {
        OnModelMaxHealthChanged(_model.Health.Value);
        _model.Health.OnChanged += OnModelMaxHealthChanged;

        OnModelCurrentHealthChanged(_model.CurrentHealth.Value);
        _model.CurrentHealth.OnChanged += OnModelCurrentHealthChanged;
    }

    private void OnModelMaxHealthChanged(int newValue)
    {
        MaxHealth.Value = newValue;

        HeartsCount.Value = newValue;
    }

    private void OnModelCurrentHealthChanged(int newValue)
    {
        CurrentHealth.Value = newValue;
    }

    public override void Dispose()
    {
        _model.Health.OnChanged -= OnModelMaxHealthChanged;
        _model.CurrentHealth.OnChanged -= OnModelCurrentHealthChanged;
    }
}
