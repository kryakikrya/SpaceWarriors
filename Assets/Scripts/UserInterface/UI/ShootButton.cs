using System;
using UnityEngine.UI;

public class ShootButton : Button
{
    public event Action OnShoot;

    private void Awake()
    {
        onClick.AddListener(() => OnShoot?.Invoke());
    }
}
