public class LaserInformationViewModel : ViewModel
{
    private LaserInformationModel _model;

    public ReactiveProperty<int> LaserCharges = new ReactiveProperty<int>();

    public ReactiveProperty<float> LaserCD = new ReactiveProperty<float>();

    public LaserInformationViewModel(LaserInformationModel model) : base(model)
    {
        _model = model;

        Subscribe();
    }

    public override void Subscribe()
    {
        OnModelLaserChargesChanged(_model.CurrentCharges.Value);
        _model.CurrentCharges.OnChanged += OnModelLaserChargesChanged;

        OnModelLaserCDChanged(_model.CurrentCD.Value);
        _model.CurrentCD.OnChanged += OnModelLaserCDChanged;
    }

    private void OnModelLaserChargesChanged(int newValue)
    {
        LaserCharges.Value = newValue;
    }

    private void OnModelLaserCDChanged(float newValue)
    {
        LaserCD.Value = newValue;
    }

    public override void Dispose()
    {
        _model.CurrentCharges.OnChanged -= OnModelLaserChargesChanged;
        _model.CurrentCD.OnChanged -= OnModelLaserCDChanged;
    }
}
