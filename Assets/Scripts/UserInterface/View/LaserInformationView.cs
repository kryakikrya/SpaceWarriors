using TMPro;
using UnityEngine;

public class LaserInformationView : View
{
    [SerializeField] private TextMeshProUGUI _charges;
    [SerializeField] private TextMeshProUGUI _cd;

    private LaserInformationViewModel _viewModel;

    public override void SetViewModel(ViewModel viewModel)
    {
        _viewModel = viewModel as LaserInformationViewModel;

        Subscribe();
    }

    public override void Subscribe()
    {
        _viewModel.LaserCharges.OnChanged += ChangeCharges;

        _viewModel.LaserCD.OnChanged += ChangeCD;
    }

    public override void Dispose()
    {
        _viewModel.LaserCharges.OnChanged -= ChangeCharges;

        _viewModel.LaserCD.OnChanged -= ChangeCD;
    }

    private void ChangeCharges(int value)
    {
        _charges.text = value.ToString();
    }

    private void ChangeCD(float value)
    {
        _cd.text = value.ToString();
    }
}
