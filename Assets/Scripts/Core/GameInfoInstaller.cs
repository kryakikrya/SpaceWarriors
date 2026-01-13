using UnityEngine;
using Zenject;

public class GameInfoInstaller : MonoInstaller
{
    [SerializeField] private LayerMask _invulnerabilityLayer;
    [SerializeField] private LayerMask _defaultLayer;
    [SerializeField] private GameSettings _settings;
    public override void InstallBindings()
    {
        Container.Bind<Invulnerability>().AsSingle();

        PhysicalLayers physicalLayers = new PhysicalLayers();
        physicalLayers.Initialize(_invulnerabilityLayer, _defaultLayer);

        Container.Bind<PhysicalLayers>().FromInstance(physicalLayers).AsSingle();

        Container.Bind<GameSettings>().FromInstance(_settings).AsSingle();
    }
}