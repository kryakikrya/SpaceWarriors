using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LivingObjectPhysics))]
public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Inject] private SignalBus _signalBus;

    public event Action Shooting;

    private LivingObjectPhysics _physics;

    private bool _canControl = true;

    private void Start()
    {
        _physics = GetComponent<LivingObjectPhysics>();
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PlayerDamagedSignal>(OnDamaged);
    }

    private void Update()
    {
        if (_canControl)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            _physics.AddForce(new Vector2(horizontal, vertical), _speed);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shooting?.Invoke();
            }
        }
        else
        {
            _physics.AddForce(_physics.CurrentVelocity, _speed / 3);
        }
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
