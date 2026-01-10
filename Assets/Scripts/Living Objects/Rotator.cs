using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Rotator : MonoBehaviour
{
    [SerializeField] protected float _offset;
    [SerializeField] protected float _rotationSpeed;

    protected Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rotate(CheckPosition());    
    }

    public abstract Vector3 CheckPosition();

    public virtual void Rotate(Vector3 target)
    {
        target.z = 0f;

        Vector2 direction = (target - transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _offset;

        float smoothAngle = Mathf.MoveTowardsAngle(_rb.rotation, angle, _rotationSpeed * Time.fixedDeltaTime);

        _rb.MoveRotation(smoothAngle);
    }
}
