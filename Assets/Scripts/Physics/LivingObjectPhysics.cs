using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class LivingObjectPhysics : IDisposable
{
    [SerializeField] private float _bounceReduction = 2;

    private const int MaxChangesInOneMovement = 3;

    private ContactFilter2D _collisionFilter = new ContactFilter2D().NoFilter();

    [SerializeField] private float _minMovement = 0.03f;

    [SerializeField] private float _hitOffset = 0.03f;

    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _inertionModifier = 5f;

    protected Vector2 _frameMovementVector = Vector2.zero;
    protected Vector2 _velocity = Vector2.zero;

    protected Rigidbody2D _rb;
    private List<RaycastHit2D> _hits = null;

    private Collider2D[] _results = new Collider2D[8];

    private Collider2D _myCollider;

    public Action<RaycastHit2D> Colliding;

    public Vector2 CurrentVelocity => _velocity;

    public float MaxSpeed => _maxSpeed;

    [Inject]
    public void Construct(Rigidbody2D rb)
    {
        _rb = rb;

        _myCollider = _rb.GetComponent<Collider2D>();
        Debug.Log($"LivingObjectPhysics instanceId={GetHashCode()} rb={_rb} col={_myCollider}");

        _rb.bodyType = RigidbodyType2D.Kinematic;

        _hits = ListPool<RaycastHit2D>.Get();

        _rb.useFullKinematicContacts = true;
    }

    public void Dispose()
    {
        if (_hits != null)
        {
            ListPool<RaycastHit2D>.Release(_hits);
            _hits = null;
        }
    }

    public void Tick()
    {
        CheckOverlap();
    }

    public virtual void AddForce(Vector2 direction, float speed)
    {
        Debug.Log("Add force");
        direction = direction.normalized;
        _frameMovementVector = new Vector2(direction.x * speed / _mass, direction.y * speed / _mass);
    }

    public void Bounce(RaycastHit2D other, LivingObjectPhysics physics)
    {
        Vector2 hit = other.normal.normalized;

        Vector2 otherVelocity = -hit / physics._mass * _mass / physics._bounceReduction * _velocity.magnitude;

        otherVelocity = Vector2.ClampMagnitude(otherVelocity, Mathf.Log(otherVelocity.magnitude, 2));

        physics._velocity += otherVelocity;

        _velocity = Vector2.Reflect(_velocity, hit) * physics._mass / _mass / _bounceReduction;

        _velocity = Vector2.ClampMagnitude(_velocity, Mathf.Log(_velocity.magnitude, 2));
    }


    public void ZeroVelocity()
    {
        _velocity = Vector2.zero;
        _frameMovementVector = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Perform();
    }

    public virtual void Perform()
    {
        float delta = Time.fixedDeltaTime;

        _velocity += new Vector2(_frameMovementVector.x * delta * _maxSpeed, _frameMovementVector.y * delta * _maxSpeed);

        _velocity *= Mathf.Exp(-_inertionModifier * delta);

        Move();
    }

    public void SetMaxSpeed(float maxSpeed)
    {
        _maxSpeed = maxSpeed;
    }

    private void CheckOverlap()
    {
        Debug.Log($"CHECK instanceId={GetHashCode()} rb={_rb} col={_myCollider}");
        int count = _myCollider.OverlapCollider(_collisionFilter, _results);

        for (int i = 0; i < count; i++)
        {
            Collider2D other = _results[i];

            var colliderDistance = Physics2D.Distance(other, _myCollider);

            if (colliderDistance.isOverlapped)
            {
                other.attachedRigidbody.position += colliderDistance.normal * colliderDistance.distance;
            }
        }
    }

    protected void Move()
    {
        _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);

        if (_velocity.sqrMagnitude <= Mathf.Epsilon)
        {
            return;
        }

        var vectorDirection = _velocity.normalized;
        var vectorLength = _velocity.magnitude;

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

                Hit(hit);
            }
            else
            {
                position += direction * distance;
            }

            distance -= currentDistance;
        }

        _rb.MovePosition(position);
    }

    public void Hit(RaycastHit2D hit)
    {
        Colliding?.Invoke(hit);
    }

    public void ChangeFilter(ContactFilter2D filter)
    {
        _collisionFilter = filter;
    }
}