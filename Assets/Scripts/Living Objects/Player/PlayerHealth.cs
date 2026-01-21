using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public override void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
