using System;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

public class AdsController : IDisposable
{
    private string _menuName;

    private PlayerFacade _playerFacade;

    [Inject]
    private void Construct(GameSettings settings, PlayerFacade facade)
    {
        YG2.onOpenAnyAdv += ChangeScene;

        _menuName = settings.MenuName;

        _playerFacade = facade;

        _playerFacade.Health.OnObjectDeath += ShowAds;
    }

    public void Dispose()
    {
        _playerFacade.Health.OnObjectDeath -= ShowAds;
    }

    public void ShowAds()
    {
        YG2.InterstitialAdvShow();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(_menuName);
    }
}
