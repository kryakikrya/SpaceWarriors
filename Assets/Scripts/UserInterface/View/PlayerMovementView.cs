using UnityEngine;
using TMPro;

public class PlayerMovementView : View
{
    [SerializeField] private TextMeshProUGUI _coordinates;
    [SerializeField] private TextMeshProUGUI _rotation;
    [SerializeField] private TextMeshProUGUI _velocity;

    private PlayerMovementViewModel _viewModel;

    public override void SetViewModel(ViewModel viewModel)
    {
        _viewModel = viewModel as PlayerMovementViewModel;

        Subscribe();
    }

    public override void Subscribe()
    {
        _viewModel.Coordinates.OnChanged += ChangeCoordinates;

        _viewModel.RotationAngle.OnChanged += ChangeRotation;

        _viewModel.Velocity.OnChanged += ChangeVelocity;
    }

    public override void Dispose()
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
