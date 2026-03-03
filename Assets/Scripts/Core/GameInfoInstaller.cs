using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using System.IO;

public class GameInfoInstaller : MonoInstaller
{
    [SerializeField] private string _invulnerabilityLayer;
    [SerializeField] private string _defaultLayer;
    [SerializeField] private string _wrappingLayer;
    [SerializeField] private string _fragmentLayer;
    [SerializeField] private string _asteroidLayer;
    [SerializeField] private string _ufoLayer;

    [SerializeField] private PlayerFacade _player;

    [SerializeField] private GameSettings _settings;

    [SerializeField] private NewRewardDictionary _typeToReward;

    [SerializeField] private string _playerJSON;

    [SerializeField] private VirtualJoystick _joystick;

    [SerializeField] private VirtualJoystick _directionJoystick;

    [SerializeField] private ShootButton[] _buttons;

    public override void InstallBindings()
    {
        EnableControls();

        Container.Bind<Invulnerability>().AsSingle();

        PhysicalLayers physicalLayers = new PhysicalLayers();
        physicalLayers.Initialize(_invulnerabilityLayer, _defaultLayer, _wrappingLayer, _fragmentLayer, _asteroidLayer, _ufoLayer);

        Container.Bind<PhysicalLayers>().FromInstance(physicalLayers).AsSingle();

        Container.Bind<GameSettings>().FromInstance(_settings).AsSingle();

        Container.Bind<PlayerFacade>().FromInstance(_player).AsSingle();

        Container.Bind<Dictionary<PoolableObjectType, int>>().FromInstance(_typeToReward.ToDictionary()).AsSingle();

        Container.Bind<PlayerParametersSettings>().FromInstance(GetSettings(_playerJSON)).AsSingle();

        Container.Bind<PlayerHealth>().AsSingle();

        Container.Bind<AdsController>().AsSingle().NonLazy();

        Container.BindInterfacesTo<FirebaseDataSaver>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<PhysicsResolver>().AsSingle().NonLazy();
    }

    public PlayerParametersSettings GetSettings(string jsonName)
    {
        return JsonUtility.FromJson<PlayerParametersSettings>(File.ReadAllText($"{Application.streamingAssetsPath}/{jsonName}"));
    }

    public void EnableControls()
    {
#if  UNITY_ANDROID || UNITY_IOS
        Container.Bind<IPlayerInputSource>().To<MobileInputSource>().AsSingle().WithArguments(_joystick, _buttons);

        Container.Bind<VirtualJoystick>().FromInstance(_directionJoystick);

        _joystick.gameObject.SetActive(true);
        _directionJoystick.gameObject.SetActive(true);

        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(true);
        }
#else
        Container.BindInterfacesTo<PCInputSource>().AsSingle();
#endif
    }
}

[Serializable]
public class NewRewardDictionary
{
    [SerializeField] private NewRewardItem[] _items;

    public Dictionary<PoolableObjectType, int> ToDictionary()
    {
        Dictionary<PoolableObjectType, int> newDictionary = new Dictionary<PoolableObjectType, int>();

        foreach (var item in _items)
        {
            newDictionary.Add(item.Type, item.Reward);
        }

        return newDictionary;
    }
}

[Serializable]
public class NewRewardItem
{
    [SerializeField] private PoolableObjectType _type;

    public PoolableObjectType Type => _type;

    [SerializeField] private int _reward;

    public int Reward => _reward;
}