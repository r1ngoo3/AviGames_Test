using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAPManager>().AsSingle().NonLazy();
        Container.Bind<AdsManager>().AsSingle().NonLazy();
        Container.Bind<SaveSystem>().AsSingle().NonLazy();
        Container.Bind<AssetProvider>().AsSingle().NonLazy();
    }
}