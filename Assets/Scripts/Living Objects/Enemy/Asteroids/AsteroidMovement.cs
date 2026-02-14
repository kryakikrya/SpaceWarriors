using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 0.1f;

    private float _speed = 1f;

    private LivingObjectPhysics _physics;

    private Vector2 _lastFrameVelocity;

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void StartMovement(Vector2 direciton)
    {
        _physics = GetComponent<LivingObjectPhysics>();

        Vector2 startVelocity = direciton.normalized * _speed;

        _physics.AddForce(startVelocity.normalized, startVelocity.magnitude);

        _lastFrameVelocity = startVelocity;
    }

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
}
