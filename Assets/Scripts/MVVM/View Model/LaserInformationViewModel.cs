public class LaserInformationViewModel : IViewModel
{
    public ReactiveProperty<int> LaserCharges = new ReactiveProperty<int>();

    public ReactiveProperty<float> LaserCD = new ReactiveProperty<float>();

    private LaserInformationModel _model;

    public LaserInformationViewModel(LaserInformationModel model)
    {
        _model = model;

        Subscribe();
    }

    public void Subscribe()
    {
        OnModelLaserChargesChanged(_model.CurrentCharges.Value);
        _model.CurrentCharges.OnChanged += OnModelLaserChargesChanged;

        OnModelLaserCDChanged(_model.CurrentCD.Value);
        _model.CurrentCD.OnChanged += OnModelLaserCDChanged;
    }

    public void Dispose()
    {
        _model.CurrentCharges.OnChanged -= OnModelLaserChargesChanged;
        _model.CurrentCD.OnChanged -= OnModelLaserCDChanged;
    }

    private void OnModelLaserChargesChanged(int newValue)
    {
        LaserCharges.Value = newValue;
    }

    private void OnModelLaserCDChanged(float newValue)
    {
        LaserCD.Value = newValue;
    }
}
