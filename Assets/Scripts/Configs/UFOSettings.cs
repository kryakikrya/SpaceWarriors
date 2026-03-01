[System.Serializable]

public class UFOSettings : IObjectSettings
{
    public int Health { get; set; }

    public float Speed;
    public float DashCharingTime;
    public float DashSpeedModificator;
    public float MaxSpeedModificator;
    public float DistanceToStartDash;
}
