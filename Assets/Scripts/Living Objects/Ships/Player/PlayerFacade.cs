using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    private Invulnerability _invulnerability;

    private LivingObjectPhysics _physics;

    private PhysicalLayers _physicalLayers;

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
