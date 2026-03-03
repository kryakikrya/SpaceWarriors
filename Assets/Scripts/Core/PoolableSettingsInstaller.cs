using Zenject;

public class PoolableSettingsInstaller : MonoInstaller
{
    private const string AsteroidConfig = "AsteroidConfig.json";
    private const string BulletConfig = "BulletConfig.json";
    private const string FragmentConfig = "FragmentConfig.json";
    private const string UFOConfig = "UFOConfig.json";

    public override void InstallBindings()
    {
        ObjectSettingsProvider settingsProvider = new ObjectSettingsProvider();

        settingsProvider.LoadSetting<AsteroidPresentation, AsteroidSettings>(AsteroidConfig);
        settingsProvider.LoadSetting<BulletPresentation, BulletSettings>(BulletConfig);
        settingsProvider.LoadSetting<FragmentPresentation, FragmentSettings>(FragmentConfig);
        settingsProvider.LoadSetting<UFOPresentation, UFOSettings>(UFOConfig);

        Container.Bind<ObjectSettingsProvider>().FromInstance(settingsProvider).AsSingle();
    }
}
