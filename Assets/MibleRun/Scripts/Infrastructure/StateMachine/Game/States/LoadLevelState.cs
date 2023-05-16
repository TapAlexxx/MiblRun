using System;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Factories.UIFactory;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Logic.CameraControl;
using Scripts.Logic.HapticControl;
using Scripts.Logic.PlayerControl.Spawn;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Scripts.Infrastructure.StateMachine.Game.States
{
    public class LoadLevelState : IPayloadedState<string>, IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IUIFactory _uiFactory;
        private readonly IStateMachine<IGameState> _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly DiContainer _container;
        private readonly IPersistenceProgressService _persistenceProgressService;
        private readonly ISoundEffectService _soundEffectService;

        public LoadLevelState(IStateMachine<IGameState> gameStateMachine,
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain,
            IUIFactory uiFactory, IPersistenceProgressService persistenceProgressService,
            IGameFactory gameFactory, DiContainer container, ISoundEffectService soundEffectService)
        {
            _soundEffectService = soundEffectService;
            _persistenceProgressService = persistenceProgressService;
            _container = container;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, OnLevelLoad);
        }

        public void Exit()
        {
            
        }

        protected virtual void OnLevelLoad()
        {
            InitGameWorld();

            _loadingCurtain.Hide();
            EnterGameLoop();
        }

        private void InitGameWorld()
        {
            _gameFactory.Clear();
            
            InitHud();
            _uiFactory.CreateUiRoot();

            _soundEffectService.Refresh(_persistenceProgressService.PlayerData.ProgressData.IsSoundOn);

            InitPlayer();
            InitCamera();
        }

        private void InitPlayer()
        {
            PlayerSpawnPoint playerSpawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>();
            if (playerSpawnPoint == null)
                throw new NullReferenceException("no playerSpawnPoint on scene");
            
            _gameFactory.CreatePlayer(playerSpawnPoint.transform);
        }

        private void InitCamera()
        {
            CameraStateChanger cameraStateChanger = Object.FindObjectOfType<CameraStateChanger>();
            if (cameraStateChanger == null)
                throw new NullReferenceException("no camera state changer on scene");
            
            Transform target = _gameFactory.Player.transform;
            if (target == null)
                throw new NullReferenceException("no target for camera, create target first");
            
            cameraStateChanger.Initialize(target);
            cameraStateChanger.SwitchTo(CameraViewState.Start, target);
        }

        private void InitHud()
        {
            _gameFactory.CreateHud();
        }

        private void EnterGameLoop() => 
            _gameStateMachine.Enter<GameLoopState>();

        private void Inject<T>() where T : Object
        {
            foreach (var injectable in Object.FindObjectsOfType<T>())
                _container.Inject(injectable);
        }
    }
}