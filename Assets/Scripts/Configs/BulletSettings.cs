[System.Serializable]
public class BulletSettings : IObjectSettings
{
    public int MaxHealth;

    public int Health
    {
        get { return MaxHealth; }
        set { MaxHealth = value; }
    }

    public float Speed;
}
