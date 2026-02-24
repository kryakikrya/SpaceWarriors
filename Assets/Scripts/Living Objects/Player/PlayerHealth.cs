using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using Zenject;
using System;

public class PlayerHealth : Health
{
    private SignalBus _signalBus;

    private const int MenuSceneID = 0;

    private Invulnerability _invulnerability;

    private float _invulnerabilityTime;

    private LivingFacade _player;

    public Action<float> OnInvulnerability;

    public PlayerHealth(int health, Invulnerability invulnerability, float invulnerabilityTime, LivingFacade player, SignalBus signalBus) : base(health)
    {
        _invulnerability = invulnerability;
        _invulnerabilityTime = invulnerabilityTime;
        _player = player;
        _signalBus = signalBus;
    }

    public override async void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        _player.EnableInvulnerability();

        _signalBus.Fire(new PlayerDamagedSignal { InvulnerabilityTime = _invulnerabilityTime });

        await InvulnerabilityCD();

        _player?.DisableInvulnerability();
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
