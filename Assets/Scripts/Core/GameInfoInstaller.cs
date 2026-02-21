using UnityEngine;
using Zenject;

public class GameInfoInstaller : MonoInstaller
{
    [SerializeField] private string _invulnerabilityLayer;
    [SerializeField] private string _defaultLayer;
    [SerializeField] private string _wrappingLayer;
    [SerializeField] private string _fragmentLayer;
    [SerializeField] private string _enemyLayer;

    [SerializeField] private GameSettings _settings;
    public override void InstallBindings()
    {
        Container.Bind<Invulnerability>().AsSingle();

        PhysicalLayers physicalLayers = new PhysicalLayers();
        physicalLayers.Initialize(_invulnerabilityLayer, _defaultLayer, _wrappingLayer, _fragmentLayer, _enemyLayer);

        Container.Bind<PhysicalLayers>().FromInstance(physicalLayers).AsSingle();

        Container.Bind<GameSettings>().FromInstance(_settings).AsSingle();
    }
}