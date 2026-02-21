using UnityEngine;

public class PhysicalLayers
{
    private LayerMask _invulnerabilityLayer;
    private LayerMask _defaultLayer;
    private LayerMask _wrappingLayer;
    private LayerMask _fragmentLayer;

    public LayerMask InvulnerabilityLayer => _invulnerabilityLayer;

    public LayerMask DefaultLayer => _defaultLayer;

    public LayerMask WrappingLayer => _wrappingLayer;

    public LayerMask FragmentLayer => _fragmentLayer;

    public void Initialize(LayerMask invulnerabilityLayer, LayerMask defaultLayer, LayerMask wrappingLayer, LayerMask fragmentLayer)
    {
        _invulnerabilityLayer = invulnerabilityLayer;
        _defaultLayer = defaultLayer;
        _wrappingLayer = wrappingLayer;
        _fragmentLayer = fragmentLayer;
    }
}
