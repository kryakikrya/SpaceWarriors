using UnityEngine;

public class PlayerMovementModel : Model
{
    public ReactiveProperty<Vector2> Coordinates { get; private set; } = new ReactiveProperty<Vector2>();

    public ReactiveProperty<float> RotationAngle { get; private set; } = new ReactiveProperty<float>();

    public ReactiveProperty<Vector2> Velocity { get; private set; } = new ReactiveProperty<Vector2>();

    public void ChangeCoordinates(Vector2 newCoordinates)
    {
        Coordinates.Value = newCoordinates;
    }

    public void ChangeRotationAngle(float newAngle)
    {
        RotationAngle.Value = newAngle;
    }

    public void ChangeVelocity(Vector2 newVelocity)
    {
        Velocity.Value = newVelocity;
    }
}
