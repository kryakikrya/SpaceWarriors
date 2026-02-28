using UnityEngine;
using System;

public class MobileInputSource : IPlayerInputSource
{
    public event Action Shooting;
    public event Action Laser;

    private VirtualJoystick _joystick;

    private ShootButton[] _shootButtons;

    public Vector2 Movement => _joystick.Direction;

    public MobileInputSource(VirtualJoystick joystick, ShootButton[] shootButtons)
    {
        _joystick = joystick;

        _shootButtons = shootButtons;

        _shootButtons[0].OnShoot += () => Shooting?.Invoke();
        _shootButtons[1].OnShoot += () => Laser?.Invoke();
    }
}