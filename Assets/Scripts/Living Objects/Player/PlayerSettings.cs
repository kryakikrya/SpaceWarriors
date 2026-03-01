public class PlayerParametersSettings : IObjectSettings
{
    public float Speed;

    public int MaxHealth;
    public int Health
    {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    public int MaxLasers;
    public int LaserCD;
    public int RotationSpeed;
    public float LaserTime;
    public int LaserDamagePerRate;
    public float DamageRate;
    public float InvulnerabilityTime;
}
