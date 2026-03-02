using Cysharp.Threading.Tasks;
using UnityEngine;

public class UFOStrategyDash : IUFOMovementStrategy
{
    private float _dashChargingTime;
    private float _dashSpeedModificator;
    private float _maxSpeedModificator;

    private int _dashCount = 0;

    public bool IsDashing => _isDashing;

    public int DashCount => _dashCount;

    public void Init(float charingTime, float dashSpeedModificator, float maxSpeed)
    {
        _dashChargingTime = charingTime;
        _dashSpeedModificator = dashSpeedModificator;
        _maxSpeedModificator = maxSpeed;
    }

    private bool _isDashing = false;

    public async void Move(GameObject player, LivingFacade facade, float speed)
    {
        if (_isDashing == false)
        {
            _dashCount++;

            await Dash(player, facade, speed);

            _isDashing = false;
        }
    }

    private async UniTask Dash(GameObject player, LivingFacade facade, float speed)
    {
        _isDashing = true;

        LivingObjectPhysics physics = facade.Physics;

        float oldMaxSpeed = physics.MaxSpeed;

        physics.SetMaxSpeed(oldMaxSpeed * _maxSpeedModificator);

        physics.ZeroVelocity();

        Vector3 target = player.transform.position - facade.gameObject.transform.position;

        await UniTask.WaitForSeconds(_dashChargingTime);

        for (int i = 0; i < 10; i++)
        {
            physics.AddForce(target, _dashSpeedModificator);

            await UniTask.WaitForFixedUpdate();
        }

        physics.SetMaxSpeed(oldMaxSpeed);
    }

    public void ZeroingCount()
    {
        _dashCount = 0;
    }
}