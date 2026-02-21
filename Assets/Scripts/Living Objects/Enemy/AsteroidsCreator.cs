using UnityEngine;
using Zenject;

public class AsteroidsCreator : MonoBehaviour
{
    private const string JsonName = "AsteroidConfig.json";
    private const float DefaultCameraSize = 5;

    [SerializeField] private PoolableAsteroid _asteroid;

    [SerializeField] private float _asteroidsRate = 3f;
    [SerializeField] private float _rateRandomOffset = 0.5f;
    private float _stepTime;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _player;

    [Inject] private GameSettings _settings;

    private float _currentTime = 0;

    [Inject] private PoolableObjectFactory _factory;

    [Inject] private ObjectPool<PoolableAsteroid> _pool;

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
        _stepTime = _asteroidsRate + Random.Range(-_rateRandomOffset, _rateRandomOffset);

        _currentTime = _stepTime;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _stepTime)
        {
            int rnd = Random.Range(0, _spawnPoints.Length);

            Vector3 directionToTarget = (_player.position - _spawnPoints[rnd].position).normalized;

            PoolableObject asteroid = _pool.GetAvailableObject<AsteroidSettings>(_asteroid, JsonName, _spawnPoints[rnd].position, directionToTarget);

            asteroid.GetComponent<AsteroidMovement>().StartMovement(directionToTarget);

            _currentTime = 0;
        }
    }
}
