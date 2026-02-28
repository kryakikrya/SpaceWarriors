using System;
using UnityEngine.SceneManagement;
using YG;

public class AdsController : IDisposable
{
    private string _menuName;

    private PlayerFacade _playerFacade;

    public AdsController(string menu, PlayerFacade facade)
    {
        YG2.onCloseAnyAdv += ChangeScene;

        _menuName = menu;

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
