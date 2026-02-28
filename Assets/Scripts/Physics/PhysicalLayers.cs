public class PhysicalLayers
{
    private string _invulnerabilityLayer;
    private string _defaultLayer;
    private string _wrappingLayer;
    private string _fragmentLayer;
    private string _asteroidLayer;
    private string _ufoLayer;

    public string InvulnerabilityLayer => _invulnerabilityLayer;

    public string DefaultLayer => _defaultLayer;

    public string WrappingLayer => _wrappingLayer;

    public string FragmentLayer => _fragmentLayer;

    public string AsteroidLayer => _asteroidLayer;

    public string UFOLayer => _ufoLayer;

    public void Initialize(string invulnerabilityLayer, string defaultLayer, string wrappingLayer, string fragmentLayer, string enemyLayer, string ufoLayer)
    {   
        _invulnerabilityLayer = invulnerabilityLayer;
        _defaultLayer = defaultLayer;
        _wrappingLayer = wrappingLayer;
        _fragmentLayer = fragmentLayer;
        _asteroidLayer = enemyLayer;
        _ufoLayer = ufoLayer;
    }
}
