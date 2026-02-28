using UnityEngine;
using System;
using Zenject;

public class PCInputSource : IPlayerInputSource, ITickable
{
    public event Action Shooting;
    public event Action Laser;

    public Vector2 Movement
    {
        get
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            return new Vector2(horizontal, vertical);
        }
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shooting?.Invoke();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            Laser?.Invoke();
    }
}