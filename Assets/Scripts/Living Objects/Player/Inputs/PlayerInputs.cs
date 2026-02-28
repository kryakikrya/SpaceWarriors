using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LivingObjectPhysics))]
public class PlayerInputs : MonoBehaviour
{
    private float _speed;

    [Inject] private SignalBus _signalBus;

    [Inject] private IPlayerInputSource _inputSource;

    private LivingObjectPhysics _physics;

    private bool _canControl = true;

    public IPlayerInputSource Source => _inputSource;

    private void Start()
    {
        _physics = GetComponent<LivingObjectPhysics>();
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PlayerDamagedSignal>(OnDamaged);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        if (_canControl == false)
        {
            _physics.AddForce(Vector2.zero, _speed);
            return;
        }

        Vector2 movement = _inputSource.Movement;

        _physics.AddForce(movement, _speed);
    }

    private async void OnDamaged(PlayerDamagedSignal signal)
    {
        await BlockInputs(signal.InvulnerabilityTime);

        _canControl = true;
    }

    private async UniTask BlockInputs(float time)
    {
        _canControl = false;

        await UniTask.WaitForSeconds(time);
    }
}
