using UnityEngine;
using Zenject;

public class EnemiesCreator : MonoBehaviour
{
    private const float DefaultCameraSize = 5;

    [SerializeField] private AsteroidPresentation _asteroid;
    [SerializeField] private UFOPresentation _UFO;

    [SerializeField] private float _enemiesRate = 3f;
    [SerializeField] private float _rateRandomOffset = 0.5f;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    private GameSettings _settings;

    private float _currentTime = 0;

    private float _stepTime;

    private PoolableObjectFactory<UFOPresentation> _UFOfactory;
    private PoolableObjectFactory<AsteroidPresentation> _asteroidFactory;

    private ObjectSettingsProvider _provider;

    [Inject]
    private void Costruct(GameSettings settings, PoolableObjectFactory<UFOPresentation> UFOFactory, PoolableObjectFactory<AsteroidPresentation> asteroidFactory, ObjectSettingsProvider provider)
    {
        _settings = settings;
        _UFOfactory = UFOFactory;
        _asteroidFactory = asteroidFactory;
        _provider = provider;
    }

    private void Start()
    {
        foreach (Transform points in _spawnPoints)
        {
            points.position = points.position * (_settings.CameraSize / DefaultCameraSize);
        }

        RandomTime();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _stepTime)
        {
            int rnd = Random.Range(0, _spawnPoints.Length);

            int asteroidOrUFO = Random.Range(0, 3);

            if (asteroidOrUFO > 1)
            {
                Vector3 directionToTarget = (_player.position - _spawnPoints[rnd].position).normalized;

                SpawnAsteroid(rnd, directionToTarget);
            }
            else
            {
                SpawnUFO(rnd);
            }

            _currentTime = 0;

            RandomTime();
        }
    }

    private void RandomTime()
    {
        _stepTime = _enemiesRate + Random.Range(-_rateRandomOffset, _rateRandomOffset);
    }

    private void SpawnAsteroid(int rnd, Vector3 directionToTarget)
    {
        PoolableObject asteroid = _asteroidFactory.Create(_asteroid);

        asteroid.transform.position = _spawnPoints[rnd].position;
        asteroid.transform.rotation = Quaternion.Euler(directionToTarget);
        asteroid.InitializeInfo(_provider.Get<AsteroidPresentation>());

        asteroid.GetComponent<AsteroidMovement>().StartMovement(directionToTarget);
    }

    private void SpawnUFO(int rnd)
    {
        PoolableObject ufo = _UFOfactory.Create(_UFO);

        ufo.transform.position = _spawnPoints[rnd].position;
        ufo.InitializeInfo(_provider.Get<UFOPresentation>());
    }
}
