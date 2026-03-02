using UnityEngine;
using Zenject;

public class UFOMovement : MonoBehaviour
{
    [Inject] private PlayerFacade _playerTarget;

    private LivingFacade _facade;

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
        _facade = GetComponent<LivingFacade>();

        _movementStrategy = _strategyChanger.GetStrategy(transform, _playerTarget.transform);
    }

    private void Update()
    {
        _movementStrategy = _strategyChanger.GetStrategy(transform, _playerTarget.transform);

        _movementStrategy.Move(_playerTarget.gameObject, _facade, _speed);
    }
}
