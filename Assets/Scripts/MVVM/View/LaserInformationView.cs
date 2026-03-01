using TMPro;
using UnityEngine;

public class LaserInformationView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _charges;
    [SerializeField] private TextMeshProUGUI _cd;

    private LaserInformationViewModel _viewModel;

    public void SetViewModel(LaserInformationViewModel viewModel)
    {
        _viewModel = viewModel;

        Subscribe();
    }

    public void Subscribe()
    {
        _viewModel.LaserCharges.OnChanged += ChangeCharges;

        _viewModel.LaserCD.OnChanged += ChangeCD;
    }

    public void Dispose()
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
