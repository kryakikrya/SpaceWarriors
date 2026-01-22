using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private const int MenuSceneID = 0;

    public override void Death()
    {
        SceneManager.LoadScene(MenuSceneID);
    }
}
