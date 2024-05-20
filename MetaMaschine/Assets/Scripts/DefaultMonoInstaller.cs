using UnityEngine;
using Zenject;

public class DefaultMonoInstaller : MonoInstaller<DefaultMonoInstaller>
{
    [SerializeField] private AppManager appManager;
    [SerializeField] private ActorManager actorManager;
    [SerializeField] private EnvironmentManager environmentManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private VisualManager visualManager;
    [SerializeField] private ViewManager viewManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Logger logger;

    public override void InstallBindings()
    {
        Container.Bind<IAppManager>().FromInstance(appManager).AsSingle();
        Container.Bind<IActorManager>().FromInstance(actorManager).AsSingle();
        Container.Bind<IEnvironmentManager>().FromInstance(environmentManager).AsSingle();
        Container.Bind<IInputManager>().FromInstance(inputManager).AsSingle();
        Container.Bind<IVisualManager>().FromInstance(visualManager).AsSingle();
        Container.Bind<IViewManager >().FromInstance(viewManager).AsSingle();
        Container.Bind<IUIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<IAudioManager>().FromInstance(audioManager).AsSingle();
        Container.Bind<ILogger>().FromInstance(logger).AsSingle();
    }
}