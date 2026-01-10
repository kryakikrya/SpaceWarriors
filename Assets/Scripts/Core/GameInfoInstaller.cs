using UnityEngine;
using Zenject;

public class GameInfoInstaller : MonoInstaller
{
    [SerializeField] private LayerMask _invulnerabilityLayer;
    [SerializeField] private LayerMask _defaultLayer;
    public override void InstallBindings()
    {
        Container.Bind<Invulnerability>().AsSingle();

        PhysicalLayers physicalLayers = new PhysicalLayers();
        physicalLayers.Initialize(_invulnerabilityLayer, _defaultLayer);

        Container.Bind<PhysicalLayers>().FromInstance(physicalLayers).AsSingle();
    }
}