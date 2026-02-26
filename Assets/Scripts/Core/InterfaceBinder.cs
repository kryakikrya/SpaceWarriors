using UnityEngine;

public class InterfaceBinder : MonoBehaviour
{
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private PlayerFacade _facade;

    [SerializeField] private  PlayerObjectPhysics _physics;
    [SerializeField] private PlayerMovementView _movementView;

    [SerializeField] private PlayerLaser _laser;
    [SerializeField] private LaserInformationView _laserView;

    private void Start()
    {
        InitializeHealth();
        InitializeMovement();
        InitializeLaser();
    }

    private void InitializeHealth()
    {
        PlayerHealth health = _facade.Health as PlayerHealth;

        PlayerHealthViewModel _healthViewModel = new PlayerHealthViewModel(health.Model);

        _healthView.SetViewModel(_healthViewModel);
    }

    private void InitializeMovement()
    {
        PlayerMovementModel model = _physics.Model;

        PlayerMovementViewModel _movementViewModel = new PlayerMovementViewModel(model);

        _movementView.SetViewModel(_movementViewModel);
    }

    private void InitializeLaser()
    {
        LaserInformationModel model = _laser.Model;

        LaserInformationViewModel _movementViewModel = new LaserInformationViewModel(model);

        _laserView.SetViewModel(_movementViewModel);
    }
}
