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

    public override void InstallBindings()
    {
        Container.Bind<Invulnerability>().AsSingle();

        PhysicalLayers physicalLayers = new PhysicalLayers();
        physicalLayers.Initialize(_invulnerabilityLayer, _defaultLayer, _wrappingLayer, _fragmentLayer, _asteroidLayer, _ufoLayer);

        Container.Bind<PhysicalLayers>().FromInstance(physicalLayers).AsSingle();

        Container.Bind<GameSettings>().FromInstance(_settings).AsSingle();

        Container.Bind<PlayerFacade>().FromInstance(_player).AsSingle();

        Container.Bind<Dictionary<EnemyType, int>>().FromInstance(_typeToReward.ToDictionary()).AsSingle();

        Container.Bind<PlayerSettings>().FromInstance(GetSettings(_playerJSON)).AsSingle();
    }

    public PlayerSettings GetSettings(string jsonName)
    {
        return JsonUtility.FromJson<PlayerSettings>(File.ReadAllText($"{Application.streamingAssetsPath}/{jsonName}"));
    }
}

[Serializable]
public class NewRewardDictionary
{
    [SerializeField] private NewRewardItem[] _items;

    public Dictionary<EnemyType, int> ToDictionary()
    {
        Dictionary<EnemyType, int> newDictionary = new Dictionary<EnemyType, int>();

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
    [SerializeField] private EnemyType _type;

    public EnemyType Type => _type;

    [SerializeField] private int _reward;

    public int Reward => _reward;
}