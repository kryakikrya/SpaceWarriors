public class ScoreRewardViewModel : IViewModel
{
    public ReactiveProperty<int> Score = new ReactiveProperty<int>();

    private ScoreRewardModel _model;

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

    public void Dispose()
    {
        _model.Score.OnChanged -= OnModelScoreChanged;
    }

    private void OnModelScoreChanged(int newValue)
    {
        Score.Value = newValue;
    }
}
