using UnityEngine;
using Zenject;

public class EnemiesCreator : MonoBehaviour
{
    private const string AsteroidJsonName = "AsteroidConfig.json";
    private const string UFOJsonName = "UFOConfig.json";

    private const float DefaultCameraSize = 5;

    [SerializeField] private PoolableAsteroid _asteroid;
    [SerializeField] private PoolableUFO _UFO;

    [SerializeField] private float _enemiesRate = 3f;
    [SerializeField] private float _rateRandomOffset = 0.5f;
    private float _stepTime;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    [Inject] private GameSettings _settings;

    private float _currentTime = 0;

    [Inject] private PoolableObjectFactory _factory;

    [Inject] private ObjectPool<PoolableAsteroid> _asteroidPool;

    [Inject] private ObjectPool<PoolableUFO> _UFOPool;

    private void Start()
    {
        foreach (Transform points in _spawnPoints)
        {
            points.position = points.position * (_settings.CameraSize / DefaultCameraSize);
        }

        RandomTime();
    }

    private void RandomTime()
    {
        _stepTime = _enemiesRate + Random.Range(-_rateRandomOffset, _rateRandomOffset);

        _currentTime = _stepTime;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _stepTime)
        {
            int rnd = Random.Range(0, _spawnPoints.Length);

            int asteroidOrUFO = Random.Range(0, 3);

            PoolableObject poolableObject;

            if (asteroidOrUFO > 1)
            {
                Vector3 directionToTarget = (_player.position - _spawnPoints[rnd].position).normalized;

                poolableObject = SpawnAsteroid(rnd, directionToTarget);
            }
            else
            {
                poolableObject = SpawnUFO(rnd);
            }

            poolableObject.GetComponent<LivingFacade>().Health.HealToMax();

            _currentTime = 0;
        }
    }

    private PoolableObject SpawnAsteroid(int rnd, Vector3 directionToTarget)
    {
        PoolableObject asteroid = _asteroidPool.GetAvailableObject<AsteroidSettings>(_asteroid, AsteroidJsonName, _spawnPoints[rnd].position, directionToTarget);

        asteroid.GetComponent<AsteroidMovement>().StartMovement(directionToTarget);

        return asteroid;
    }

    private PoolableObject SpawnUFO(int rnd)
    {
        PoolableObject ufo = _UFOPool.GetAvailableObject<UFOSettings>(_UFO, UFOJsonName, _spawnPoints[rnd].position, Vector3.zero);

        return ufo;
    }
}
