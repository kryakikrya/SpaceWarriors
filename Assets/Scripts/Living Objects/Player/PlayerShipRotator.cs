using UnityEngine;
public class PlayerShipRotator : Rotator
{
    [SerializeField] private Camera _camera;

    public void SetRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
    }

    public override Vector3 CheckPosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
