using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
public class PlayerHealth : Health
{
    private const int MenuSceneID = 0;

    private Invulnerability _invulnerability;

    private float _invulnerabilityTime;

    private LivingFacade _player;

    public PlayerHealth(int health, Invulnerability invulnerability, float invulnerabilityTime, LivingFacade player) : base(health)
    {
        _invulnerability = invulnerability;
        _invulnerabilityTime = invulnerabilityTime;
        _player = player;
    }

    public override async void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        _player.EnableInvulnerability();

        await InvulnerabilityCD();

        _player.DisableInvulnerability();
    }

    public override void Death()
    {
        SceneManager.LoadScene(MenuSceneID);
    }

    private async UniTask InvulnerabilityCD()
    {
        await UniTask.WaitForSeconds(_invulnerabilityTime);
    }
}
