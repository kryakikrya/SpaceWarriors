using System;
using System.Threading;
using UnityEngine;

public class FragmentFacade : LivingFacade, INeedStartMove
{
    [SerializeField] private AsteroidMovement _movement;

    private CancellationTokenSource _cts;

    private FragmentSettings _settings;

    private void Awake()
    {
        _health = new PoolableObjectHealth(_maxHealth);
    }

    public void InitializeInfo(FragmentSettings settings)
    {
        _health.HealToMax();

        _settings = settings;

        transform.localScale = Vector3.one * UnityEngine.Random.Range(_settings.MinSize, _settings.MaxSize);

        StartMove();
    }

    public async void StartMove()
    {
        try
        {
            _movement.SetSpeed(_settings.Speed);

            _health.HealToMax();

            FragmentVisual visual = new FragmentVisual();
            await visual.FireTask(_cts.Token, transform, _settings.FireTime);

            if (gameObject.activeSelf == true)
            {
                _health.Death();
            }
        }
        catch (OperationCanceledException)
        {

        }
    }

    protected override void Enable()
    {
        base.Enable();
        _cts = new CancellationTokenSource();
    }

    protected override void Disable()
    {
        base.Disable();
        _cts.Cancel();
        _cts.Dispose();
    }
}
