using UnityEngine;
using Zenject;

public class PoolableAsteroid : PoolableObject
{
    [SerializeField] private int _minFragmentsCount = 2;
    [SerializeField] private int _maxFragmentsCount = 4;
    [SerializeField] private Transform[] _spawnpoint;
    [SerializeField] private float _fragmentRotateOffset = 10f;

    [Inject] private ObjectPool<PoolableAsteroid> _asteroidPool;

    [Inject] private ObjectPool<PoolableFragment> _fragmentPool;

    private const string JsonName = "FragmentConfig.json";

    [SerializeField] private PoolableFragment _fragment;

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is AsteroidSettings)
        {
            GetComponent<AsteroidsFacade>().InitializeInfo((AsteroidSettings) settings);
        }
    }

    public override void Death()
    {
        int rnd = Random.Range(_minFragmentsCount, _maxFragmentsCount);

        if (_spawnpoint.Length < _maxFragmentsCount)
        {
            Debug.Log("Слишком мало поинтов!");
        }

        for (int i = 0; i < rnd; i++)
        {
            PoolableFragment fragment = _fragmentPool.GetAvailableObject<FragmentSettings>(_fragment, JsonName, _spawnpoint[i].position, new Vector3 (0, 0, Random.Range(0, 360f)));
            Vector2 velocity = GetComponent<LivingObjectPhysics>().CurrentVelocity;
            fragment.GetComponent<AsteroidMovement>().StartMovement(new Vector2 (velocity.x + Random.Range(-_fragmentRotateOffset, _fragmentRotateOffset), velocity.y + Random.Range(-_fragmentRotateOffset, _fragmentRotateOffset)));
        }

        _asteroidPool.MakeObjectUnavailable(this);
        gameObject.SetActive(false);
    }
}
