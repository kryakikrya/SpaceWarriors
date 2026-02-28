using TMPro;
using UnityEngine;

public class ScoreView : View
{
    [SerializeField] private TextMeshProUGUI _score;

    private ScoreRewardViewModel _viewModel;

    public override void SetViewModel(ViewModel viewModel)
    {
        _viewModel = viewModel as ScoreRewardViewModel;

        Subscribe();
    }

    public override void Subscribe()
    {
        ChangeScore(_viewModel.Score.Value);
        _viewModel.Score.OnChanged += ChangeScore;
    }

    public override void Dispose()
    {
        _viewModel.Score.OnChanged -= ChangeScore;
    }

    private void ChangeScore(int value)
    {
        _score.text = value.ToString();
    }
}
