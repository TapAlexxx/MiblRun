using DG.Tweening;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.ColorService;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UIFactory;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.Infrastructure.Services.StaticData;
using Scripts.Infrastructure.Services.Wallet;
using Scripts.Infrastructure.Services.Window;
using Scripts.Infrastructure.StateMachine;
using Scripts.Infrastructure.StateMachine.Game;
using Scripts.Infrastructure.StateMachine.Game.States;
using Scripts.Logic.HapticControl;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private LoadingCurtain curtain;
        [SerializeField] private SoundEffectService soundEffectService;

        public override void InstallBindings()
        {
            Debug.Log("Installer");

            BindMonoServices();
            BindServices();
            BindGameStateMachine();
            BindGameStates();

            DOTween.Init();
        }

        public void Initialize() =>
            BootstrapGame();

        private void BindServices()
        {
            Container.Bind<IInitializable>().FromInstance(this).AsSingle();
            BindStaticDataService();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IPersistenceProgressService>().To<PersistenceProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IColorService>().To<ColorService>().AsSingle();
            Container.Bind<IWalletService>().To<WalletService>().AsSingle();
        }
        
        private void BindMonoServices()
        {
            Container.Bind<ICoroutineRunner>().FromMethod(() => Container.InstantiatePrefabForComponent<ICoroutineRunner>(coroutineRunner)).AsSingle();
            Container.Bind<ILoadingCurtain>().FromMethod(() => Container.InstantiatePrefabForComponent<ILoadingCurtain>(curtain)).AsSingle();
            Container.Bind<ISoundEffectService>().FromMethod(() => Container.InstantiatePrefabForComponent<ISoundEffectService>(soundEffectService)).AsSingle();

            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            ISceneLoader sceneLoader = new SceneLoader(Container.Resolve<ICoroutineRunner>());
            Container.Bind<ISceneLoader>().FromInstance(sceneLoader).AsSingle();
        }

        private void BindStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadData();
            Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }

        private void BootstrapGame() => 
            Container.Resolve<IStateMachine<IGameState>>().Enter<BootstrapState>();
    }
}