using UnityEngine;
using Zenject;

public class UFOMovement : MonoBehaviour
{
    [Inject] private PlayerFacade _playerTarget;

    private LivingObjectPhysics _physics;

    private float _speed;

    private IUFOMovementStrategy _movementStrategy;

    private UFOStrategyChanger _strategyChanger;

    public void InitializeInfo(UFOSettings settings)
    {
        _strategyChanger = new UFOStrategyChanger(settings);

        _speed = settings.Speed;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Start()
    {
        _physics = GetComponent<LivingObjectPhysics>();

        _movementStrategy = _strategyChanger.GetStrategy(transform, _playerTarget.transform);
    }

    private void Update()
    {
        _movementStrategy = _strategyChanger.GetStrategy(transform, _playerTarget.transform);

        _movementStrategy.Move(_playerTarget.gameObject, _physics, _speed);
    }
}
