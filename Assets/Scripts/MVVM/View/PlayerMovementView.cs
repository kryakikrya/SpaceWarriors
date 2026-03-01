using UnityEngine;
using TMPro;

public class PlayerMovementView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _coordinates;
    [SerializeField] private TextMeshProUGUI _rotation;
    [SerializeField] private TextMeshProUGUI _velocity;

    private PlayerMovementViewModel _viewModel;

    public void SetViewModel(IViewModel viewModel)
    {
        _viewModel = viewModel as PlayerMovementViewModel;

        Subscribe();
    }

    public void Subscribe()
    {
        _viewModel.Coordinates.OnChanged += ChangeCoordinates;

        _viewModel.RotationAngle.OnChanged += ChangeRotation;

        _viewModel.Velocity.OnChanged += ChangeVelocity;
    }

    public void Dispose()
    {
        _viewModel.Coordinates.OnChanged -= ChangeCoordinates;

        _viewModel.RotationAngle.OnChanged -= ChangeRotation;

        _viewModel.Velocity.OnChanged -= ChangeVelocity;
    }

    private void ChangeCoordinates(Vector2 value)
    {
        _coordinates.text = value.ToString();
    }

    private void ChangeRotation(float value)
    {
        _rotation.text = value.ToString();
    }

    private void ChangeVelocity(Vector2 value)
    {
        _velocity.text = value.ToString();
    }
}
