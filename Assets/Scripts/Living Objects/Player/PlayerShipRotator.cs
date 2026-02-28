using UnityEngine;
using Zenject;
public class PlayerShipRotator : Rotator
{
    [SerializeField] private Camera _camera;

    [Inject] private VirtualJoystick _joystick;

    public void SetRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
    }

    public override Vector3 CheckPosition()
    {
#if UNITY_ANDROID || UNITY_IOS
    Vector2 dir = _joystick.Direction;

    return (Vector2)transform.position + dir;
#else
        return _camera.ScreenToWorldPoint(Input.mousePosition);
#endif
    }
}
