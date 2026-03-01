[System.Serializable]

public class UFOSettings : IObjectSettings
{
    public int MaxHealth;
    public int Health
    {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    public float Speed;
    public float DashCharingTime;
    public float DashSpeedModificator;
    public float MaxSpeedModificator;
    public float DistanceToStartDash;
}
