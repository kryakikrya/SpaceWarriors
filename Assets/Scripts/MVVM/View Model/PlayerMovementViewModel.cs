using UnityEngine;

public class PlayerMovementViewModel : IViewModel
{
    public ReactiveProperty<Vector2> Coordinates { get; private set; } = new ReactiveProperty<Vector2>();

    public ReactiveProperty<float> RotationAngle { get; private set; } = new ReactiveProperty<float>();

    public ReactiveProperty<Vector2> Velocity { get; private set; } = new ReactiveProperty<Vector2>();

    private PlayerMovementModel _model;

    public PlayerMovementViewModel(PlayerMovementModel model)
    {
        _model = model;

        Subscribe();
    }

    public void Subscribe()
    {
        OnModelCoordinatesChanged(_model.Coordinates.Value);
        _model.Coordinates.OnChanged += OnModelCoordinatesChanged;

        OnModelRotationAngleChanged(_model.RotationAngle.Value);
        _model.RotationAngle.OnChanged += OnModelRotationAngleChanged;

        OnModelVelocityChanged(_model.Velocity.Value);
        _model.Velocity.OnChanged += OnModelVelocityChanged;
    }

    public void Dispose()
    {
        _model.Coordinates.OnChanged -= OnModelCoordinatesChanged;
        _model.RotationAngle.OnChanged -= OnModelRotationAngleChanged;
        _model.Velocity.OnChanged -= OnModelVelocityChanged;
    }

    private void OnModelCoordinatesChanged(Vector2 newValue)
    {
        Coordinates.Value = newValue;
    }

    private void OnModelRotationAngleChanged(float newValue)
    {
        RotationAngle.Value = newValue;
    }

    private void OnModelVelocityChanged(Vector2 newValue)
    {
        Velocity.Value = newValue;
    }
}
