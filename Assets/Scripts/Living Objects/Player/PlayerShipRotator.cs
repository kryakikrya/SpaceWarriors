using UnityEngine;
using Zenject;
public class PlayerShipRotator : Rotator
{
    [SerializeField] private Camera _camera;

#if UNITY_ANDROID || UNITY_IOS
    [Inject] private VirtualJoystick _joystick;

    Vector2 _lastDir = Vector2.zero;
#endif

    public void SetRotationSpeed(float speed)
    {
        _rotationSpeed = speed;
    }

    public override Vector3 CheckPosition()
    {
#if UNITY_ANDROID || UNITY_IOS
        Vector2 dir = _joystick.Direction;

        if (dir.magnitude < 0.01)
        {
            dir = _lastDir;
        }

        _lastDir = dir;

        return (Vector2)transform.position + dir;
#else
        return _camera.ScreenToWorldPoint(Input.mousePosition);
#endif
    }
}
