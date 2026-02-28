using System;
using UnityEngine;

public interface IPlayerInputSource
{
    event Action Shooting;
    event Action Laser;

    Vector2 Movement { get; }
}