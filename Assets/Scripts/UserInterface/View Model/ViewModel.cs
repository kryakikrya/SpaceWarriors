using System;

public abstract class ViewModel : IDisposable
{
    public ViewModel(Model model)
    {
    }

    public abstract void Subscribe();

    public abstract void Dispose();
}
