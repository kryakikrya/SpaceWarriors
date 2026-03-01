using UnityEngine;
using Zenject;
using static Zenject.SignalDeclaration;

public class EnemiesCreator : MonoBehaviour
{
    private const string AsteroidJsonName = "AsteroidConfig.json";
    private const string UFOJsonName = "UFOConfig.json";

    private const float DefaultCameraSize = 5;

    [SerializeField] private AsteroidPresentation _asteroid;
    [SerializeField] private UFOPresentation _UFO;

    [SerializeField] private float _enemiesRate = 3f;
    [SerializeField] private float _rateRandomOffset = 0.5f;
    private float _stepTime;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    private GameSettings _settings;

    private float _currentTime = 0;

    private PoolableObjectFactory _factory;

    [Inject]
    private void Costruct(GameSettings settings, PoolableObjectFactory factory)
    {
        _settings = settings;
        _factory = factory;
    }

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

    private void SpawnAsteroid(int rnd, Vector3 directionToTarget)
    {
        PoolableObject asteroid = _factory.Create<AsteroidSettings>(_asteroid, AsteroidJsonName, _spawnPoints[rnd].position, directionToTarget);

        asteroid.GetComponent<AsteroidMovement>().StartMovement(directionToTarget);
    }

    private void SpawnUFO(int rnd)
    {
        _factory.Create<UFOSettings>(_UFO, UFOJsonName, _spawnPoints[rnd].position, Vector3.zero);
    }
}
