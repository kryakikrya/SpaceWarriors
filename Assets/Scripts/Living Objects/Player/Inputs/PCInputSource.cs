using UnityEngine;
using System;
using Zenject;

public class PCInputSource : IPlayerInputSource, ITickable
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Horizontal";

    public event Action Shooting;
    public event Action Laser;

    public Vector2 Movement
    {
        get
        {
            float horizontal = Input.GetAxis(HorizontalAxisName);
            float vertical = Input.GetAxis(VerticalAxisName);

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