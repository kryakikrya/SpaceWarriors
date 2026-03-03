using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 0.1f;

    [SerializeField] private LivingFacade _facade;

    private float _speed = 1f;

    private LivingObjectPhysics _physics;

    private Vector2 _lastFrameVelocity;

    private void Update()
    {
        if (_physics.CurrentVelocity.magnitude < _minSpeed)
        {
            _physics.AddForce(_lastFrameVelocity, 1);
        }
        else
        {
            _lastFrameVelocity = _physics.CurrentVelocity;
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void StartMovement(Vector2 direciton)
    {
        _physics = _facade.Physics;

        _physics.ZeroVelocity();

        Vector2 startVelocity = direciton.normalized * _speed;

        _physics.AddForce(startVelocity.normalized, startVelocity.magnitude);

        _lastFrameVelocity = startVelocity;
    }
}
