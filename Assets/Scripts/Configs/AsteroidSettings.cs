[System.Serializable]
public class AsteroidSettings : IObjectSettings
{
    public int MaxHealth;
    public int Health
    {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    public float Speed;
    public float MinSize;
    public float MaxSize;
}
