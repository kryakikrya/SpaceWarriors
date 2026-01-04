using UnityEngine;

public class PlayerShipRotator : Rotator
{
    [SerializeField] private Camera _camera;

    public override Vector3 CheckPosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
