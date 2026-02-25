using UnityEngine;

public class InterfaceBinder : MonoBehaviour
{
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private PlayerFacade _facade;

    private void Start()
    {
        PlayerHealth health = _facade.Health as PlayerHealth;

        PlayerHealthViewModel _healthViewModel = new PlayerHealthViewModel(health.Model);
        _healthViewModel.Subscribe();

        _healthView.SetViewModel(_healthViewModel);
        _healthView.Subscribe();
    }
}
