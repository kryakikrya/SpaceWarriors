using UnityEngine;

public class PhysicalLayers
{
    private LayerMask _invulnerabilityLayer;
    private LayerMask _defaultLayer;

    public LayerMask InvulnerabilityLayer => _invulnerabilityLayer;

    public LayerMask DefaultLayer => _defaultLayer;

    public void Initialize(LayerMask invulnerabilityLayer, LayerMask defaultLayer)
    {
        _invulnerabilityLayer = invulnerabilityLayer;
        _defaultLayer = defaultLayer;
    }
}
