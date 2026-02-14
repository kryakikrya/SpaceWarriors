using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _minSpeed = 0.1f;

    private LivingObjectPhysics _physics;

    private Vector2 _lastFrameVelocity;

    private void Awake()
    {
        _physics = GetComponent<LivingObjectPhysics>();

        Vector2 startVelocity = new Vector2(Random.Range(-_speed, _speed), Random.Range(-_speed, _speed));

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
