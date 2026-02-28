using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private GameObject _laserVisual;
    [SerializeField] private GameObject _laserOrigin;
    [SerializeField] private GameObject _laserDirection;

    [SerializeField] private PlayerInputs _inputs;

    [SerializeField] private float _laserTime;

    [SerializeField] private int _damagePerRate = 1;

    [SerializeField] private float _damageRate = 0.05f;

    [SerializeField] private float _laserCD;

    private int _maxLasers = 3;

    private int _currentLasers;

    private DamageApplier _applier;

    private bool _isShooting = false;

    public Action<int> OnLaserSpend;

    private LaserInformationModel _model = new LaserInformationModel();

    public LaserInformationModel Model => _model;

    private void Start()
    {
        _applier = new DamageApplier(_damagePerRate);

        _currentLasers = _maxLasers;

        LaserCD().Forget();
    }

    private void OnEnable()
    {
        _inputs.Source.Laser += Laser;
    }

    private void OnDisable()
    {
        _inputs.Source.Laser -= Laser;
    }

    public void SetSettings(int maxLasers, int laserCD)
    {
        _maxLasers = maxLasers;
        _laserCD = laserCD;
    }

    private async void Laser()
    {
        if (_isShooting == false && _currentLasers > 0)
        {
            _laserVisual.SetActive(true);

            SpendLaser();

            await LaserBeam();

            _isShooting = false;

            _laserVisual.SetActive(false);
        }
    }

    private void SpendLaser()
    {
        _currentLasers--;

        _model.ChangeCurrentCharges(_currentLasers);

        OnLaserSpend?.Invoke(_currentLasers);
    }

    private async UniTask LaserBeam()
    {
        float timer = 0;
        _isShooting = true;

        while (timer < _laserTime)
        {
            timer += Time.fixedDeltaTime;

            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(_laserOrigin.transform.position, _laserDirection.transform.position - _laserOrigin.transform.position, 30f);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.TryGetComponent(out LivingFacade facade))
                {
                    _applier.ApplyDamage(facade.Health);
                }
            }

            await UniTask.WaitForSeconds(_damageRate);
        }
    }

    private async UniTask LaserCD()
    {
        await UniTask.WaitForFixedUpdate();
        _model.ChangeCurrentCharges(_currentLasers);

        while (true)
        {
            float timer = 0;

            while (timer < _laserCD)
            {
                await UniTask.WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;

                _model.ChangeCurrentCD(Mathf.Clamp(_laserCD - timer, 0, _laserCD));
            }

            _currentLasers++;

            _currentLasers = Mathf.Clamp(_currentLasers, 0, _maxLasers);

            _model.ChangeCurrentCharges(_currentLasers);
        }
    }
}
