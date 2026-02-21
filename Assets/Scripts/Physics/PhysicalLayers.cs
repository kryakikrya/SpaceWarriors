using UnityEngine;

public class PhysicalLayers
{
    private string _invulnerabilityLayer;
    private string _defaultLayer;
    private string _wrappingLayer;
    private string _fragmentLayer;
    private string _enemyLayer;

    public string InvulnerabilityLayer => _invulnerabilityLayer;

    public string DefaultLayer => _defaultLayer;

    public string WrappingLayer => _wrappingLayer;

    public string FragmentLayer => _fragmentLayer;

    public string EnemyLayer => _enemyLayer;

    public void Initialize(string invulnerabilityLayer, string defaultLayer, string wrappingLayer, string fragmentLayer, string enemyLayer)
    {   
        _invulnerabilityLayer = invulnerabilityLayer;
        _defaultLayer = defaultLayer;
        _wrappingLayer = wrappingLayer;
        _fragmentLayer = fragmentLayer;
        _enemyLayer = enemyLayer;
    }
}
