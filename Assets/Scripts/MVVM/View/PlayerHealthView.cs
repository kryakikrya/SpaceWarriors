using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour, IView
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Image _HPItem;

    private List<Image> _hp = new List<Image>();

    private PlayerHealthViewModel _viewModel;

    public void SetViewModel(IViewModel viewModel)
    {
        _viewModel = viewModel as PlayerHealthViewModel;

        Subscribe();
    }

    public void Subscribe()
    {
        CreateHPItems(_viewModel.HeartsCount.Value);
        _viewModel.HeartsCount.OnChanged += CreateHPItems;

        ChangeHPItems(_viewModel.CurrentHealth.Value);
        _viewModel.CurrentHealth.OnChanged += ChangeHPItems;
    }

    public void Dispose()
    {
        _viewModel.HeartsCount.OnChanged -= CreateHPItems;
        _viewModel.CurrentHealth.OnChanged -= ChangeHPItems;
    }

    private void CreateHPItems(int value)
    {
        for (int i = _hp.Count - 1; i >= 0; i--)
        {
            Destroy(_hp[i].gameObject);
        }

        _hp.Clear();

        for (int i = 0; i < value; i++)
        {
            Image item = Instantiate(_HPItem, _parent);

            _hp.Add(item);

            item.color = Color.red;
        }
    }

    private void ChangeHPItems(int value)
    {
        for (int i = 0; i < _hp.Count; i++)
        {
            if (i < value)
            {
                _hp[i].gameObject.SetActive(true);
            }
            else
            {
                _hp[i].gameObject.SetActive(false);
            }
        }
    }
}
