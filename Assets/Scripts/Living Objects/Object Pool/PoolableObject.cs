using UnityEngine;
using Zenject;
public abstract class PoolableObject : MonoBehaviour
{
    [SerializeField] private PoolableObjectFacade _facade;

    [SerializeField] private PoolableObjectType _type;

    protected PoolableObjectHealth _health;

    private ScoreRewardModel _rewardModel;

    public PoolableObjectType Type => _type;

    [Inject]
    private void Construct(ScoreRewardModel model)
    {
        _rewardModel = model;
    }

    public virtual void InitializeInfo(IObjectSettings settings)
    {
        _health = new PoolableObjectHealth(settings);

        _facade.InitializeHealth(_health);

        _health.OnObjectDeath += Death;
    }

    public virtual void Death()
    {
        _rewardModel.AddScore(Type);
    }
}
