using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConfig", menuName = "ScriptableObjects/PhysicsConfig", order = 1)]
public class PhysicsSO : ScriptableObject
{
    public float BounceReduction = 2;

    public float MinMovement = 0.03f;

    public float HitOffset = 0.03f;

    public float MaxSpeed = 5f;

    public float Mass = 1f;

    public float InertionModifier = 5f;
}
