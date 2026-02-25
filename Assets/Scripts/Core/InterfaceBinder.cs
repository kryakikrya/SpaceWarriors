using UnityEngine;

public class InterfaceBinder : MonoBehaviour
{
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private PlayerFacade _facade;

    [SerializeField] private  PlayerObjectPhysics _physics;
    [SerializeField] private PlayerMovementView _movementView;

    private void Start()
    {
        InitializeHealth();
        InitializeMovement();
    }

    private void InitializeHealth()
    {
        PlayerHealth health = _facade.Health as PlayerHealth;

        PlayerHealthViewModel _healthViewModel = new PlayerHealthViewModel(health.Model);

        _healthView.SetViewModel(_healthViewModel);
        _healthView.Subscribe();
    }

    private void InitializeMovement()
    {
        PlayerMovementModel model = _physics.Model;

        PlayerMovementViewModel _movementViewModel = new PlayerMovementViewModel(model);

        _movementView.SetViewModel(_movementViewModel);
        _movementView.Subscribe();
    }
}
