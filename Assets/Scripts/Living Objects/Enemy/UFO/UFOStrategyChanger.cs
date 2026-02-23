using UnityEngine;

public class UFOStrategyChanger
{
    private const int DashesInARow = 2;

    private UFOStrategyDefault _default = new UFOStrategyDefault();
    private UFOStrategyDash _dash;

    private float _dashDistance;

    public UFOStrategyChanger(UFOSettings UFOSettings)
    {
        _dash = new UFOStrategyDash();

        _dash.Init(UFOSettings.DashCharingTime, UFOSettings.DashSpeedModificator, UFOSettings.MaxSpeedModificator);

        _dashDistance = UFOSettings.DistanceToStartDash;
    }

    public IUFOMovementStrategy GetStrategy(Transform myPosition, Transform targetPosition)
    {
        if (Vector3.Distance(myPosition.position, targetPosition.position) < _dashDistance)
        {
            return _dash;
        }
        else
        {
            if (_dash.DashCount > DashesInARow)
            {
                _dash.ZeroingCount();
                return _default;
            }
            else if (_dash.IsDashing)
            {
                return _dash;
            }

            return _default;
        }
    }
}
