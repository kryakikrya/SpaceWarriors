using UnityEngine;
using Zenject;

public class AsteroidPresentation : PoolableObject
{
    [SerializeField] private int _minFragmentsCount = 2;
    [SerializeField] private int _maxFragmentsCount = 4;
    [SerializeField] private Transform[] _spawnpoint;
    [SerializeField] private float _fragmentRotateOffset = 10f;

    [SerializeField] private FragmentPresentation _fragment;

    private PoolableObjectFactory<FragmentPresentation> _fragmentFactory;

    private ObjectSettingsProvider _settingsProvider;

    [Inject]
    private void Construct(PoolableObjectFactory<FragmentPresentation> fragmentFactory, ObjectSettingsProvider provider)
    {
        _fragmentFactory = fragmentFactory;

        _settingsProvider = provider;
    }

    public override void InitializeInfo(IObjectSettings settings)
    {
        if (settings is AsteroidSettings)
        {
            base.InitializeInfo(settings);
            GetComponent<AsteroidsFacade>().InitializeInfo((AsteroidSettings) settings);
        }
    }

    public override void Death()
    {
        base.Death();

        int rnd = Random.Range(_minFragmentsCount, _maxFragmentsCount);

        if (_spawnpoint.Length < _maxFragmentsCount)
        {
            Debug.Log("Слишком мало поинтов!");
        }

        for (int i = 0; i < rnd; i++)
        {
            PoolableObject fragment = _fragmentFactory.Create(_fragment);

            fragment.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            fragment.transform.position = _spawnpoint[i].position;
            fragment.InitializeInfo(_settingsProvider.Get<FragmentPresentation>());

            Vector2 velocity = GetComponent<AsteroidsFacade>().Physics.CurrentVelocity;

            fragment.GetComponent<AsteroidMovement>().StartMovement(new Vector2 (velocity.x + Random.Range(-_fragmentRotateOffset, _fragmentRotateOffset), velocity.y + Random.Range(-_fragmentRotateOffset, _fragmentRotateOffset)));
        }

        gameObject.SetActive(false);
    }
}
