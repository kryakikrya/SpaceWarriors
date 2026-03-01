public class ScoreRewardViewModel : IViewModel
{
    private ScoreRewardModel _model;

    public ReactiveProperty<int> Score = new ReactiveProperty<int>();

    public ScoreRewardViewModel(ScoreRewardModel model)
    {
        _model = model;

        Subscribe();
    }

    public void Subscribe()
    {
        OnModelScoreChanged(_model.Score.Value);
        _model.Score.OnChanged += OnModelScoreChanged;
    }

    private void OnModelScoreChanged(int newValue)
    {
        Score.Value = newValue;
    }

    public void Dispose()
    {
        _model.Score.OnChanged -= OnModelScoreChanged;
    }
}
