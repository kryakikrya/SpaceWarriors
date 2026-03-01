using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _score;

    private ScoreRewardViewModel _viewModel;

    public void SetViewModel(IViewModel viewModel)
    {
        _viewModel = viewModel as ScoreRewardViewModel;

        Subscribe();
    }

    public void Subscribe()
    {
        ChangeScore(_viewModel.Score.Value);
        _viewModel.Score.OnChanged += ChangeScore;
    }

    public void Dispose()
    {
        _viewModel.Score.OnChanged -= ChangeScore;
    }

    private void ChangeScore(int value)
    {
        _score.text = value.ToString();
    }
}
