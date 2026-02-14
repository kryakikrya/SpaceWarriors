using UnityEngine;
using Zenject;

public class AsteroidsCreator : MonoBehaviour
{
    [SerializeField] private PoolableAsteroid _asteroid;

    [SerializeField] private float _asteroidsRate = 3f;
    [SerializeField] private float _rateRandomOffset = 0.5f;

    private float _currentTime = 0;

    [Inject] private PoolableObjectFactory _factory;

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _asteroidsRate)
        {
            _factory.Create(_asteroid);
        }
    }
}
