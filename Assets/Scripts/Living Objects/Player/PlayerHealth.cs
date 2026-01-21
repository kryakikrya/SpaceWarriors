using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public PlayerHealth(int health) : base(health)
    {
    }

    public override void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
