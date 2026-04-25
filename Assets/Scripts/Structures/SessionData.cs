[System.Serializable]
public class SessionData
{
    public string Email;
    public int Score;

    public SessionData()
    {
        Email = string.Empty;
        Score = 0;
    }

    public SessionData(string email)
    {
        Email = email;
        Score = 0;
    }
}