using System.Collections.Generic;

public class ScoreRewardModel : IModel
{
    public ReactiveProperty<int> Score { get; private set; } = new ReactiveProperty<int>();

    private Dictionary<PoolableObjectType, int> _objectToScore;

    public ScoreRewardModel(Dictionary<PoolableObjectType, int> objectToScore)
    {
        _objectToScore = objectToScore;

        Score.Value = 0;
    }

    public void AddScore(PoolableObjectType type)
    {
        Score.Value += _objectToScore[type];
    }
}
