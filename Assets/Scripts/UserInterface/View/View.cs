using System;
using UnityEngine;

public abstract class View : MonoBehaviour, IDisposable
{
    public abstract void SetViewModel(ViewModel vm);

    public abstract void Subscribe();

    public abstract void Dispose();
}
