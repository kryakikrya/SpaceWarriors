using UnityEngine;
using Zenject;

public class GameInfoInstaller : MonoInstaller
{
    [SerializeField] private string _invulnerabilityLayer;
    [SerializeField] private string _defaultLayer;
    [SerializeField] private string _wrappingLayer;
    [SerializeField] private string _fragmentLayer;
    [SerializeField] private string _asteroidLayer;
    [SerializeField] private string _ufoLayer;

    [SerializeField] private PlayerFacade _player;

    [SerializeField] private GameSettings _settings;
    public override void InstallBindings()
    {
        Container.Bind<Invulnerability>().AsSingle();

        PhysicalLayers physicalLayers = new PhysicalLayers();
        physicalLayers.Initialize(_invulnerabilityLayer, _defaultLayer, _wrappingLayer, _fragmentLayer, _asteroidLayer, _ufoLayer);

        Container.Bind<PhysicalLayers>().FromInstance(physicalLayers).AsSingle();

        Container.Bind<GameSettings>().FromInstance(_settings).AsSingle();

        Container.Bind<PlayerFacade>().FromInstance(_player).AsSingle();
    }
}