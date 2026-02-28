public class ScoreRewardViewModel : ViewModel
{
    private ScoreRewardModel _model;

    public ReactiveProperty<int> Score = new ReactiveProperty<int>();

    public ScoreRewardViewModel(ScoreRewardModel model) : base(model)
    {
        _model = model;

        Subscribe();
    }

    public override void Subscribe()
    {
        OnModelScoreChanged(_model.Score.Value);
        _model.Score.OnChanged += OnModelScoreChanged;
    }

    private void OnModelScoreChanged(int newValue)
    {
        Score.Value = newValue;
    }

    public override void Dispose()
    {
        _model.Score.OnChanged -= OnModelScoreChanged;
    }
}
