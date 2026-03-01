using System;
using UnityEngine.UI;

public class ShootButton : Button
{
    public event Action OnShoot;

    protected override void OnEnable()
    {
        onClick.AddListener(() => OnShoot?.Invoke());
    }

    protected override void OnDisable()
    {
        onClick.RemoveAllListeners();
    }
}
