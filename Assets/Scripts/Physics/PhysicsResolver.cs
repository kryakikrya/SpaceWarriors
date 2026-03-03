using System;
using System.Collections.Generic;
using Zenject;

public class PhysicsResolver : ITickable, IDisposable, IFixedTickable
{
    private List<LivingObjectPhysics> _currentFramePhysics = new List<LivingObjectPhysics>();
    private List<LivingObjectPhysics> _elementsToAdd = new List<LivingObjectPhysics>();

    public void AddPhysics(LivingObjectPhysics physics)
    {
        _elementsToAdd.Add(physics);
    }

    public void Tick()
    {
        foreach (var element in _elementsToAdd)
        {
            _currentFramePhysics.Add(element);
        }

        _elementsToAdd.Clear();

        foreach (var physics in _currentFramePhysics)
        {
            physics.Tick();
        }
    }

    public void Dispose()
    {
        foreach (var physics in _currentFramePhysics)
        {
            physics.Dispose();
        }
    }

    public void FixedTick()
    {
        foreach (var physics in _currentFramePhysics)
        {
            physics.FixedTick();
        }
    }
}
