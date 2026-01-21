using UnityEngine;

public class PhysicalLayers
{
    private LayerMask _invulnerabilityLayer;
    private LayerMask _defaultLayer;
    private LayerMask _wrappingLayer;

    public LayerMask InvulnerabilityLayer => _invulnerabilityLayer;

    public LayerMask DefaultLayer => _defaultLayer;

    public LayerMask WrappingLayer => _wrappingLayer;

    public void Initialize(LayerMask invulnerabilityLayer, LayerMask defaultLayer, LayerMask wrappingLayer)
    {
        _invulnerabilityLayer = invulnerabilityLayer;
        _defaultLayer = defaultLayer;
        _wrappingLayer = wrappingLayer;
    }
}
