using System;
using UnityEngine;

[RequireComponent(typeof(LivingObjectPhysics))]
public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event Action Shooting;

    private LivingObjectPhysics _physics;

    private void Start()
    {
        _physics = GetComponent<LivingObjectPhysics>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _physics.AddForce(new Vector2(horizontal, vertical), _speed);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting?.Invoke();
        }
    }
}
