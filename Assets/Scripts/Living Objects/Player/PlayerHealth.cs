using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
public class PlayerHealth : Health
{
    private const int MenuSceneID = 0;

    private Invulnerability _invulnerability;

    private float _invulnerabilityTime;

    private PhysicalLayers _physicalLayers;

    private GameObject _player;

    public PlayerHealth(int health, Invulnerability invulnerability, float invulnerabilityTime, PhysicalLayers layers, GameObject player) : base(health)
    {
        _invulnerability = invulnerability;
        _invulnerabilityTime = invulnerabilityTime;
        _physicalLayers = layers;
        _player = player;

        _invulnerability.EnableInvulnerability(_player, _physicalLayers.InvulnerabilityLayer);
    }

    public override async void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Debug.Log(_player.layer.ToString());

        _invulnerability.EnableInvulnerability(_player, _physicalLayers.InvulnerabilityLayer);

        await InvulnerabilityCD();

        _invulnerability.DisableInvulnerability(_player, _physicalLayers.DefaultLayer, _physicalLayers.EnemyLayer, _physicalLayers.InvulnerabilityLayer);
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
