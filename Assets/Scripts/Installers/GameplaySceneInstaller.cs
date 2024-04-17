using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private CoreLoopSystem coreLoopSystem;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayTimer>().AsSingle().NonLazy();
        Container.Bind<CoreLoopSystem>().FromInstance(coreLoopSystem);
    }
}