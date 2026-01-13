using UnityEngine;
using Zenject;

public class LivingFacade : MonoBehaviour
{
    protected Invulnerability _invulnerability;

    protected LivingObjectPhysics _physics;

    protected PhysicalLayers _physicalLayers;

    [Inject]
    private void Construct(Invulnerability invulnerability, PhysicalLayers layers)
    {
        _invulnerability = invulnerability;
        _physicalLayers = layers;
    }

    private void Awake()
    {
        _physics = GetComponent<LivingObjectPhysics>();

        DisableInvulnerability();
    }

    public void EnableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.EnableInvulnerability(gameObject, _physicalLayers.InvulnerabilityLayer));
    }

    public void DisableInvulnerability()
    {
        _physics.ChangeFilter(_invulnerability.DisableInvulnerability(gameObject, _physicalLayers.DefaultLayer, _physicalLayers.InvulnerabilityLayer));
    }
}
