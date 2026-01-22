using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

[RequireComponent (typeof(Rigidbody2D))]
public class LivingObjectPhysics : MonoBehaviour
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

    private Rigidbody2D _rb;
    private List<RaycastHit2D> _hits = null;

    public Action<LivingObjectPhysics> Colliding;

    public Vector2 CurrentVelocity => _velocity;

    #region Initialization

    private void Awake ()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;

        _hits = ListPool<RaycastHit2D>.Get();

        _rb.useFullKinematicContacts = true;
    }

    private void OnDisable()
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

    public virtual void AddForce(Vector2 direction, float speed)
    {
        _frameMovementVector = new Vector2(direction.x * speed / _mass, direction.y * speed / _mass);
    }

    private void Bounce(Vector2 hit, LivingObjectPhysics other)
    {
        Vector2 otherVelocity = -hit / other._mass * _mass / other._bounceReduction * _velocity.magnitude;

        otherVelocity = Vector2.ClampMagnitude(otherVelocity, Mathf.Log(otherVelocity.magnitude, 2));

        other._velocity += otherVelocity;

        _velocity = Vector2.Reflect(_velocity, hit) * other._mass / _mass / _bounceReduction;

        _velocity = Vector2.ClampMagnitude(_velocity, Mathf.Log(_velocity.magnitude, 2));
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

    private void CheckOverlap(Collision2D collision)
    {
        if (_collisionFilter.IsFilteringLayerMask(collision.collider.gameObject))
        {
            return;
        }

        var colliderDistance = Physics2D.Distance(collision.otherCollider, collision.collider);

        if (colliderDistance.isOverlapped)
        {
            collision.otherRigidbody.position += colliderDistance.normal * colliderDistance.distance;
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

    public virtual void Hit(RaycastHit2D hit)
    {
        if (hit.rigidbody.gameObject.TryGetComponent(out LivingObjectPhysics physics))
        {
            Bounce(hit.normal.normalized, physics);

            Colliding?.Invoke(physics);
        }
    }

    #endregion

    #region Filtering

    public void ChangeFilter(ContactFilter2D filter)
    {
        _collisionFilter = filter;
    }

    #endregion
}