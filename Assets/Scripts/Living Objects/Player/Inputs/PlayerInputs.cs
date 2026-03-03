using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private LivingFacade _facade;

    private float _speed;

    private SignalBus _signalBus;

    private IPlayerInputSource _inputSource;

    private LivingObjectPhysics _physics;

    private bool _canControl = true;

    public IPlayerInputSource Source => _inputSource;

    [Inject]
    private void Construct(SignalBus bus, IPlayerInputSource inputSource)
    {
        _signalBus = bus;
        _inputSource = inputSource;
    }

    private void Start()
    {
        _physics = _facade.Physics;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PlayerDamagedSignal>(OnDamaged);
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

    public void SetSpeed(float speed)
    {
        _speed = speed;
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
