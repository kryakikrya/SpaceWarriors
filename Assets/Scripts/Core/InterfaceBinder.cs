using UnityEngine;
using Zenject;

public class InterfaceBinder : MonoBehaviour
{
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private PlayerFacade _facade;

    [SerializeField] private  PlayerObjectPhysics _physics;
    [SerializeField] private PlayerMovementView _movementView;

    [SerializeField] private PlayerLaser _laser;
    [SerializeField] private LaserInformationView _laserView;

    [Inject] private ScoreRewardModel _scoreRewardModel;
    [SerializeField] private ScoreView _scoreView;

    private void Start()
    {
        InitializeHealth();
        InitializeMovement();
        InitializeLaser();
        InitializeScore();
    }

    private void InitializeHealth()
    {
        PlayerHealth health = _facade.Health as PlayerHealth;

        PlayerHealthViewModel healthViewModel = new PlayerHealthViewModel(health.Model);

        _healthView.SetViewModel(healthViewModel);
    }

    private void InitializeMovement()
    {
        PlayerMovementModel model = _physics.Model;

        PlayerMovementViewModel movementViewModel = new PlayerMovementViewModel(model);

        _movementView.SetViewModel(movementViewModel);
    }

    private void InitializeLaser()
    {
        LaserInformationModel model = _laser.Model;

        LaserInformationViewModel movementViewModel = new LaserInformationViewModel(model);

        _laserView.SetViewModel(movementViewModel);
    }

    private void InitializeScore()
    {
        ScoreRewardViewModel viewModel = new ScoreRewardViewModel(_scoreRewardModel);

        _scoreView.SetViewModel(viewModel);
    }
}
