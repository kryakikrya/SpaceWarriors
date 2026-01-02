using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent (typeof(Rigidbody2D))]
public class LivingObjectPhysics : MonoBehaviour
{
    private const int MaxChangesInOneMovement = 3;

    [SerializeField] private ContactFilter2D _collisionFilter = new ContactFilter2D().NoFilter();

    [SerializeField] private float _minMovement = 0.03f;

    [SerializeField] private float _hitOffset = 0.03f;

    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _inertionModifier = 5f;

    private Vector2 _frameMovementVector = Vector2.zero;
    private Vector2 _velocity = Vector2.zero;

    private Rigidbody2D _rb;
    private List<RaycastHit2D> _hits = null;

    #region Initialization
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;

        _hits = ListPool<RaycastHit2D>.Get();

        _rb.useFullKinematicContacts = true;
    }

    private void OnDestroy()
    {
        if ( _hits != null )
        {
            ListPool<RaycastHit2D>.Release(_hits);
            _hits = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) => CheckOverlap(collision);

    private void OnCollisionStay2D(Collision2D collision) => CheckOverlap(collision);

    #endregion

    #region Movement Logic

    public void AddForce(Vector2 direction, float speed)
    {
        _frameMovementVector = new Vector2(direction.x * speed / _mass, direction.y * speed / _mass);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        _velocity += new Vector2(_frameMovementVector.x * delta * _maxSpeed, _frameMovementVector.y * delta * _maxSpeed);

        _velocity *= Mathf.Exp(-_inertionModifier * delta);

        Move(_velocity);
    }

    private void CheckOverlap(Collision2D collision)
    {
        if (_collisionFilter.IsFilteringLayerMask(collision.collider.gameObject))
        {
            return;
        }

        var colliderDistance = Physics2D.Distance(collision.otherCollider, collision.collider);

        if (colliderDistance.isOverlapped)
        {
            Debug.Log(1);

            collision.otherRigidbody.position += colliderDistance.normal * colliderDistance.distance;
        }
    }

    private void Move(Vector2 movement)
    {
        if (movement.sqrMagnitude <= Mathf.Epsilon)
        {
            return;
        }

        var vectorDirection = movement.normalized;
        var vectorLength = movement.magnitude;

        CompleteTheDistance(MaxChangesInOneMovement, vectorLength, vectorDirection);
    }

    private void CompleteTheDistance(int iterationCount, float distance, Vector2 direction)
    {
        var position = _rb.position;

        while (iterationCount > 1 && distance > _minMovement && direction.sqrMagnitude > Mathf.Epsilon) 
        {
            iterationCount--;

            var currentDistance = distance;

            var hitCount = _rb.Cast(direction, _collisionFilter, _hits, distance);

            if (hitCount > 0)
            {
                var hit = _hits[0];

                if (hit.distance > _hitOffset)
                {
                    currentDistance = hit.distance - _hitOffset;

                    position += direction * currentDistance;
                }
                else
                {
                    currentDistance = 0;
                }
                direction -= hit.normal * Vector2.Dot(direction, hit.normal);
            }
            else
            {
                position += direction * distance;
            }

            distance -= currentDistance;
        }

        _rb.MovePosition(position);
    }

    #endregion
}