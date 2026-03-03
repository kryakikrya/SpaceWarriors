using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectSettingsProvider
{
    private Dictionary<Type, IObjectSettings> _settings = new Dictionary<Type, IObjectSettings>();

    public void LoadSetting<Poolable, Setting>(string jsonName) where Poolable : PoolableObject where Setting : IObjectSettings
    {
        _settings.Add(typeof(Poolable), JsonUtility.FromJson<Setting>(File.ReadAllText($"{Application.streamingAssetsPath}/{jsonName}")));
    }

    public IObjectSettings Get<T>() where T : PoolableObject
    {
        {
            return _settings[typeof(T)];
        }
    }
}
